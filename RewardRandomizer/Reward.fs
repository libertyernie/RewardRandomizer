namespace RewardRandomizer

type Method =
| Chest
| Village
| StartingInventory
| FE6Story
| FE7Story
| FE8Story
| FE8Desert

type Route =
| Bartre | Echidna | Ilia | Sacae
| Eliwood | Hector
| Lloyd | Linus
| Kenneth | Jerme
| Eirika | Ephraim

type Difficulty = Normal | Hard | HectorNormal | HectorHard

type Reward =
    { Method: Method
      ItemId: byte
      Unit: byte
      Offsets: int list
      Route: Route option
      Difficulty: Difficulty option
      Tag: string option }

module Reward =
    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offsets = [offset]
          Route = None
          Difficulty = None
          Tag = None }

    let route new_route reward =
        { reward with Route = Some new_route }

    let difficulty new_difficulty reward =
        { reward with Difficulty = Some new_difficulty }

    let tag tag reward =
        { reward with Tag = Some tag }

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
