namespace RewardRandomizer

module Correlator =
    type Split =
        { Route: Route option
          Condition: Condition option
          Difficulty: Difficulty option }

    let S r c d =
        { Route = r
          Condition = c
          Difficulty = d }

    let private noSplit = S None None None

    let private D x = S None None (Some x)

    let private onSplit (split: Split) (reward: Reward) =
        reward.Route = split.Route
        && reward.Condition = split.Condition
        && reward.Difficulty = split.Difficulty

    let private condition_permutations_for route (rewards: Reward seq) = [
        let names =
            rewards
            |> Seq.where (fun x -> x.Route = route)
            |> Seq.choose (fun x -> x.Condition)
            |> Seq.map (fun (n, _) -> n)
            |> Set.ofSeq
        for x in names do [
            S route (Some (x, true)) None
            S route (Some (x, false)) None
        ]

        [S route None None]
    ]

    let ExtractAll reward_source = [
        let mutable rewards = Set.ofSeq reward_source

        // Determines which items are the "same"
        // Groups of the "same" item are collected into sets
        let ExtractMatches (splits: Split list) (require_matches_everywhere: bool) = [
            let primary_split = List.head splits
            let primary_rewards = rewards |> Set.filter (onSplit primary_split)

            for p in primary_rewards do
                // This function will find the best match for this item on the route given
                let find_matching condition =
                    rewards
                    |> Seq.where (onSplit condition)
                    |> Seq.where (fun r -> r.ItemId = p.ItemId)
                    |> Seq.sortBy (fun r -> [
                        if r.Unit = p.Unit then 1 else 2 // First, prefer an item who's "give to this unit" flag is the same as this one
                        abs (r.Offset - p.Offset) // In case of a tie, prefer the item whose location in the ROM is closer to this one
                    ])
                    |> Seq.tryHead

                // Find a match on each route, and collect matches found into a list
                // If no match, that route will simply not have that item (or whatever item the randomizer ends up replacing it with)
                let matching_set =
                    splits
                    |> Seq.map find_matching
                    |> Seq.choose id
                    |> Set.ofSeq

                // Only add this list of "same" items if there are enough matches
                let required_count = if require_matches_everywhere then List.length splits else 1
                if Set.count matching_set >= required_count then
                    yield matching_set
                    rewards <- Set.difference rewards matching_set
        ]

        for (routeA, routeB) in [
            (Bartre, Echidna)
            (Ilia, Sacae)
            (Eliwood, Hector)
            (Lloyd, Linus)
            (Kenneth, Jerme)
            (Eirika, Ephraim)
        ] do
            let conditionsA = rewards |> condition_permutations_for (Some routeA)
            let conditionsB = rewards |> condition_permutations_for (Some routeB)
            for (lA, lB) in List.allPairs conditionsA conditionsB do
                let list = lA @ lB
                printfn "%O" list
                yield! ExtractMatches list true

        yield! ExtractMatches [D Normal; D Hard; D HectorNormal; D HectorHard] false

        for conditionPair in rewards |> condition_permutations_for None do
            yield! ExtractMatches conditionPair true

        // Any items that aren't route-specific get their own sets, where they are the only item
        //for x in rewards do
        //    if x |> onSplit noSplit then
        //        yield Set.singleton x
    ]
