using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace K1TO
{
    class BinaryReader2 : BinaryReader
    {
        public BinaryReader2(System.IO.Stream stream) : base(stream) { }

        public override int ReadInt32()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        public Int16 ReadInt16()
        {
            var data = base.ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        public Int64 ReadInt64()
        {
            var data = base.ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }

        public UInt32 ReadUInt32()
        {
            var data = base.ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

    }
    class FileInformation
    {
        public static List<string> getFileInformation(string filepath)
        {
            ProgramState thing = new ProgramState();
            if (filepath != "")
            {
                BinaryReader2 reader = new BinaryReader2(new FileStream(filepath, FileMode.Open));
                Plugins.ZipPlugin zipFile = new Plugins.ZipPlugin(reader);
                return zipFile.filesWithin;

            }
            return new List<string>{ };


        }
    }
}
