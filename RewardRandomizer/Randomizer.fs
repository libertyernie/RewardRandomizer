namespace RewardRandomizer

open System
open System.Text

module Randomizer =
    type Mode = Shuffle | Randomize

    type ItemCollection =
    | ItemCollection of seq<Item>
    | AllItemsInCategory of ItemCategory
    | AllItemsNotInCategory of ItemCategory
    | AllItems

    let private itemIsIn itemCollection item =
        match itemCollection with
        | ItemCollection c -> Seq.contains item c
        | AllItemsInCategory c -> item.Category = c
        | AllItemsNotInCategory c -> item.Category <> c
        | AllItems -> true

    type MethodCollection = MethodCollection of seq<Method> | AllMethods

    let private rewardMethodIsIn methodCollection reward =
        match methodCollection with
        | MethodCollection c -> Seq.contains reward.Method c
        | AllMethods -> true

    type RandomizationParameters =
        { Mode: Mode
          Items: ItemCollection
          Methods: MethodCollection }

    type WriteOperation =
        { Offsets: Set<int>
          WriteData: byte }

    let private random = new Random()

    let private GenerateSingleRandomization (game: Game) (parameters: RandomizationParameters) = [
        let item_ids =
            game.Items
            |> Seq.where (itemIsIn parameters.Items)
            |> Seq.map (fun x -> x.Id)
            |> Seq.toList
        let reward_sets =
            game.Rewards
            |> Seq.where (rewardMethodIsIn parameters.Methods)
            |> Seq.where (fun x -> Seq.contains x.ItemId item_ids)
            |> Correlator.ExtractAll
        let shuffled_sets =
            reward_sets
            |> List.sortBy (fun _ -> random.Next())
        for (old_set, new_set) in Seq.zip reward_sets shuffled_sets do
            let new_item =
                match parameters.Mode with
                | Shuffle ->
                    new_set
                    |> Seq.map (fun x -> x.ItemId)
                    |> Seq.distinct
                    |> Seq.exactlyOne
                | Randomize ->
                    item_ids[random.Next (List.length item_ids)]
            for old_location in old_set do
                { Offsets = old_location.Offsets; WriteData = new_item }
    ]

    let private ApplyRandomizationsToBaseline game operations =
        let array = Seq.toArray game.Rewards
        for x in operations do
            for y in 0 .. array.Length - 1 do
                let reward = array[y]
                if reward.Offsets = x.Offsets then
                    array[y] <- { reward with ItemId = x.WriteData }
        { game with Rewards = Array.toList array }

    let rec private GenerateMultipleRandomizations game parameters = [
        match parameters with
        | [] -> ()
        | head::tail ->
            let old_operations = GenerateSingleRandomization game head

            let new_game = ApplyRandomizationsToBaseline game old_operations
            let new_operations = GenerateMultipleRandomizations new_game tail

            for o in old_operations do
                let no_matches =
                    new_operations
                    |> Seq.where (fun x -> x.Offsets = o.Offsets)
                    |> Seq.isEmpty
                if no_matches then
                    yield o

            yield! new_operations
    ]

    let GenerateOperations game parameters =
        GenerateMultipleRandomizations game (List.ofSeq parameters)

    let ApplyOperations (data: byte[]) (operations: seq<WriteOperation>) =
        let arr = Array.copy data
        for x in operations do
            for o in x.Offsets do
                arr[o] <- x.WriteData
        arr

    let CreateIPS (operations: seq<WriteOperation>) = [|
        yield! Encoding.UTF8.GetBytes "PATCH"

        for x in operations do
            for o in x.Offsets do
                if o >>> 24 <> 0 then
                    failwith "Cannot generate an IPS patch that writes to an offset outside the range 0 - 16 MiB"
                if o = 0x454F46 then
                    failwith "Cannot generate an IPS patch that writes to offset 0x454F46"

                byte (o >>> 16)
                byte (o >>> 8)
                byte (o >>> 0)

                byte 0
                byte 1

                x.WriteData

        yield! Encoding.UTF8.GetBytes "EOF"
    |]
