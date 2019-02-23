using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaPlaner.Objects
{
    [Serializable]
    public class KaEvent
    {
        User owner;
        DateTime date;
        string[] members;

    }
}
