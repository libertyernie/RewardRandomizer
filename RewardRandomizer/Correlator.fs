namespace RewardRandomizer

open System.Collections.Generic

module Correlator =
    // Find all condition names on the given route.
    let GetConditionNames route rewards = set [
        for x in rewards do
            if x.Branch.Route = route then
                match x.Branch.Condition with
                | Some (name, _) -> name
                | None -> ()
    ]

    // Given a route, find all pairs of mutually exclusive conditions (like
    // "saved all civilians" or "visited with a certain character") and yield
    // the pairs in lists.
    // A singleton list, with a branch object that matches the route with no
    // special conditions, will also be included.
    let GetMutuallyExclusiveConditionBranchSetsForRoute route rewards = [
        let br = { Branch.none with Route = route }
        yield [br]

        let names = rewards |> GetConditionNames route
        for x in names do
            let b1 = { br with Condition = Some (x, true) }
            let b2 = { br with Condition = Some (x, false) }
            yield [b1; b2]
    ]

    type MututallyExclusiveBranchSet =
        { RequiredBranches: Branch list
          OptionalBranches: Branch list }

    let GetMutuallyExclusiveBranchSets reward_source = [
        let mutex r o = { RequiredBranches = r; OptionalBranches = o }

        let rewards = Set.ofSeq reward_source

        // Handle route splits
        for (routeA, routeB) in [
            (Bartre, Echidna)
            (Ilia, Sacae)
            (Eliwood, Hector)
            (Lloyd, Linus)
            (Kenneth, Jerme)
            (Eirika, Ephraim)
        ] do
            // Create branch objects for each route, grouped into lists based on which ones are mutually exclusive
            // e.g. [A] [A1 A2]
            let mutexBranchGroupsA = rewards |> GetMutuallyExclusiveConditionBranchSetsForRoute (Some routeA)
            // e.g. [B] [BX BY]
            let mutexBranchGroupsB = rewards |> GetMutuallyExclusiveConditionBranchSetsForRoute (Some routeB)
            // Go through all sets whose items, together, are all mutually exlcusive
            // e.g. [A B]     [A1 A2 B]
            //      [A BX BY] [A1 A2 BX BY]
            for (groupA, groupB) in List.allPairs mutexBranchGroupsA mutexBranchGroupsB do
                let combinedMutexBranchGroup = groupA @ groupB
                yield mutex combinedMutexBranchGroup []

        // Peform the same process for items outside of any route split
        let mutexBranchGroups = rewards |> GetMutuallyExclusiveConditionBranchSetsForRoute None
        for group in mutexBranchGroups do
            yield mutex group []

        // Handle items specific to a difficulty level
        // Items specific to both a difficulty level, and to a route and/or condition
        let alternate_levels =
            rewards
            |> Seq.choose (fun x -> x.Branch.Difficulty)
            |> Seq.except [Normal]
            |> Seq.distinct
            |> Seq.toList

        let D x = { Branch.none with Difficulty = Some x }
        yield mutex [D Normal] [for x in alternate_levels do D x]
    ]

    // Determines which items are the "same".
    // Groups of the "same" item are collected into sets.
    // Any items found are removed from the set.
    let GetMatches (rewards: HashSet<Reward>) (mutex: MututallyExclusiveBranchSet) = [
        // Pick one of the branches to scan for items
        let primary_branch = List.head mutex.RequiredBranches
        let primary_rewards = rewards |> Seq.where (fun x -> x.Branch = primary_branch)

        for p in primary_rewards do
            // This function will find the best match for this item on the route given.
            let find_matching branch =
                rewards
                |> Seq.where (fun r -> r.Branch = branch)
                |> Seq.where (fun r -> r.ItemId = p.ItemId)
                |> Seq.sortBy (fun r -> [
                    if r.Unit = p.Unit then 1 else 2 // First, prefer an item who's "give to this unit" flag is the same as this one
                    abs (r.Offset - p.Offset) // In case of a tie, prefer the item whose location in the ROM is closer to this one
                ])
                |> Seq.tryHead

            // Find a match on each branch (both required and optional), and collect matches found into a list.
            let matching_set =
                (mutex.RequiredBranches @ mutex.OptionalBranches)
                |> Seq.map find_matching
                |> Seq.choose id
                |> Set.ofSeq

            // Remove matched items from the pool for further operations.
            for x in matching_set do
                rewards.Remove x |> ignore

            // Make sure no required branches lacked a match.
            // If any of them did, this item won't be included in the randomizer.
            let missing_required_branches = seq {
                for b in mutex.RequiredBranches do
                    let missing =
                        matching_set
                        |> Seq.where (fun x -> x.Branch = b)
                        |> Seq.isEmpty
                    if missing then yield b
            }

            if Seq.isEmpty missing_required_branches then
                yield matching_set
    ]

    let ExtractAll (reward_source: seq<Reward>): Set<Reward> list = [
        let rewards = reward_source |> HashSet
        for x in GetMutuallyExclusiveBranchSets rewards do
            yield! GetMatches rewards x
    ]
