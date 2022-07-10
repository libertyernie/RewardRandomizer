open System.IO
open RewardRandomizer

let data = File.ReadAllBytes @"C:\Users\isaac\Desktop\Fire Emblem - Fuuin no Tsurugi (Japan).gba"

let matching_games = seq {
    for g in Game.All do
        let mismatches = seq {
            for l in g.locations do
                if l.offset >= data.Length || data[l.offset] <> l.item then
                    yield l
        }
        if Seq.isEmpty mismatches then
            printfn "Matching game: %s" g.name
            yield g
}

let game = Seq.exactlyOne matching_games

let l2 = game.locations |> Correlator.ExtractAll |> Seq.sortBy id |> Seq.toList

for f in l2 do
    let id = [for x in f do x.item] |> List.distinct |> List.exactlyOne
    for y in f do
        let actual_item = data[y.offset]
        if actual_item <> byte id then
            failwith "Not recognized"

    printfn "--> %O %s" f (Seq.exactlyOne [for x in game.items do if x.id = byte id then x.name])
