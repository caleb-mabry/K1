using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1TO.Interfaces
{
    interface ArchiveInformation
    {
        string Extension 
        {
            get;
        }
        int FileByteIdentifier
        {
            get;
        }
    }
}
