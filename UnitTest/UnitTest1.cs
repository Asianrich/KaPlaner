using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaPlaner.Networking;
using KaPlaner.Objects;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int a = 2;
            int b = 2;

            User user = new User();



            Assert.AreEqual(a, b);
            
        }

        [TestMethod]
        public void serialization()
        {
            User user = new User("Manfred", "asd");


            byte [] serializeObj = user.Serialize();




            Assert.AreEqual(User.Deserialize(serializeObj), user);



        }

        [TestMethod]
        public void sendObjects()
        {
            ClientConnection client = new ClientConnection();

            client.connectServer();
            User user = new User("Richard", "test");

            

            Assert.IsTrue(client.logging(user));


        }
        
    }
}
