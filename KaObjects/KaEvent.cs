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
        public int TerminID;
        public User owner { get; set; }
        public DateTime date { get; set; }
        public List<string> members { get; set; }

        public string Titel { get; set; }
        public string Ort { get; set; }
        public int Ganztaegig { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }
        public int Prioritaet { get; set; }
        public string Beschreibung { get; set; }
        public string Haeufigkeit { get; set; }
        public int Haeufigkeit_Anzahl { get; set; }
        public int Immer_Wiederholen { get; set; }
        public int Wiederholungen { get; set; }
        public DateTime Wiederholen_bis { get; set; }

        public string Wochentag { get; set; }
        public int XMontag { get; set; }
        public int XDienstag { get; set; }
        public int XMittwoch { get; set; }
        public int XDonnerstag { get; set; }
        public int XFreitag { get; set; }
        public int XSamstag { get; set; }
        public int XSonntag { get; set; }

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
            this.Ganztaegig = kaEvent.Ganztaegig;
            this.Beginn = kaEvent.Beginn;
            this.Ende = kaEvent.Ende;
            this.Prioritaet = kaEvent.Prioritaet;
            this.Beschreibung = kaEvent.Beschreibung;
            this.Haeufigkeit = kaEvent.Haeufigkeit;
            this.Haeufigkeit_Anzahl = kaEvent.Haeufigkeit_Anzahl;
            this.Immer_Wiederholen = kaEvent.Immer_Wiederholen;
            this.Wiederholungen = kaEvent.Wiederholungen;
            this.Wiederholen_bis = kaEvent.Wiederholen_bis;
            this.Wochentag = kaEvent.Wochentag;
            this.XMontag = kaEvent.XMontag;
            this.XDienstag = kaEvent.XDienstag;
            this.XMittwoch = kaEvent.XMittwoch;
            this.XDonnerstag = kaEvent.XDonnerstag;
            this.XFreitag = kaEvent.XFreitag;
            this.XSamstag = kaEvent.XSamstag;
            this.XSonntag = kaEvent.XSonntag;
        }
    }
}
