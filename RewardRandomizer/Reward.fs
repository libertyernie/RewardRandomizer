namespace RewardRandomizer

type Method =
| Chest
| Village
| StartingInventory
| Story
| Sand

type Route =
| EarlyItem | LateItem
| Bartre | Echidna
| Ilia | Sacae
| EliwoodNormal | EliwoodHard | HectorNormal | HectorHard
| Tactician | NoTactician
| Lloyd | Linus
| Kenneth | Jerme
| Eirika | Ephraim

type Reward =
    { Method: Method
      ItemId: byte
      Unit: byte
      Offsets: Set<int>
      Route: Route option }

module internal Reward =
    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offsets = Set.singleton offset
          Route = None }

    let route new_route reward =
        if reward.Route = None
        then { reward with Route = Some new_route }
        else failwith "Object already has route"

    let exclusiveTo routes rewards = seq {
        for x in rewards do
            for y in routes do
                if x.Route = Some y || x.Route = None then { x with Route = Some y }
    }

    let mututallyExclusive rewards =
        let without_offsets =
            rewards
            |> Seq.map (fun x -> { x with Offsets = Set.empty })
            |> Seq.distinct
            |> Seq.toList
        match without_offsets with
        | [single] -> Seq.singleton { single with Offsets = set [for x in rewards do yield! x.Offsets] }
        | _ -> Seq.empty
