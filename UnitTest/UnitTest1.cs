using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaPlaner.Networking;



namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ClientConnection client = new ClientConnection();
            client.connectServer();

            Assert.AreEqual(1, 1);
            
        }

        
    }
}
