using System.Security.Cryptography;

namespace iForecast.Services.Cypher
{
    public class RsaEncryption
    {
        private RSACryptoServiceProvider rsaService;
        public RsaEncryption()
        {
            rsaService = new RSACryptoServiceProvider();
        }
    }
}
