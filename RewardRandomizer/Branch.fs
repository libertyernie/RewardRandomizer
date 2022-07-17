namespace RewardRandomizer

type Route =
| Bartre | Echidna | Ilia | Sacae
| Eliwood | Hector
| Lloyd | Linus
| Kenneth | Jerme
| Eirika | Ephraim

type Condition = string * bool

type Difficulty = Normal | Hard | HectorNormal | HectorHard

type Branch =
    { Route: Route option
      Condition: Condition option
      Difficulty: Difficulty option }

module Branch =
    let none =
        { Route = None
          Condition = None
          Difficulty = None }

    let route new_route object =
        if object.Route = None
        then { object with Route = Some new_route }
        else failwith "Cannot assign two routes to the same branch object"

    let condition name value object =
        if object.Condition = None
        then { object with Condition = Some (name, value) }
        else failwith "Cannot assign two conditions to the same branch object"

    let difficulty new_difficulty object =
        if object.Difficulty = None
        then { object with Difficulty = Some new_difficulty }
        else failwith "Cannot assign two difficulty levels to the same branch object"
