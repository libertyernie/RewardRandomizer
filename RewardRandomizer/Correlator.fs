namespace RewardRandomizer

open System.Collections.Generic

module Correlator =
    let MutuallyExclusiveFilterSets = [
        let isOnRoute x r = r.Route = Some x
        let isNotOnRoute r = r.Route = None

        // Handle route splits
        for set in [
            [EarlyItem; LateItem]
            [Bartre; Echidna]
            [Ilia; Sacae]
            [EliwoodNormal; EliwoodHard; HectorNormal; HectorHard]
            [Tactician; NoTactician]
            [Lloyd; Linus]
            [Kenneth; Jerme]
            [Eirika; Ephraim]
        ] do
            yield ([for x in set do isOnRoute x], [])

        // Handle items that are neither
        yield ([isNotOnRoute], [])
    ]

    // Determines which items are the "same".
    // Groups of the "same" item are collected into sets.
    // Any items found are removed from the set.
    let GetMatches (rewards: HashSet<Reward>) required optional = [
        // Pick one of the routes/difficulty levels, and scan for items that appear in a special place there
        let primary_filter = List.head required
        let primary_rewards = rewards |> Seq.where primary_filter |> Seq.toList

        let remove item = ignore (rewards.Remove item)

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
                remove x

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
        // Create a HashSet to store objects to be processed
        let untagged_reward_set = HashSet reward_source
        // Get a list of filters that are mutually exclusive with each other (e.g. "is Sacae" and "is Ilia")
        // One of these will simply be "all routes, all difficulty levels"
        let reward_groups = MutuallyExclusiveFilterSets
        // Pass each to GetMatches, which will remove processed items from the HashSet,
        // and return the results
        for (required, optional) in reward_groups do
            yield! GetMatches untagged_reward_set required optional
    ]
