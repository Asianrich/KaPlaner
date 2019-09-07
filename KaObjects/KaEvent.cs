using System;
using System.Collections.Generic;

namespace KaObjects
{
    [Serializable]
    public class KaEvent
    {
        public int TerminID;
        public User owner { get; set; }
        public DateTime date { get; set; }
        public List<User> members { get; set; }

        public string Titel { get; set; }
        public string Ort { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }
        public string Beschreibung { get; set; }

        public KaEvent() { }

        /// <summary>
        /// Shallow copy
        /// </summary>
        /// <param name="kaEvent"></param>
        public KaEvent(KaEvent kaEvent)
        {
            this.TerminID = kaEvent.TerminID;
            this.owner = kaEvent.owner;
            this.date = kaEvent.date;
            this.members = kaEvent.members;
            this.Titel = kaEvent.Titel;
            this.Ort = kaEvent.Ort;
            this.Beginn = kaEvent.Beginn;
            this.Ende = kaEvent.Ende;
            this.Beschreibung = kaEvent.Beschreibung;
        }
    }
}
