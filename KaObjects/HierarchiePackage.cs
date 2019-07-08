using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaObjects
{
    public enum HierarchieRequest { NewServer, RegisterServer, RegisterUser, Invite}
    [Serializable]
    public class HierarchiePackage
    {
        public int destinationID;
        
        public HierarchieRequest HierarchieRequest;
        private Guid packageID; //Package ID? braucht man das? just in case
        public int serverID;
        public string serveradress;
        public int anzUser = -1;
        public int anzConnection = -1;








        public HierarchiePackage()
        {
            GeneratePID();
        }

        private void GeneratePID()
        {
            packageID = Guid.NewGuid();
        }

        public Guid GetPackageID()
        {
            return packageID;
        }


        public void AddChild()
        {


        }



        //public void FindRoute()                     // Root finden
        //{
        //    // start with this as root
        //                    //MyObject root = this;
        //    // get the parent
        //                    //MyObject parent = this.Parent;

        //    // keep going until no more parents
        //    //while (parent != null)
        //    //{
        //        // save the parent
        //        //root = parent;
        //        // get the parent of the parent
        //        //parent = parent.Parent;
        //    //}

        //    //return root;
        //}







    }





}