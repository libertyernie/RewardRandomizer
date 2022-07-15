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
| All
| Bartre | Echidna | Ilia | Sacae | EarlyPercival | LatePercival
| EliwoodNormal | EliwoodHard | HectorNormal | HectorHard
| Eirika | Ephraim | JoshuaAlive | JoshuaDead
| SpecialBehavior

type Reward =
    { Method: Method
      ItemId: byte
      Unit: byte
      Offset: int
      Route: Route }

module internal Reward =
    let consolidate routes =
        match routes |> List.except [All] |> List.distinct with
        | [] -> All
        | [single] -> single
        | _::_::_ -> SpecialBehavior

    let route new_route object = { object with Route = consolidate [object.Route; new_route] }

    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offset = offset
          Route = All }
