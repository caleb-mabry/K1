using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace K1TO
{
    
    class FileInformation
    {
        public string filename;
        public byte[] data = new byte[] { };
        public Enum compression;
        public List<FileInformation> children = new List<FileInformation>();

        public FileInformation()
        {}
        public FileInformation(string filename)
        {
            this.filename = filename;
        }
    }
}
