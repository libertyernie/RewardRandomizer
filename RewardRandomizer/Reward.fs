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

type Condition = string * bool

type Difficulty = Normal | Hard | HectorNormal | HectorHard

type Reward =
    { Method: Method
      ItemId: byte
      Unit: byte
      Offset: int
      Route: Route option
      Condition: Condition option
      Difficulty: Difficulty option }

module internal Reward =
    let route new_route object =
        if object.Route = None
        then { object with Route = Some new_route }
        else failwith "Cannot assign two routes to the same reward"

    let condition name value object =
        if object.Condition = None
        then { object with Condition = Some (name, value) }
        else failwith "Cannot assign two conditions to the same reward"

    let difficulty new_difficulty object =
        if object.Difficulty = None
        then { object with Difficulty = Some new_difficulty }
        else failwith "Cannot assign two difficulty levels to the same reward"

    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offset = offset
          Route = None
          Condition = None
          Difficulty = None }
