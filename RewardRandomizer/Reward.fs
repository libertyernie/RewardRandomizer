namespace RewardRandomizer

type Method =
| Chest
| Village
| StartingInventory
| FE6Story
| FE7Story
| FE8Story
| FE8Desert

type Reward =
    { Method: Method
      ItemId: byte
      Unit: byte
      Offset: int
      Branch: Branch }

module Reward =
    let reward method offset item unit =
        { Method = method
          ItemId = byte item
          Unit = byte unit
          Offset = offset
          Branch = Branch.none }

    let route new_route object =
        { object with Branch = object.Branch |> Branch.route new_route }

    let condition name value object =
        { object with Branch = object.Branch |> Branch.condition name value }

    let difficulty new_difficulty object =
        { object with Branch = object.Branch |> Branch.difficulty new_difficulty }
