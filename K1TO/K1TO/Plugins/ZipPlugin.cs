using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace K1TO.Plugins
{
    class ZipPlugin : K1TO.Interfaces.ArchiveInformation
    {
        public string Extension => ".zip";

        public int FileByteIdentifier => 0;
        public FileInformation FileInfo;
        public byte[] NewFileMagic = new byte[] { 0x50 };
        public byte[] compressedData = new byte[] { };
        public int fileHeader;
        public short versionExtract;
        public short generalPurposeBitFlag;
        public short compressionMethod;
        public short fileLastModificationTime;
        public short fileLastModificationDate;
        public int crcUncompressed;
        public int compressedSize;
        public int uncompressedSize;
        public short fileNameLength;
        public short extraFieldLength;
        public byte[] firstFileName;
        public byte[] extraInformation;
        public List<string> filesWithin = new List<string>();

        public ZipPlugin(BinaryReader reader)
        {
            FileInfo = new FileInformation();

            using (reader)
            {
                Debug.WriteLine("The length of the file: " + reader.BaseStream.Length);
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    fileHeader = reader.ReadInt32();
                    versionExtract = reader.ReadInt16();
                    generalPurposeBitFlag = reader.ReadInt16();
                    compressionMethod = reader.ReadInt16();
                    fileLastModificationTime = reader.ReadInt16();
                    fileLastModificationDate = reader.ReadInt16();
                    crcUncompressed = reader.ReadInt32();
                    compressedSize = reader.ReadInt32();
                    uncompressedSize = reader.ReadInt32();
                    fileNameLength = reader.ReadInt16();
                    extraFieldLength = reader.ReadInt16();
                    firstFileName = reader.ReadBytes(fileNameLength);
                    extraInformation = reader.ReadBytes(extraFieldLength);
                    compressedData = reader.ReadBytes(compressedSize);

                    var childFile = new FileInformation();
                    childFile.filename = Encoding.Default.GetString(firstFileName);
                    FileInfo.children.Add(childFile);
                }
            }
        }
    }
}
