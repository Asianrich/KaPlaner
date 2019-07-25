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
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
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



        private string Serialize<T>(T myObject)
        {
            string msg;
            List<Type> extra = new List<Type>();
            extra.Add(typeof(P2PPackage));
            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw))
                {
                    XmlSerializer xs = new XmlSerializer(myObject.GetType(), extra.ToArray());
                    xs.Serialize(xw, myObject);
                }
                msg = sw.ToString();
            }
            return msg;
        }


        private T DeSerialize<T>(string msg)
        {
            T myObject;
            List<Type> extra = new List<Type>();
            extra.Add(typeof(P2PPackage));
            using (var sr = new StringReader(msg))
            {
                using (var xr = XmlReader.Create(sr))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T),extra.ToArray());
                    myObject = (T)xs.Deserialize(xr);


                }
            }
            return myObject;
        }


        [TestMethod]
        public void ServerEntrytest()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Richard\\source\\repos\\KaPlanerServer\\KaPlanerServer\\Data\\KaPlaner.mdf;Integrated Security=True";

            Database db = new Database(connectionString);

            //db.newServerEntry("192.168.1.5", 110);
        }


        public void DeleteDatePruefung()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Richard\\source\\repos\\KaPlanerServer\\KaPlanerServer\\Data\\KaPlaner.mdf;Integrated Security=True";

            Database db = new Database(connectionString);
        }


        [TestMethod]
        public void CheckMemberList()
        {
            Database test = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            int i = 0;
            i = test.CheckMemberList();

            Console.WriteLine("Listeneintraege: {0}", i);
        }


        [TestMethod]
        public void CheckSaveInvites()
        {
            //int testtermin = 7;
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            List<Package> testlist = new List<Package>();

            //testdb.SaveInvites(testlist, testtermin);
        }


        [TestMethod]
        public void CheckReadInvites()
        {
            List<KaEvent> test = new List<KaEvent>();
            List<KaEvent> test2 = new List<KaEvent>();
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            //string user = "TestUser";

            //test2 = testdb.ReadInvites(user, test);
        }


        [TestMethod]
        public void CheckGetServer()
        {
            int testSeverID = 2;
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            testdb.getServer(testSeverID);
        }


        [TestMethod]
        public void CheckGetServerCount()
        {
            int count = 0;
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            count = testdb.getServerCount();
            Console.WriteLine(count);
        }

        [TestMethod]
        public void AnswerInvitePruefung()
        {

            List<KaEvent> test = new List<KaEvent>();
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");

            User newOwner = new User();
            // newOwner.name = user;
            // kaEvent.owner = newOwner;

            // User newOwner = new User()
            // newOwner.name = user;
            // kaEvent.owner = newOwner;

            //string user = "TestUser";
            // SaveEvent(kaEvent);

        }


        [TestMethod]
        public void CheckLogin()
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";

            User testUser = new User("Mustermann, Max", "passwort123", 10);
            Database testDatabase = new Database(connectionString);
            bool check = false;
            check = testDatabase.login(testUser);
            Console.WriteLine(check.ToString());
        }

        [TestMethod]
        public void CheckRegisterUser()
        {
            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True";

            //User testUser = new User("Mustermann, Max", "passwort123", 10);

            //Database testDatabase = new Database(connectionString);
            //bool check = false;
            //check = testDatabase.registerUser(testUser, "passwort123");
            //Console.WriteLine(check.ToString());
        }


        [TestMethod]
        public void CheckSaveEvents()
        {
            //Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            //User testUser2 = new User("Mustermann, Moritz", "passwort123", 101);
            //DateTime testdatetime = new DateTime(2013, 6, 1);

            //KaEvent testEvent = new KaEvent();
            //testEvent.TerminID = 9999;
            //testEvent.date = testdatetime;
            //testEvent.Titel = "TestTitel";
            //testEvent.Ort = "Musterstadt";
            //DateTime Beginn = new DateTime (2013, 6, 1);     
            //DateTime Ende = new DateTime(2013, 6, 4);
            //testEvent.Beschreibung = "TESTBESCHREIBUNG";
            //testEvent.owner = testUser2;
            //testdb.SaveEvent(testEvent);

            //testdb.SaveInvites(testlist, testtermin);
        }

        [TestMethod]
        public void CheckLoadEvents()
        {
            User testUser = new User("Mustermann, Maja", "passwort123", 101);
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            DateTime testdatetime = new DateTime(2013, 6, 1);

            testdb.LoadEvents(testUser, testdatetime);
        }

        [TestMethod]
        public void CheckRead()
        {
            User testUser = new User("Mustermann, Maja", "passwort123", 101);
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            List<KaEvent> testList = new List<KaEvent>();
            
            testList = testdb.read(testUser.name);
        }

        [TestMethod]
        public void CheckDelete_date()
        {
            int testID = 1;
            Database testdb = new Database("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Data\\User_Calendar.mdf;Integrated Security = True");
            testdb.Delete_date(testID);
        }

        //[TestMethod]
        //public void DoMath()
        //{
        //    int ask = 100;
        //    int numberask = GetDigitCount(ask);
        //    int id = 101;
        //    int numberid = GetDigitCount(id);
        //    bool isUp = false;

        //    if(numberid >= numberask )
        //    {
        //        isUp = true;
        //        //Selbe Ebene oder höhere muss ich eh Hochschicken oO
        //    }
        //    else
        //    {

        //        int dif = (int)(numberask - numberid);
        //        int level = (int)(ask / Math.Pow(10, dif));

        //        //Bin ich das?
        //        if (level != id)
        //        {
        //            isUp = true;
        //        }
        //        else
        //        {
        //            int child = (int)(ask / Math.Pow(10, dif - 1));
        //            //get IP
        //            Assert.IsTrue(true);
        //        }

        //        //Senden
        //    }

        //    Assert.AreEqual(2, 1);

        //}
        //public int GetDigitCount(int number)
        //{
        //    if (number != 0)
        //    {
        //        double baseExp = Math.Log10(Math.Abs(number));
        //        return Convert.ToInt32(Math.Floor(baseExp) + 1);
        //    }
        //    else { return 1; }
        //}


        //[TestMethod]
        //public void p2pSerialize()
        //{
        //    //Package package = new P2PPackage();
        //    Package package = new Package();
        //    package.p2p = new P2PPackage();
        //    //package.packageReference = new P2PPackage();
        //    string test = Serialize<Package>(package);
        //    byte[] msg = Encoding.ASCII.GetBytes(test);

        //    string decode = Encoding.ASCII.GetString(msg);
        //    Package package1 = DeSerialize<Package>(decode);

        //    //AFK
        //    if (package1.hierarchie != null)
        //    {

        //        Assert.IsTrue(true);
        //    }
        //    else
        //    {


        //        Assert.IsTrue(false);
        //    }
        //}

        //[TestMethod]
        //public void Write()
        //{
        //    string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\manhk\\source\\repos\\KaPlaner\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";

        //    IDatabase db = new Database(connectionString);
        //    KaEvent ka = new KaEvent();

        //    ka.owner = new User("qwe");
        //    ka.Titel = String.Format("asd");
        //    ka.Ort = "wer";
        //    ka.Beschreibung = "ads";

        //    ka.Beginn = DateTime.Now;
        //    ka.Ende = DateTime.Now;


        //    db.SaveEvent(ka);
        //    int a = 0;
        //}



        //[TestMethod]
        //public void SQLREAD()
        //{
        //    string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Swathi_Su\\Source\\Repos\\KaPlaner2\\KaPlanerServer\\Data\\User_Calendar.mdf;Integrated Security=True";


        //    Database db = new Database(connectionString);

        //    KaEvent[] list = db.read("asd").ToArray();

        //    int a = 0;
        //}


        //[TestMethod]
        //public void readandwrite()
        //{
        //    ClientConnection cc = new ClientConnection();

        //    User user = new User("aaa", "sss");


        //    Assert.IsTrue(true);
        //}


        //[TestMethod]
        //public void serialization()
        //{


        //    //Binary Serialization. Who knows when i need this
        //    User user = new User("Manfred", "asd");
        //    //byte [] serializeObj = user.Serialize();
        //    //Assert.AreEqual(User.Deserialize(serializeObj), user);

        //    User test;

        //    //XML-Serialization
        //    using (var stream = new MemoryStream())
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(User));

        //        serializer.Serialize(stream, user);


        //        stream.Seek(0, 0);
        //        var asd = serializer.Deserialize(stream);

        //        test = (User)asd;




        //    }

        //    Assert.AreEqual(test, user);

        //}

        //[TestMethod]
        //public void sendObjects()
        //{
        //    ClientConnection client = new ClientConnection();

        //    Package state = new Package
        //    {
        //        user = new User("Richard", "test"),
        //        request = Request.Test
        //    };

        //    Package asd = client.Start(state);

        //    Assert.IsTrue(true);
        //}
    }
}
