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
        public User owner { get; set; }
        public DateTime date { get; set; }
        public string[] members { get; set; }

    }
}
