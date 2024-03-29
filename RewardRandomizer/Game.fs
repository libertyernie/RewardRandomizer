﻿namespace RewardRandomizer

type Game = {
    Name: string
    Items: Item list
    Rewards: Reward list
} with
    member this.GetItem id =
        this.Items
        |> Seq.where (fun y -> y.Id = id)
        |> Seq.head
    member this.Without exclude =
        { Name = this.Name
          Items =
              this.Items
              |> List.where (fun x -> x.Category <> exclude)
          Rewards =
              this.Rewards
              |> List.where (fun x -> (this.GetItem x.ItemId).Category <> exclude) }

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

    exception RewardIndexOutOfBoundsException of int

    let SummarizeDifferences (game: Game) (data: byte[]) = String.concat System.Environment.NewLine [
        let itemName id =
            game.Items
            |> Seq.where (fun x -> x.Id = id)
            |> Seq.map (fun x -> x.Name)
            |> Seq.tryHead
            |> Option.defaultValue (sprintf "%2X" id)

        "Changed:"
        for x in game.Rewards do
            for o in x.Offsets do
                if o < 0 || o >= data.Length then
                    sprintf "%6X (%O): out of bounds" o x.Method
                else if data[o] <> x.ItemId then
                    sprintf "%6X (%O): %s -> %s" o x.Method (itemName x.ItemId) (itemName data[o])
        ""
        "Unchanged:"
        for x in game.Rewards do
            for o in x.Offsets do
                if o < 0 || o >= data.Length then
                    sprintf "%6X (%O): out of bounds" o x.Method
                else if data[o] = x.ItemId then
                    sprintf "%6X (%O): %s" o x.Method (itemName data[o])
    ]
