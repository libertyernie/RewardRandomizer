namespace RewardRandomizer

type ItemCategory = Promotion | StatBooster | Other

type Item = {
    id: byte
    name: string
    category: ItemCategory
} with
    override this.ToString() = sprintf "%02X %s" this.id this.name

module Item =
    let item id name cat = {
        id = byte id
        name = name
        category = cat
    }
