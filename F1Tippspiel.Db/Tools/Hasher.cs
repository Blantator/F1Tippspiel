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
        public static String GenerateMD5(String wert)
        {
            byte[] bWert = Encoding.UTF8.GetBytes(wert);
            MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bWert);
            string md5Wert = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return md5Wert;
        }
    }
}
