namespace RewardRandomizer

type Game = {
    Name: string
    Items: Item list
    Rewards: Reward list
}

module Game =
    let FE6_JP =
        { Name = "Fire Emblem: The Binding Blade (J)"
          Items = FE6.Items
          Rewards = FE6.JP }

    let FE6_Localization =
        { Name = "Fire Emblem: The Binding Blade (Localization Patch v1.1.3)"
          Items = FE6.Items
          Rewards = FE6.FE6Localization }

    let FE7_US =
        { Name = "Fire Emblem: The Blazing Blade (U)"
          Items = FE7.Items
          Rewards = FE7.US }

    let FE8_US =
        { Name = "Fire Emblem: The Sacred Stones (U)"
          Items = FE8.Items
          Rewards = FE8.US }

    let All = [
        FE6_JP
        FE6_Localization
        FE7_US
        FE8_US
    ]
