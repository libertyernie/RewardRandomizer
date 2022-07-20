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
            var data1 = new byte[0x100];
            random.NextBytes(data1);
            var operations = new[]
            {
                new Randomizer.WriteOperation(offsets: SetModule.OfArray(new[] { 0x30, 0x50, 0x51 }), writeData: 7),
                new Randomizer.WriteOperation(offsets: SetModule.OfArray(new[] { 0x99 }), writeData: 100),
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

        [TestMethod, ExpectedException(typeof(Exception))]
        public void TestProhibitAmbiguity()
        {
            var random = new Random();
            var data1 = new byte[0x500000];
            random.NextBytes(data1);
            var operations = new[]
            {
                new Randomizer.WriteOperation(offsets: SetModule.OfArray(new[] { 0x454F46 }), writeData: 100),
            };
            Randomizer.CreateIPS(operations);
        }
    }
}