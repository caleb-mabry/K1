using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace K1TO
{
    class FileInformation
    {
        public static string testing()
        {
            ProgramState thing = new ProgramState();
            byte[] buffer = new byte[4];
            string word = "";
            using (Stream fs = File.OpenRead(@"C:\Users\Koi\Desktop\Test.zip"))
            {
                fs.Read(buffer,0,4);

                foreach(byte bitty in buffer)
                {
                    word += Convert.ToChar(bitty);
                }
            }
            return word;
        }
    }
}
