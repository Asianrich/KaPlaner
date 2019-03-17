using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaPlaner.Networking;
using KaObjects;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Runtime;
using System.IO;

using KaPlaner.Logic;

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

            Package state = new Package
            {
                user = new User("Richard", "test"),
                results = "TEST"
            };

            client.Start(state);

            Assert.IsTrue(true);


        }

        [TestMethod]
        public void writer()
        {


            User user = new User("Man", "123");

            string msg;
            using (var writer = new StringWriter())
            {


                using (var xmlwriter = XmlWriter.Create(writer))
                {

                    XmlSerializer xml = new XmlSerializer(typeof(User));
                    xml.Serialize(xmlwriter, user);




                }

                msg = writer.ToString();


            }


            User isUser;
            using (var reader = new StringReader(msg))
            {


                using (var xmlreader = XmlReader.Create(reader))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(User));
                    var tObjects = xml.Deserialize(xmlreader);

                    isUser = (User)tObjects;



                }

            }









                Assert.IsTrue(true);



        }



        [TestMethod]
        public void readandwrite()
        {
            ClientConnection cc = new ClientConnection();

            User user = new User("aaa", "sss");

            string test = cc.Serialize(user);


            User aso = cc.DeSerialize<User>(test);

            Assert.IsTrue(true);
        }

        
        [TestMethod]
        public void myTest()
        {

            ClientLogic client = new ClientLogic();

            Assert.IsTrue(client.GetTest());
        }



    }
}
