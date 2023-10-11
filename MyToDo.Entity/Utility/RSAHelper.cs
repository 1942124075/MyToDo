using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Library.Utility
{
    public class RSAHelper
    {
        public static bool TryGetKeyParameters(string url,bool isPublicKey,out RSAParameters keyParameter)
        {
            keyParameter = default;
            if (string.IsNullOrEmpty(url)) return false;
            string ketPath = Path.Combine( url , (isPublicKey ? "key.public.json":"key.json"));
            if (!File.Exists(ketPath))
            {
                return false;
            }
            keyParameter = JsonConvert.DeserializeObject<RSAParameters>(ketPath);
            return true;
        }
        public static RSAParameters GenerateAndSaveKey(string url, bool isPublicKey)
        {
            RSAParameters publicKey, privateKey;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKey = rsa.ExportParameters(true);
                    publicKey = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            File.WriteAllText(Path.Combine(url,"key.public.json"), JsonConvert.SerializeObject(publicKey));
            File.WriteAllText(Path.Combine(url,"key.json"), JsonConvert.SerializeObject(privateKey));
            return isPublicKey ? publicKey : privateKey;

        }


    }
}
