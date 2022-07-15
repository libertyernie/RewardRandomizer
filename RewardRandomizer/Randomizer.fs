namespace RewardRandomizer

open System
open System.Text

module Randomizer =
    type Mode = Shuffle | Randomize

    type ItemCollection = ItemCollection of seq<Item> | AllItems
    with
        member this.Contains item =
            match this with
            | ItemCollection c -> Seq.contains item c
            | AllItems -> true

    type MethodCollection = MethodCollection of seq<Method> | AllMethods
    with
        member this.Contains method =
            match this with
            | MethodCollection c -> Seq.contains method c
            | AllMethods -> true

    type RandomizationParameters =
        { Mode: Mode
          Items: ItemCollection
          Methods: MethodCollection }

    type WriteOperation =
        { Offset: int
          WriteData: byte[] }
    with
        member this.Split (max_length: int) =
            match this.WriteData.Length with
            | 0 ->
                []
            | x when x <= max_length ->
                [this]
            | _ ->
                let initial =
                    { Offset = this.Offset
                      WriteData = Array.take max_length this.WriteData }
                let remainder = 
                    { Offset = this.Offset + max_length
                      WriteData = Array.skip max_length this.WriteData }
                initial :: remainder.Split max_length

    let private random = new Random()

    let GenerateOperations (game: Game) (parameters: RandomizationParameters seq) = seq {
        for p in parameters do
            let item_ids =
                game.Items
                |> Seq.where (fun x -> p.Items.Contains x)
                |> Seq.map (fun x -> x.Id)
                |> Seq.toList
            let reward_sets =
                game.Rewards
                |> Seq.where (fun x -> p.Methods.Contains x.Method)
                |> Seq.where (fun x -> Seq.contains x.ItemId item_ids)
                |> Correlator.ExtractAll
            let shuffled_sets =
                reward_sets
                |> List.sortBy (fun _ -> random.Next())
            for (old_set, new_set) in Seq.zip reward_sets shuffled_sets do
                let new_item =
                    match p.Mode with
                    | Shuffle ->
                        new_set
                        |> Seq.map (fun x -> x.ItemId)
                        |> Seq.distinct
                        |> Seq.exactlyOne
                    | Randomize ->
                        item_ids[random.Next (List.length item_ids)]
                for old_location in old_set do
                    { Offset = old_location.Offset; WriteData = [| new_item |] }
    }

    let ApplyOperations (data: byte[]) (operations: seq<WriteOperation>) =
        let arr = Array.copy data
        for x in operations do
            Array.Copy(x.WriteData, 0, arr, x.Offset, x.WriteData.Length)
        arr

    let CreateIPS (operations: seq<WriteOperation>) = [|
        yield! Encoding.UTF8.GetBytes "PATCH"

        for operation in operations do
            for x in operation.Split 0xFFFF do
                if x.Offset >>> 24 <> 0 then
                    failwith "Cannot generate an IPS patch that writes to an offset outside the range 0 - 16 MiB"
                if x.Offset = 0x454F46 then
                    failwith "Cannot generate an IPS patch that writes to offset 0x454F46"

                byte (x.Offset >>> 16)
                byte (x.Offset >>> 8)
                byte (x.Offset >>> 0)

                if x.WriteData.Length >>> 16 <> 0 then
                    failwith "Cannot generate an IPS patch that writes 64 KiB or more in one hunk"

                byte (x.WriteData.Length >>> 8)
                byte (x.WriteData.Length >>> 0)

                yield! x.WriteData

        yield! Encoding.UTF8.GetBytes "EOF"
    |]
