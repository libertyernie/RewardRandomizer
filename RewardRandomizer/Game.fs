namespace RewardRandomizer

type Game = {
    Name: string
    Items: Item list
    Locations: Reward list
}

module Game =
    let FE6_JP = {
        Name = "Fire Emblem: The Binding Blade (J)"
        Items = FE6.Items
        Locations = FE6.JP
    }

    let FE6_Localization = {
        FE6_JP with
            Name = "Fire Emblem: The Binding Blade (Localization Patch v1.1.3)"
            Locations = FE6.FE6Localization
    }

    let FE6_ProjectEmber = {
        FE6_JP with
            Name = "Project Ember v1.85"
            Locations = FE6.ProjectEmber
    }

    let FE7_US = {
        Name = "Fire Emblem: The Blazing Blade (U)"
        Items = FE7.Items
        Locations = FE7.US
    }

    let FE8_US = {
        Name = "Fire Emblem: The Sacred Stones (U)"
        Items = FE8.Items
        Locations = FE8.US
    }

    let All = [
        FE6_JP
        FE6_Localization
        FE6_ProjectEmber
        FE7_US
        FE8_US
    ]