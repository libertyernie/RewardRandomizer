namespace RewardRandomizer

open System

module Procedure =
    let random = new Random()

    type Mode = Shuffle | Randomize

    type ItemCollection = ItemCollection of seq<Item> | AllItems

    let isItemOf this item =
        match this with
        | ItemCollection c -> Seq.contains item c
        | AllItems -> true

    type MethodCollection = MethodCollection of seq<Method> | AllMethods

    let isMethodOf this location =
        match this with
        | MethodCollection m -> Seq.contains location.method m
        | AllMethods -> true

    let Run mode game items methods (data: byte[]) =
        let item_ids =
            game.items
            |> Seq.where (isItemOf items)
            |> Seq.map (fun x -> x.id)
            |> Seq.toList
        let location_sets =
            game.locations
            |> Seq.where (isMethodOf methods)
            |> Seq.where (fun x -> Seq.contains x.item item_ids)
            |> Correlator.ExtractAll game
        let shuffled_sets =
            location_sets
            |> List.sortBy (fun _ -> random.Next())
        for (old_set, new_set) in Seq.zip location_sets shuffled_sets do
            let old_item =
                old_set
                |> Seq.map (fun x -> x.item)
                |> Seq.distinct
                |> Seq.exactlyOne
            let new_item =
                match mode with
                | Shuffle ->
                    new_set
                    |> Seq.map (fun x -> x.item)
                    |> Seq.distinct
                    |> Seq.exactlyOne
                | Randomize ->
                    item_ids[random.Next (List.length item_ids)]
            let old_name = [for i in game.items do if i.id = old_item then i.name]
            let new_name = [for i in game.items do if i.id = new_item then i.name]
            printfn "Replacing %O with %O (%O -> %O)" old_name new_name old_set new_set
            for old_location in old_set do
                data[old_location.offset] <- new_item
            ()
