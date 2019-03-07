using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace KaPlaner.Objects
{
    [Serializable]
    public class KaEvent
    {
        User owner;
        DateTime date;
        string[] members;

        public string Title;
        public string Ort;

        public KaEvent(string Title)
        {
            this.Title = Title;
        }

        public byte[] Serialize()
        {
            BinaryFormatter bin = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            bin.Serialize(mem, this);
            return mem.GetBuffer();
        }

        public KaEvent Deserialize(byte[] dataBuffer)
        {
            BinaryFormatter bin = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            mem.Write(dataBuffer, 0, dataBuffer.Length);
            mem.Seek(0, 0);
            return (KaEvent)bin.Deserialize(mem);

        }

    }
}
