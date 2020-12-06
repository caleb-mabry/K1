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

            try
            {
                using (reader)
                {
                    fileHeader = reader.ReadInt32();
                    Debug.WriteLine(fileHeader.ToString("X"));
                    while (fileHeader != 0x02014b50)
                    {
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
                        // Add Filename
                        if (Encoding.Default.GetString(firstFileName) != "")
                        {
                            Debug.WriteLine(Encoding.Default.GetString(firstFileName));
                            var childFile = new FileInformation();
                            childFile.filename = Encoding.Default.GetString(firstFileName);
                            FileInfo.children.Add(childFile);

                            // I need to see about removing this line. I think it's out of scope
                            filesWithin.Add(Encoding.Default.GetString(firstFileName));

                        }
                        List<int> compressedData = new List<int>();
                        var prevPosition = reader.BaseStream.Position;
                        var potentialData = reader.ReadByte();
                        var foundNewFile = false;
                        while(foundNewFile == false)
                        {
                            if (potentialData == NewFileMagic[0])
                            {
                                reader.BaseStream.Position = prevPosition;
                                fileHeader = reader.ReadInt32();
                                foundNewFile = true;
                            } else
                            {
                                compressedData.Add(potentialData);
                                prevPosition = reader.BaseStream.Position;
                                potentialData = reader.ReadByte();
                            }
                        }

                        //Debug.WriteLine(potentialData.ToString("X"));
                        //while (potentialData != NewFileMagic[0] && foundNewFile == false)
                        //{
                        //    compressedData.Add(potentialData);
                        //    potentialData = reader.ReadByte();
                        //    if (potentialData == NewFileMagic[0])
                        //    {
                        //        reader.BaseStream.Position = prevPosition;
                        //        var checkIfNewFile = reader.ReadInt32();
                        //        Debug.WriteLine(checkIfNewFile.ToString("X"));
                        //        if (checkIfNewFile == 0x04034b50)
                        //        {
                        //            foundNewFile = true;
                        //            fileHeader = checkIfNewFile;
                        //        }
                        //        else
                        //        {

                        //        }
                        //    }
                        //}
                    }

                }
            }
            catch
            {
                Debug.WriteLine("Man this is tough"); 
            }
        }
    }
}
