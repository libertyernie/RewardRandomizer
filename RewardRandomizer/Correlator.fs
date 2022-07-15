namespace RewardRandomizer

module Correlator =
    let ExtractAll reward_source = [
        let mutable rewards = Set.ofSeq reward_source

        // Determines which items are the "same"
        // Groups of the "same" item are collected into lists
        let ExtractMatches routes = [
            // Treat the first route listed as the canonical one (e.g. Eliwood normal mode)
            let primary_route = List.head routes
            let primary_rewards = [for x in rewards do if x.route = primary_route then x]

            for p in primary_rewards do
                // This function will find the best match for this item on the route given
                let find_matching route =
                    rewards
                    |> Seq.where (fun r -> r.route = route)
                    |> Seq.where (fun r -> r.item = p.item)
                    |> Seq.sortBy (fun r -> [
                        if r.unit = p.unit then 1 else 2
                        System.Math.Abs(r.offset - p.offset)
                    ])
                    |> Seq.tryHead

                // Find a match on each route, and collect matches found into a list
                // If no match, that route will simply not have that item (or whatever item the randomizer ends up replacing it with)
                let matching_set =
                    routes
                    |> Seq.map find_matching
                    |> Seq.choose id
                    |> Set.ofSeq

                // Only add this list of "same" items if there is more than one item in it
                if Set.count matching_set > 1 then
                    yield matching_set
                    rewards <- Set.difference rewards matching_set
        ]

        // Get all lists of "same" items
        yield! ExtractMatches [Bartre; Echidna]
        yield! ExtractMatches [EarlyPercival; LatePercival]
        yield! ExtractMatches [Ilia; Sacae]
        yield! ExtractMatches [EliwoodNormal; EliwoodHard; HectorNormal; HectorHard]
        yield! ExtractMatches [Eirika; Ephraim]
        yield! ExtractMatches [JoshuaAlive; JoshuaDead; Ephraim]

        // Any items that aren't route-specific get their own lists, where they are the only item
        for x in rewards do
            if x.route = All then
                yield Set.singleton x
    ]
