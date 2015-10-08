using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace F1Tippspiel.Db.Tools
{
    public class Hasher
    {
        /// <summary>
        /// Generates a simple MD5-Hash
        /// </summary>
        /// <param name="wert"></param>
        /// <returns></returns>
        public static string GenerateMD5(String wert)
        {
            byte[] bWert = Encoding.UTF8.GetBytes(wert);
            MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bWert);
            string md5Wert = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return md5Wert;
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}
