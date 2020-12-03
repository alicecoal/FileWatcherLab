using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Crypter
    {
        public CrypterOptions Options { get; set; }
        public Crypter(CrypterOptions options)
        {
            this.Options = options;
        }
        public static void Encrypt(string fileName)
        {
            try
            {
                FileStream myStream = new FileStream(fileName, FileMode.OpenOrCreate);

                Aes aes = Aes.Create();

                byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
                byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

                StreamReader sReader = new StreamReader(myStream);
                string fileContain = sReader.ReadToEnd();
                sReader.Close();
                myStream.Close();
                if (fileContain != "")
                {
                    myStream = new FileStream(fileName, FileMode.OpenOrCreate);
                    CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                    StreamWriter sWriter = new StreamWriter(cryptStream);
                    sWriter.WriteLine(fileContain);

                    sWriter.Close();
                    cryptStream.Close();
                    myStream.Close();

                }
            }
            catch
            {
                throw;
            }
        }

        public static void Decrypt(string fileName)
        {
            byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
            try
            {
                FileStream myStream = new FileStream(fileName, FileMode.Open);
                Aes aes = Aes.Create();

                CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);

                StreamReader sReader = new StreamReader(cryptStream);

                string message = sReader.ReadToEnd();
                sReader.Close();
                cryptStream.Close();
                myStream.Close();
                File.WriteAllText(fileName, string.Empty);
                myStream = new FileStream(fileName, FileMode.Open);
                StreamWriter sWriter = new StreamWriter(myStream);
                sWriter.WriteLine(message);
                sWriter.Close();
                myStream.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
