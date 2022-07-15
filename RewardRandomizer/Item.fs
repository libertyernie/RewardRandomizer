namespace RewardRandomizer

type ItemCategory = Promotion | StatBooster | Other

type Item =
    { Id: byte
      Name: string
      Category: ItemCategory }

module internal Item =
    let item id name cat =
        { Id = byte id
          Name = name
          Category = cat }
