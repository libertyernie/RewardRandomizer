namespace RewardRandomizer

open System.Collections.Generic

module Correlator =
    let Untag rewards = [
        for (tag, group) in rewards |> Seq.groupBy (fun r -> r.Tag) do
            match tag with
            | None -> yield! group
            | Some t ->
                let without_offsets =
                    group
                    |> Seq.map (fun x -> x.ItemId)
                    |> Seq.distinct
                    |> Seq.length
                match without_offsets with
                | 0 -> failwith "Empty group"
                | 1 -> yield { Seq.head group with Tag = None; Offsets = set [for x in group do yield! x.Offsets] }
                | _ -> ()
    ]

    let IsOnRoute x r = r.Route = Some x
    let IsNotOnRoute r = r.Route = None

    let MutuallyExclusiveFilterSets = [
        // Handle route splits
        for (a, b) in [
            (Bartre, Echidna)
            (Ilia, Sacae)
            (Eliwood, Hector)
            (Tactician, NoTactician)
            (Lloyd, Linus)
            (Kenneth, Jerme)
            (Eirika, Ephraim)
        ] do
            yield ([IsOnRoute a; IsOnRoute b], [])

        // Handle items that are neither
        yield ([IsNotOnRoute], [])
    ]

    let private remove (item: 'T) (set: HashSet<'T>) = set.Remove(item) |> ignore

    // Determines which items are the "same".
    // Groups of the "same" item are collected into sets.
    // Any items found are removed from the set.
    let GetMatches rewards required optional = [
        // Pick one of the routes/difficulty levels, and scan for items that appear in a special place there
        let primary_filter = List.head required
        let primary_rewards = rewards |> Seq.where primary_filter |> Seq.toList

        for p in primary_rewards do
            // This function will find the best match for this item on the route given.
            let find_matching filter =
                rewards
                |> Seq.where filter
                |> Seq.where (fun r -> r.ItemId = p.ItemId)
                |> Seq.sortBy (fun r -> [
                    if r.Unit = p.Unit then 1 else 2 // First, prefer an item who's "give to this unit" flag is the same as this one
                    abs (Set.minElement r.Offsets - Set.minElement p.Offsets) // In case of a tie, prefer the item whose location in the ROM is closer to this one
                ])
                |> Seq.tryHead

            // Find a match on each filter (both required and optional), and collect matches found into a list.
            let matching_set =
                (required @ optional)
                |> Seq.map find_matching
                |> Seq.choose id
                |> Set.ofSeq

            // Remove matched items from the pool for further operations.
            for x in matching_set do
                rewards |> remove x

            // Make sure no required filters lacked a match.
            // If any of them did, this item won't be included in the randomizer.
            let missing_required_filters = seq {
                for filter in required do
                    let missing =
                        matching_set
                        |> Seq.where filter
                        |> Seq.isEmpty
                    if missing then yield filter
            }

            if p.ItemId = 0x65uy then
                printfn "%A" [for x in matching_set do for o in x.Offsets do sprintf "%6X" o]

            if Seq.isEmpty missing_required_filters then
                yield matching_set
    ]

    let ExtractAll reward_source: Set<Reward> list = [
        // Combine objects that have the same tag and other parameters match
        // Discard objects that have the same tag if there are mismatches
        // Create a HashSet to store objects to be processed
        let untagged_reward_set = reward_source |> Untag |> HashSet
        // Get a list of filters that are mutually exclusive with each other (e.g. "is Sacae" and "is Ilia")
        // One of these will simply be "all routes, all difficulty levels"
        let reward_groups = MutuallyExclusiveFilterSets
        // Pass each to GetMatches, which will remove processed items from the HashSet,
        // and return the results
        for (required, optional) in reward_groups do
            yield! GetMatches untagged_reward_set required optional
    ]
