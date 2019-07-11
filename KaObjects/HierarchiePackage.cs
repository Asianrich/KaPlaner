using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaObjects
{
    public enum HierarchieRequest { NewServer, RegisterServer, RegisterUser, Invite, UserLogin}
    public enum HierarchieAnswer { Success, Failure, Error}
    [Serializable]
    public class HierarchiePackage
    {

        public HierarchieAnswer HierarchieAnswer;
        public HierarchieRequest HierarchieRequest;
        private Guid packageID; //Package ID? braucht man das? just in case
        public KaEvent invite; // Später anschauen
        /// <summary>
        /// ServerID
        /// </summary>
        public int sourceID;

        /// <summary>
        /// ServerQuelle
        /// </summary>
        public string sourceAdress;

        /// <summary>
        /// Destination adress
        /// </summary>
        public string destinationAdress;

        /// <summary>
        /// DestinationID
        /// </summary>
        public int destinationID;

        /// <summary>
        /// Die Anwort auf eine registriation-Anfrage.
        /// </summary>
        public int anzUser = -1;

        /// <summary>
        /// Die Antwort auf einen Server-Verbindungsanfrage
        /// </summary>
        public int anzConnection = -1;

        /// <summary>
        /// Username Existent!!!!!
        /// </summary>
        public string login;

        /// <summary>
        /// Ist die Antwort von einem Server.
        /// </summary>
        public string serverAnswer;




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


       // public void AddChild()


        //public void FindRoute()                     
        
        // Root finden
        
        
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