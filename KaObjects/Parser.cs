using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;

using System.IO;

namespace KaObjects
{
    class Parser
    {
        public Parser()
        {

        }

        /// <summary>
        /// TO FIX: WAS SOLL IN DIE TEXTDATEI GESCHRIEBEN WERDEN???
        /// </summary>
        /// <param name="irgendwas"></param>
        public void Write(string irgendwas)
        {
            var path = @"C:\Users\Malak\Desktop\Test.txt";

            StreamWriter writer = new StreamWriter(path, true);

            writer.Write(irgendwas);
            writer.Close();
        }

        /// <summary>
        /// TO FIX: WAS SOLL AUS DER TEXTDATEI GELESEN WERDEN???
        /// </summary>
        public void Read()
        {
            var path = @"C:\Users\Malak\Desktop\Test.txt";

            StreamReader reader = new StreamReader(path);

            var input = reader.ReadToEnd();
            Console.WriteLine(input);

            reader.Close();
        }
    }
}
