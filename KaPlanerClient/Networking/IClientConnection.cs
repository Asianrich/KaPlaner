using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaObjects;

namespace KaPlaner.Networking
{
    public interface IClientConnection
    {
        StateObject Start(StateObject state);

    }
}
