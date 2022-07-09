open System.IO
open RewardRandomizer
open RewardRandomizer.Correlation

let data = File.ReadAllBytes @"C:\Users\isaac\Desktop\Fire Emblem - The Sacred Stones (USA, Australia).gba"

let matching_games = seq {
    for g in Game.All do
        let mismatches = seq {
            for l in g.Locations do
                if l.offset >= data.Length || data[l.offset] <> l.item then
                    printfn "Mismatch found in game %s: %O" g.Name l
                    yield l
        }
        if Seq.isEmpty mismatches then
            printfn "Matching game: %s" g.Name
            yield g
}

let game = Seq.exactlyOne matching_games

for f in game.Locations |> Correlator.ExtractSets do
    let id = [for x in f do x.item] |> List.distinct |> List.exactlyOne
    for y in f do
        let actual_item = data[y.offset]
        if actual_item <> byte id then
            failwith "Not recognized"

    printfn "--> %O %s" f (Seq.exactlyOne [for x in game.Items do if x.id = byte id then x.name])
