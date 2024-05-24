using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Cryptography
{
    public static class RSAKeyGenerator
    {
        public static void GenerateKeys(out string publicKey, out string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
            }
        }
    }
}
