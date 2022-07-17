namespace RewardRandomizer

open System.Collections.Generic

module Correlator =
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
            let xA = { Branch.none with Route = Some routeA }
            let xB = { Branch.none with Route = Some routeB }
            yield mutex [xA; xB] []

        yield mutex [Branch.none] []

        // Handle items specific to a difficulty level
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
                    abs (r.Offsets.Head - p.Offsets.Head) // In case of a tie, prefer the item whose location in the ROM is closer to this one
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
        let rewards = reward_source |> Reward.untag |> HashSet
        let tagged = [for m in rewards do if m.Tag.IsSome then m]
        if not (List.isEmpty tagged) then failwithf "%A" tagged
        let sets = GetMutuallyExclusiveBranchSets rewards
        for x in sets do
            let o = GetMatches rewards x
            printfn "%A +%d" x (List.length o)
            let tagged = [for m in rewards do if m.Tag.IsSome then m]
            if not (List.isEmpty tagged) then failwithf "%A" tagged
            yield! o
    ]
