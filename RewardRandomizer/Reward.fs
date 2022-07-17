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

module internal Reward =
    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offsets = [offset]
          Route = None
          Difficulty = None
          Tag = None }

    let route new_route reward =
        if reward.Route <> None then failwith "Object already has route"
        { reward with Route = Some new_route }

    let difficulty new_difficulty reward =
        if reward.Difficulty <> None then failwith "Object already has difficulty"
        { reward with Difficulty = Some new_difficulty }

    let tag tag reward =
        if reward.Tag <> None then failwith "Object already has tag"
        { reward with Tag = Some tag }
