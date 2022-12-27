using System.Security.Cryptography;
using System.Text;

namespace iForecast.Services.Cypher
{
    public static class HashEngine
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        public static string HashText(string text, string salt)
        {
            byte[] saltByte = Encoding.UTF8.GetBytes(salt);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(text),
                saltByte,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }
        public static bool VerifyMatch(string text, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(text, salt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
