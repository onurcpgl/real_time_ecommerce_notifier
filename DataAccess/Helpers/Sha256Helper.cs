using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public class Sha256Helper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // SHA256 nesnesini oluştur
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Veriyi byte dizisine dönüştür
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Byte dizisini hex string'e dönüştür
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
