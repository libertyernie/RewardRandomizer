namespace RewardRandomizer

type Method =
| Chest
| Village
| StartingInventory
| FE6Story
| FE7Story
| FE8Story
| FE8Desert

type Reward =
    { Method: Method
      ItemId: byte
      Unit: byte
      Offsets: int list
      Branch: Branch
      Tag: string option }

module Reward =
    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offsets = [offset]
          Branch = Branch.none
          Tag = None }

    let route new_route object =
        { object with Branch = object.Branch |> Branch.route new_route }

    let difficulty new_difficulty object =
        { object with Branch = object.Branch |> Branch.difficulty new_difficulty }

    let special object =
        { object with Branch = object.Branch |> Branch.special }

    let tag tag reward =
        if reward.Tag <> None
        then failwith "Cannot tag reward more than once"
        else { reward with Tag = Some tag }

    let untag rewards = [
        for (tag, group) in rewards |> Seq.groupBy (fun r -> r.Tag) do
            printfn "%A %d" tag (Seq.length group)
            match tag with
            | None -> yield! group
            | Some _ ->
                let without_offsets =
                    group
                    |> Seq.map (fun x -> { x with Offsets = [] })
                    |> Seq.distinct
                    |> Seq.toList
                match without_offsets with
                | [] -> failwith "Empty group"
                | [single] -> yield { single with Tag = None; Offsets = [for x in group do yield! x.Offsets] }
                | _::_ -> ()
    ]
