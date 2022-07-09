open System.IO
open RewardRandomizer
open RewardRandomizer.Correlation

let data = File.ReadAllBytes @"C:\Users\isaac\Desktop\Fire Emblem - Fuuin no Tsurugi (Japan).gba"
for f in Game.FE6_JP.Locations |> Correlator.ExtractSets do 
    let id = [for x in f do x.item] |> List.distinct |> List.exactlyOne
    for y in f do
        let actual_item = data[y.offset]
        if actual_item <> byte id then
            failwith "Not recognized"

    printfn "--> %O %s" f (Seq.exactlyOne [for x in Game.FE6_JP.Items do if x.id = byte id then x.name])
