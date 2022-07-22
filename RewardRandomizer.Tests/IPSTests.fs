namespace RewardRandomizer.Tests

open System
open System.Diagnostics
open System.IO
open Microsoft.VisualStudio.TestTools.UnitTesting
open RewardRandomizer.Randomizer
open RewardRandomizer

[<TestClass>]
type IPSTests() =
    [<TestMethod>]
    member _.TestGeneration() =
        let random = new Random()
        let data1 = Array.zeroCreate<byte> 0x100
        random.NextBytes data1
        let operations = [
            { Offsets = set [0x30; 0x50; 0x51]; WriteData = 7uy }
            { Offsets = set [0x9a]; WriteData = 100uy }
        ]
        if not (File.Exists "Lunar IPS.exe") then
            Assert.Inconclusive()
        File.WriteAllBytes("original_file", data1)
        File.WriteAllBytes("patch_file", Randomizer.CreateIPS operations)
        Process.Start("Lunar IPS.exe", "-ApplyIPS patch_file original_file").WaitForExit();
        let expected = File.ReadAllBytes "original_file"
        File.Delete "original_file"
        File.Delete "patch_file"
        let actual = Randomizer.ApplyOperations data1 operations
        Assert.AreEqual(List.ofArray expected, List.ofArray actual)

    [<TestMethod>]
    [<ExpectedException(typeof<Exception>)>]
    member _.TestProhibitAmbiguity() =
        let random = new Random()
        let data1 = Array.zeroCreate<byte> 0x500000
        random.NextBytes data1
        let operations = [{ Offsets = set [0x454F46]; WriteData = 100uy }]
        Randomizer.CreateIPS operations
