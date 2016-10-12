using System;
using System.Collections.Generic;
using System.Text;
//using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace BasculaGaribay
{
    class CompressUtils
    {
        public static String Compress(String sTextToCompress)
        {
            String sCompressedData = "";
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(sTextToCompress);
                MemoryStream ms = new MemoryStream();
                using(GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(buffer, 0, buffer.Length);
                }
                ms.Position = 0;
                MemoryStream msOutStream = new MemoryStream();
                byte[] compressed = new byte[ms.Length];
                ms.Read(compressed, 0, compressed.Length);
                byte[] gzBuffer = new byte[compressed.Length + 4];
                System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
                System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
                sCompressedData = Convert.ToBase64String(gzBuffer);
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("Error comprimiendo datos EX: " + ex.Message);
            }
            return sCompressedData;
        }
        public static String Decompress(String sCompressedText)
        {
            String sResultText = "";
            if (sCompressedText.Length > 0)
            {
                try
                {
                    byte[] gzBuffer = Convert.FromBase64String(sCompressedText);
                    using(MemoryStream ms = new MemoryStream())
                    {
                        int iMsgLength = BitConverter.ToInt32(gzBuffer, 0);
                        ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                        byte[] buffer = new byte[iMsgLength];
                        ms.Position = 0;
                        using(GZipStream zip = new GZipStream(ms,CompressionMode.Decompress))
                        {
                            zip.Read(buffer, 0, buffer.Length);
                        }
                        sResultText = Encoding.UTF8.GetString(buffer);
                    }
                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            return sResultText;
        }
    }
}
