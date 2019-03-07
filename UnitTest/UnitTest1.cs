using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaPlaner.Networking;
using KaObjects;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Runtime;
using System.IO;

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


            //Binary Serialization. Who knows when i need this
            User user = new User("Manfred", "asd");
            //byte [] serializeObj = user.Serialize();
            //Assert.AreEqual(User.Deserialize(serializeObj), user);

            User test;

            //XML-Serialization
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));

                serializer.Serialize(stream, user);


                stream.Seek(0, 0);
                var asd = serializer.Deserialize(stream);

                test = (User)asd;
                



            }

            Assert.AreEqual(test, user);

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
