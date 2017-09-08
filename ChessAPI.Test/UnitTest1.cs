using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessAPI;
namespace ChessAPI.Test {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            Board b = new Board();
            Assert.AreEqual("e4", b.GetTile("e4").TileName);
            b.Move(b.GetTile("e2"), b.GetTile("e4"));
            Assert.IsTrue(b.GetTile("e3").CanCaptureEnpassant);
        }
    }
}
