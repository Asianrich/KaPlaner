using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaPlaner.Networking;
using KaObjects;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Runtime;
using System.IO;
using KaPlaner;
using KaPlaner.Logic;
using WindowsFormsApp1;
using System.Windows.Forms;
using KaPlaner.GUI;
using KaObjects.Storage;



namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
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
                request = Request.Test
            };

            Package asd = client.Start(state);

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
        public void SQLREAD()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";


            Database db = new Database(connectionString);

            KaEvent[] list = db.read("asd").ToArray();

            int a = 0;
        }


        [TestMethod]
        public void readandwrite()
        {
            ClientConnection cc = new ClientConnection();

            User user = new User("aaa", "sss");


            Assert.IsTrue(true);
        }

        [TestMethod]
        public void listviewtest()
        {

            KaEvent[] kaEvents = new KaEvent[5];

            for (int i = 0; i < 3; i++)
            {
                kaEvents[i] = new KaEvent();
                kaEvents[i].Titel = "Titetl " + i;
                kaEvents[i].Ort = "Ort " + i;
            }

            //Form open_list = new Wdw_List(kaEvents);
            //open_list.Show();

            Assert.IsFalse(false);
        }



    }
}
