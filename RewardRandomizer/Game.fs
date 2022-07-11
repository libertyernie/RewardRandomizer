namespace RewardRandomizer

open System.Security.Cryptography

type Game = {
    name: string
    items: Item list
    locations: Reward list
    expected_checksum: string
}

module Game =
    let FE6_JP = {
        name = "Fire Emblem: The Binding Blade (J)"
        items = FE6.Items
        locations = FE6.JP
        expected_checksum = "EFE1EF713F681D88CCBCA950AB8B70F68683AD136FFB337C46D8B9C5A076A0FE"
    }

    let FE6_Localization = {
        name = "Fire Emblem: The Binding Blade (Localization Patch v1.1.3)"
        items = FE6.Items
        locations = FE6.FE6Localization
        expected_checksum = "4B6D0A8226A3820571D8788C73E71C487971346FC8684288F6088ACDA31E49FF"
    }

    let FE7_US = {
        name = "Fire Emblem: The Blazing Blade (U)"
        items = FE7.Items
        locations = FE7.US
        expected_checksum = "C46DF0D50E0017E8346865DE09F06544CA834579CFB124756DB4991EC7DB98A4"
    }

    let FE8_US = {
        name = "Fire Emblem: The Sacred Stones (U)"
        items = FE8.Items
        locations = FE8.US
        expected_checksum = "45363BCDD59646A5C9262B1621C81850B05358CFF903FA495525C256A09440FE"
    }

    let All = [
        FE6_JP
        FE6_Localization
        FE7_US
        FE8_US
    ]

    let ComputeChecksum (data: byte[]) (game: Game) =
        let dx = Array.copy data
        for x in game.locations do
            if x.offset < dx.Length then
                dx[x.offset] <- 0uy
        use sha = SHA256.Create()
        String.concat "" [for b in sha.ComputeHash dx do sprintf "%02X" b]

    let IsCertainlyMatch (data: byte[]) (game: Game) =
        ComputeChecksum data game = game.expected_checksum

    let IsProbablyMatch (data: byte[]) (game: Game) =
        game.locations
        |> Seq.forall (fun r -> r.offset < data.Length && data[r.offset] = r.item)
