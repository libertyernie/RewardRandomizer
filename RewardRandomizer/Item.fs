namespace RewardRandomizer

type ItemCategory = Consumable | Promotion | MasterSeal | HPBooster | StatBooster | RareStatBooster | Other

type Item =
    { Id: byte
      Name: string
      Category: ItemCategory
      Max: decimal }

module internal Item =
    let item id name cat =
        { Id = byte id
          Name = name
          Category = cat
          Max = 0m }

    let item_max count i =
        { i with Max = count }
