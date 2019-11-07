using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Battleship_Online.Security
{
    class AES
    {
        /*
         * Programmers: Leonardo Baldazzi, Tommaso Brandinelli
         * Company: OSS inc.
         * Program summary: Simulating Battleship Online via MYSQL Database
         * SubProgram summary: Log-In/Sign-Up user, download '.conf' files, check for updates
         * Class summary: AES/MD5 Security algorythm decrypt/Encrypt
         *  
         * Copyright (c) 2018-19 OSS inc. - All Rights Reserved
         */
        internal static String Decrypt(String s, byte[] key, byte[] IV) //Decript AES function
        {
            String result;

            RijndaelManaged dc = new RijndaelManaged();
            dc.Mode = CipherMode.ECB;
            dc.Padding = PaddingMode.Zeros;

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(s)))
            {
                using (ICryptoTransform decryptor = dc.CreateDecryptor(key, IV))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDecrypt = new StreamReader(csDecrypt))
                        {
                            result = swDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            dc.Clear();

            return result;
        }

        internal static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
