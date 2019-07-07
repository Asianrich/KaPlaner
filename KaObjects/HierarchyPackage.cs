//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KaObjects
{

    public enum Hierarchyrequest { NewServer, Register, Login, Invite }
    public class HierarchyPackage
    {
        public int parent;          //root-server
        public int child;           //Kinder-Knoten
        public int number;          // jedes Kind hat ne Kennung
        public int level;           // Ebene-Erweiterung


        public Hierarchyrequest hierarchyrequest; 
        // // 


        //Funktionen//










    }
}