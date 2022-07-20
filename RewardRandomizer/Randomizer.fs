namespace RewardRandomizer

open System
open System.Text

module Randomizer =
    type Mode = Shuffle | Randomize

    type ItemCollection =
    | AllItemsInCategory of ItemCategory
    | AllItemsNotInCategory of ItemCategory
    | AllItems

    let private itemIsIn itemCollection item =
        match itemCollection with
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

    let private GenerateSingleRandomization (game: Game) (parameters: RandomizationParameters) = seq {
        // Get all item IDs in the categories specified by the randomization parameters
        let item_ids =
            game.Items
            |> Seq.where (itemIsIn parameters.Items)
            |> Seq.map (fun x -> x.Id)
            |> Seq.toList
        // Get all rewards using the methods specified by the randomization parameters, and correlate them across route splits
        let reward_sets =
            game.Rewards
            |> Seq.where (rewardMethodIsIn parameters.Methods)
            |> Seq.where (fun x -> Seq.contains x.ItemId item_ids)
            |> Correlator.ExtractAll
        // Take the correlated sets of rewards and shuffle them randomly
        let shuffled_sets =
            reward_sets
            |> List.sortBy (fun _ -> random.Next())
        // Go through both the original and shuffled lists
        for (old_set, new_set) in Seq.zip reward_sets shuffled_sets do
            // Get the item from the shuffled list (or a random item, if that option is selected)
            let new_item =
                match parameters.Mode with
                | Shuffle ->
                    new_set
                    |> Seq.map (fun x -> x.ItemId)
                    |> Seq.distinct
                    |> Seq.exactlyOne
                | Randomize ->
                    item_ids[random.Next (List.length item_ids)]
            // Request a write to the offset(s) for the corresponding item in the original list
            for old_location in old_set do
                { Offsets = old_location.Offsets; WriteData = new_item }
    }

    let private ApplyRandomizationsToBaseline game operations =
        // Copy all rewards to an array
        let array = Seq.toArray game.Rewards
        // Look at each operation
        for x in operations do
            // For each operation, look at all array items
            for y in 0 .. array.Length - 1 do
                let reward = array[y]
                // If the operation and array item have the same set of offsets, replace the "expected item" with this new one
                if reward.Offsets = x.Offsets then
                    array[y] <- { reward with ItemId = x.WriteData }
        // Return a game object representing the original game *after* the operations have been applied
        // This allows us to generate another set of randomizations on top
        { game with Rewards = Array.toList array }

    let rec private GenerateMultipleRandomizations game parameters = seq {
        match parameters with
        | [] -> ()
        | head::tail ->
            // Generate a list of writes that must be made to randomize this game using the first parameter set
            let old_operations = GenerateSingleRandomization game head
            yield! old_operations

            // Take that as a new baseline, and get a list of writes needed to apply the rest of the randomizations
            let new_game = ApplyRandomizationsToBaseline game old_operations
            yield! GenerateMultipleRandomizations new_game tail
    }

    let GenerateOperations game parameters =
        // Pass to GenerateMultipleRandomizations, but only keep the last write to each set of offsets
        GenerateMultipleRandomizations game (List.ofSeq parameters)
        |> Seq.groupBy (fun x -> x.Offsets)
        |> Seq.map (fun (_, g) -> Seq.last g)

    let ApplyOperations (data: byte[]) (operations: seq<WriteOperation>) =
        let arr = Array.copy data
        for x in operations do
            for o in x.Offsets do
                arr[o] <- x.WriteData
        arr

    let private EOF =
        "EOF"
        |> Encoding.UTF8.GetBytes
        |> Seq.toList

    let CreateIPS (operations: seq<WriteOperation>) = [|
        yield! Encoding.UTF8.GetBytes "PATCH"

        for x in operations do
            for o in x.Offsets do
                if o >>> 24 <> 0 then
                    failwith "Cannot generate an IPS patch that writes to an offset outside the range 0 - 16 MiB"

                let offsetData = [
                    byte (o >>> 16)
                    byte (o >>> 8)
                    byte (o >>> 0)
                ]

                if offsetData = EOF then
                    failwithf "Cannot generate an IPS patch that writes to offset %A" offsetData

                yield! offsetData

                byte 0
                byte 1

                x.WriteData

        yield! EOF
    |]
