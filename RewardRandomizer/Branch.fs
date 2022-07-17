namespace RewardRandomizer

type Route =
| Bartre | Echidna | Ilia | Sacae
| Eliwood | Hector
| Lloyd | Linus
| Kenneth | Jerme
| Eirika | Ephraim

type Difficulty = Normal | Hard | HectorNormal | HectorHard

type Branch =
    { Route: Route option
      Difficulty: Difficulty option
      Special: bool }

module Branch =
    let none =
        { Route = None
          Difficulty = None
          Special = false }

    let route new_route object =
        if object.Route = None
        then { object with Route = Some new_route }
        else failwith "Cannot assign two routes to the same branch object"

    let difficulty new_difficulty object =
        if object.Difficulty = None
        then { object with Difficulty = Some new_difficulty }
        else failwith "Cannot assign two difficulty levels to the same branch object"

    let special object =
        { object with Special = true }
