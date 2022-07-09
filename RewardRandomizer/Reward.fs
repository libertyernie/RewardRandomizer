namespace RewardRandomizer

type Route =
| All
| Bartre | Echidna | Ilia | Sacae
| Eirika | Ephraim
| SpecialBehavior

type Reward = {
    note: string
    item: int
    unit: int option
    slot: int option
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
        this.note
        sprintf "%A" this.routes
        sprintf "%06X" this.offset
        sprintf "%02X" this.item
        match this.unit with
        | Some u -> sprintf "unit %02X" u
        | None -> ()
        match this.slot with
        | Some u -> sprintf "slot %02X" u
        | None -> ()
    ]
