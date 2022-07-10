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

type Reward = {
    method: Method
    item: byte
    unit: byte
    offset: int
    route: Route
} with
    override this.ToString() = String.concat " " [
        sprintf "%A" this.route
        sprintf "%A" this.method
        sprintf "%06X" this.offset
        sprintf "%02X" this.item
        if this.unit <> 0uy then
            sprintf "unit %02X" this.unit
    ]

module Reward =
    let consolidate routes =
        match routes |> List.except [All] |> List.distinct with
        | [] -> All
        | [single] -> single
        | _::_::_ -> SpecialBehavior

    let route new_route object = { object with route = consolidate [object.route; new_route] }

    let reward method offset item unit = {
        method = method
        item = byte item
        unit = byte unit
        offset = offset
        route = All
    }
