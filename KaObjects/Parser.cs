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
            //Der absolute Ausgangspfad
            string startPath = @"C:\Malak\";

            //Der relative Pfad
            string relative = @"..\Desktop\Test.txt";

            //Die ermittlung eines absoluten Pfades
            string absolut = Path.GetFullPath(Path.Combine(startPath, relative));

            StreamWriter writer = new StreamWriter(absolut);   

            writer.Write(irgendwas);
            writer.Close();
        }

        /// <summary>
        /// TO FIX: WAS SOLL AUS DER TEXTDATEI GELESEN WERDEN???
        /// </summary>
        public void Read()
        {
            //Der absolute Ausgangspfad
            string startPath = @"C:\Malak\";

            //Der relative Pfad
            string relative = @"..\Desktop\Test.txt";

            //Die ermittlung eines absoluten Pfades
            string absolut = Path.GetFullPath(Path.Combine(startPath, relative));

            StreamReader reader = new StreamReader(absolut);

            var input = reader.ReadToEnd();
            reader.Close();
        }
    }
}
