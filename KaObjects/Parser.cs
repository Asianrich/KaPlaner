using System.Collections.Generic;

using System.IO;

namespace KaObjects
{
    public static class Parser
    {
        /// <summary>
        /// TO FIX: WAS SOLL IN DIE TEXTDATEI GESCHRIEBEN WERDEN???
        /// </summary>
        /// <param name="irgendwas"></param>
        public static void Write(string irgendwas)
        {
            var path = @"C:\Users\Malak\Desktop\Test.txt";

            StreamWriter writer = new StreamWriter(path, true);

            writer.Write(irgendwas);
            writer.Close();
        }

        /// <summary>
        /// TO FIX: WAS SOLL AUS DER TEXTDATEI GELESEN WERDEN???
        /// </summary>
        public static List<string> Read()
        {
            var path = @"C:\Users\Malak\Desktop\Test.txt";

            StreamReader reader = new StreamReader(path);
            List<string> lines = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }

            reader.Close();

            return lines;
        }
    }
}
