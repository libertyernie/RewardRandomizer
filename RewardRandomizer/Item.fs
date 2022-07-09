namespace RewardRandomizer

type ItemCategory = Promotion | StatBooster | Other

type Item = {
    id: byte
    name: string
    category: ItemCategory
}
