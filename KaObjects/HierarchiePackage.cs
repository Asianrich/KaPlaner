using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaObjects
{
    public enum HierarchieRequest { NewServer, RegisterServer, RegisterUser, Login, Invite }

    [Serializable]
    public class HierarchiePackage
    {
        public int parent;          //root-server
        public int child;           //Kinder-Knoten
        public int number;          // jedes Kind hat ne Kennung
        public int level;           // Ebene-Erweiterung
        public HierarchieRequest hierarchieRequest;

        public int test = 0;


        public HierarchiePackage()
        
        {
            
        }

        public void AddChild()
        {

        }

        public void FindRoute()                     // Root finden
        {
            // start with this as root
                            //MyObject root = this;
            // get the parent
                            //MyObject parent = this.Parent;

            // keep going until no more parents
            //while (parent != null)
            //{
                // save the parent
                //root = parent;
                // get the parent of the parent
                //parent = parent.Parent;
            //}

            //return root;
        }







    }





}