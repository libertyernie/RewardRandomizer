namespace RewardRandomizer

open System

module Procedure =
    let random = new Random()

    type Mode = Shuffle | Randomize

    let Run mode game categories methods (data: byte[]) =
        let item_ids =
            game.items
            |> Seq.where (fun x -> Seq.contains x categories)
            |> Seq.map (fun x -> x.id)
            |> Seq.toList
        let location_sets =
            game.locations
            |> Seq.where (fun x -> Seq.contains x.method methods)
            |> Seq.where (fun x -> Seq.contains x.item item_ids)
            |> Correlator.ExtractAll
        let shuffled_sets =
            location_sets
            |> List.sortBy (fun _ -> random.Next())
        for (old_set, new_set) in Seq.zip location_sets shuffled_sets do
            let new_item =
                match mode with
                | Shuffle ->
                    new_set
                    |> Seq.map (fun x -> x.item)
                    |> Seq.distinct
                    |> Seq.exactlyOne
                | Randomize ->
                    item_ids[random.Next (List.length item_ids)]
            for old_location in old_set do
                data[old_location.offset] <- new_item
            ()
