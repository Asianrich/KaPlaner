using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace KaObjects
{
    [Serializable]
    public class KaEvent
    {
        User owner { get; set; }
        DateTime date { get; set; }
        string[] members { get; set; }

        public string Title { get; set; }
        public string Ort { get; set; }
        public int ganztaegig { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }
        public int Prioritaet { get; set; }
        public string Beschreibung { get; set; }
        public string Haeufigkeit { get; set; }
        public int Haeufigkeit_Anzahl { get; set; }
        public int Immer_Wiederholen { get; set; }
        public int Wiederholungen { get; set; }
        public DateTime Wiederholen_bis { get; set; }
        public int[] XWochentag { get; set; }
    }
}
