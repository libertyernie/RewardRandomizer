namespace RewardRandomizer

type ItemCategory = Promotion | StatBooster | Other

type Item = {
    id: byte
    name: string
    category: ItemCategory
}

module Item =
    let item id name cat = {
        id = byte id
        name = name
        category = cat
    }
