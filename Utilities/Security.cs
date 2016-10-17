using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;

namespace StoragePCL.Utilities
{
    public class Security
    {
        public static readonly byte[] SaltBytes = new byte[] {148,
    152,
    221,
    125,
    100,
    252,
    127,
    93,
    7,
    142,
    12,
    138,
    91,
    37,
    19,
    201};
        // Basic .NET Built-in AES Encryption
        public static string Encrypt(string value)
        {
            string password = Constants.Password;
            //byte[] salt = Encoding.UTF8.GetBytes(Constants.Salt);
            var encrypted = EncryptAes(value, password, SaltBytes);
            return Convert.ToBase64String(encrypted);
           
        }

        public static string Decrypt(string value)
        {
            string password = Constants.Password;
            //byte[] salt = Encoding.UTF8.GetBytes(Constants.Salt);
            var newBytes = Convert.FromBase64String(value);
            var decrypted = DecryptAes(newBytes, password, SaltBytes);
            return decrypted;
        }


        private static byte[] EncryptAes(string data, string password, byte[] salt)
        {
            var key = CreateKey(password, salt);
            var aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var symetricKey = aes.CreateSymmetricKey(key);
            var bytes = WinRTCrypto.CryptographicEngine.Encrypt(symetricKey, Encoding.UTF8.GetBytes(data));
            return bytes;
        }

        private static byte[] CreateKey(string password, byte[] salt, int keyLengthInBytes = 32, int iterations = 1000)
        {
            var key = NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
            return key;
        }
        private static string DecryptAes(byte[] data, string password, byte[] salt)
        {
            byte[] key = CreateKey(password, salt);

            ISymmetricKeyAlgorithmProvider aes = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            ICryptographicKey symetricKey = aes.CreateSymmetricKey(key);
            byte[] bytes=null;
            try
            {
                bytes = WinRTCrypto.CryptographicEngine.Decrypt(symetricKey, data);
            }
            catch (Exception ex)
            {
                return "";
                // do stuff
            }
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] CreateSalt(int lengthInBytes)
        {
            return WinRTCrypto.CryptographicBuffer.GenerateRandom(lengthInBytes);
        }
    }
}
