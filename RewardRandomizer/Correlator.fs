namespace RewardRandomizer

open System.Collections.Generic

module Correlator =
    let IsOnRoute x r = r.Route = Some x && r.Difficulty = None
    let IsOnDifficulty x r = r.Route = None && r.Difficulty = Some x
    let IsOnNeither r = r.Route = None && r.Difficulty = None

    let GetMutuallyExclusiveFilterSets reward_source: ((Reward -> bool) list * (Reward -> bool) list) list = [
        let rewards = Set.ofSeq reward_source

        // Handle route splits
        for (a, b) in [
            (Bartre, Echidna)
            (Ilia, Sacae)
            (Eliwood, Hector)
            (Lloyd, Linus)
            (Kenneth, Jerme)
            (Eirika, Ephraim)
        ] do
            yield ([IsOnRoute a; IsOnRoute b], [])

        // Handle items specific to a difficulty level
        let alternate_levels =
            rewards
            |> Seq.choose (fun x -> x.Difficulty)
            |> Seq.except [Normal]
            |> Seq.distinct
            |> Seq.toList

        yield ([IsOnDifficulty Normal], [for x in alternate_levels do IsOnDifficulty x])

        yield ([IsOnNeither], [])
    ]

    // Determines which items are the "same".
    // Groups of the "same" item are collected into sets.
    // Any items found are removed from the set.
    let GetMatches (rewards: HashSet<Reward>) (required: (Reward -> bool) list) (optional: (Reward -> bool) list) = [
        // Pick one of the routes/difficulty levels, and scan for items that appear in a special place there
        let primary_filter = List.head required
        let primary_rewards = rewards |> Seq.where primary_filter

        for p in primary_rewards do
            // This function will find the best match for this item on the route given.
            let find_matching filter =
                rewards
                |> Seq.where filter
                |> Seq.where (fun r -> r.ItemId = p.ItemId)
                |> Seq.sortBy (fun r -> [
                    if r.Unit = p.Unit then 1 else 2 // First, prefer an item who's "give to this unit" flag is the same as this one
                    abs (r.Offsets.Head - p.Offsets.Head) // In case of a tie, prefer the item whose location in the ROM is closer to this one
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
                rewards.Remove x |> ignore

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

            if Seq.isEmpty missing_required_filters then
                yield matching_set
    ]

    let ExtractAll (reward_source: seq<Reward>): Set<Reward> list = [
        let rewards = reward_source |> Reward.untag |> HashSet
        for (required, optional) in GetMutuallyExclusiveFilterSets rewards do
            yield! GetMatches rewards required optional
    ]
