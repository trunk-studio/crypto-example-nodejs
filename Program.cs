using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private const string AesKey = @"5TGB&YHN7UJM(IK<";
        private const string AesIV = @"!QAZ2WSX#EDC4RFV";

        static void Main(string[] args)
        {
            string text = "1607312359AB01;2016-07-14T08:53:34.589Z";

            var encrypted = Encryption.Encrypt(text);

            Console.WriteLine("authToken=" + encrypted);
            Console.WriteLine("decrypted=" + Encryption.Decrypt(encrypted));

            Console.Read();
        }

        public static class Encryption
        {
            public static string Encrypt(string text)
            {
                AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
                provider.BlockSize = 128;
                provider.KeySize = 128;
                provider.Key = Encoding.UTF8.GetBytes(AesKey);
                provider.IV = Encoding.UTF8.GetBytes(AesIV);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;

                byte[] src = Encoding.UTF8.GetBytes(text);

                using (ICryptoTransform encrypt = provider.CreateEncryptor())
                {
                    byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                    return Convert.ToBase64String(dest);
                }
            }

            public static string Decrypt(string encryptedText)
            {
                byte[] src = Convert.FromBase64String(encryptedText);

                AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
                provider.BlockSize = 128;
                provider.KeySize = 128;
                provider.Key = Encoding.UTF8.GetBytes(AesKey);
                provider.IV = Encoding.UTF8.GetBytes(AesIV);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decrypt = provider.CreateDecryptor())
                {
                    byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                    return Encoding.UTF8.GetString(dest);
                }
            }
        }
    }
}
