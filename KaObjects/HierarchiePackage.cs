using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaObjects
{
    [Serializable]
    public class HierarchiePackage
    {
        public int parent;          //root-server
        public int child;           //Kinder-Knoten
        public int number;          // jedes Kind hat ne Kennung
        public int level;           // Ebene-Erweiterung


        public int test = 0;
        public HierarchiePackage()
        {

        }
    }
}
