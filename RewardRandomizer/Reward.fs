namespace RewardRandomizer

type Method = Chest | Give | GiveToUnit | StartingInventory

type Route =
| All
| Bartre | Echidna | Ilia | Sacae
| Eirika | Ephraim
| SpecialBehavior

type Reward = {
    method: Method
    item: byte
    unit: byte option
    offset: int
    routes: Route list
} with
    member this.Route =
        match List.distinct this.routes with
        | [] -> All
        | [x] -> x
        | _::_::_ -> SpecialBehavior
    member this.IsToUnit =
        this.unit <> None
    member this.IsAsExpected (data: byte[]) =
        data[this.offset] = byte this.item
    override this.ToString() = String.concat " " [
        sprintf "%A" this.Route
        sprintf "%A" this.method
        sprintf "%06X" this.offset
        sprintf "%02X" this.item
        match this.unit with
        | Some u -> sprintf "unit %02X" u
        | None -> ()
    ]

module Reward =
    let route newRoute object =
        { object with routes = newRoute :: object.routes }

    let reward method offset item unit = {
        method = method
        item = byte item
        unit = unit |> Option.map byte
        offset = offset
        routes = []
    }
