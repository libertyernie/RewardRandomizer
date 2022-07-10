namespace RewardRandomizer

type Game = {
    name: string
    items: Item list
    locations: Reward list
}

module Game =
    let FE6_JP = {
        name = "Fire Emblem: The Binding Blade (J)"
        items = FE6.Items
        locations = FE6.JP
    }

    let FE6_Localization = {
        name = "Fire Emblem: The Binding Blade (Localization Patch v1.1.3)"
        items = FE6.Items
        locations = FE6.FE6Localization
    }

    let FE7_US = {
        name = "Fire Emblem: The Blazing Blade (U)"
        items = FE7.Items
        locations = FE7.US
    }

    let FE8_US = {
        name = "Fire Emblem: The Sacred Stones (U)"
        items = FE8.Items
        locations = FE8.US
    }

    let All = [
        FE6_JP
        FE6_Localization
        FE7_US
        FE8_US
    ]