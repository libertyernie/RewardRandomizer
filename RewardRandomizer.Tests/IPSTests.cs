using Microsoft.FSharp.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace RewardRandomizer.Tests
{
    [TestClass]
    public class IPSTests
    {
        [TestMethod]
        public void TestGeneration()
        {
            var random = new Random();
            var data1 = new byte[0x200000];
            random.NextBytes(data1);
            byte[] randomData = new byte[0x1E0000];
            random.NextBytes(randomData);
            var operations = new[]
            {
                new Randomizer.WriteOperation(offset: 0x30, writeData: new byte[] { 1, 2, 3 }),
                new Randomizer.WriteOperation(offset: 0x10000, writeData: randomData),
            };
            if (!File.Exists("Lunar IPS.exe"))
                Assert.Inconclusive();
            File.WriteAllBytes("original_file", data1);
            File.WriteAllBytes("patch_file", Randomizer.CreateIPS(operations));
            Process.Start("Lunar IPS.exe", "-ApplyIPS patch_file original_file").WaitForExit();
            var expected = File.ReadAllBytes("original_file");
            File.Delete("original_file");
            File.Delete("patch_file");
            var actual = Randomizer.ApplyOperations(data1, operations);
            Assert.AreEqual(
                ListModule.OfArray(expected),
                ListModule.OfArray(actual));
        }
    }
}