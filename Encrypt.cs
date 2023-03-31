using System.Security.Cryptography;
using System.Text;

namespace AESEncryption
{
    [Obsolete]
    internal class Encrypt
    {
        private const string S = "Str0n6p@55w0rd";  //for debugging purposes

        // Aes encryption is used to encrypt the data
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, string EncodedPassword)
        {
            try
            {

                // make passwordBytes array out of string S
                byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(S));
                //byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(EncodedPassword));

                byte[] encryptedBytes = null;
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                using (MemoryStream ms = new MemoryStream())
                {
                    using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.ANSIX923;
                        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        aes.Clear();
                    }
                    encryptedBytes = ms.ToArray();
                }
                return encryptedBytes;
            }
            catch (System.Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
        // Aes decryption is used to decrypt the data
        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, string EncodedPassword)
        {
            try
            {

                //Console.WriteLine($"decrypting {bytesToBeDecrypted.Length} bytes");
                // make passwordBytes array out of string S
                byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(S));
                //byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(EncodedPassword));

                byte[] decryptedBytes = null;
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                using (MemoryStream ms = new MemoryStream())
                {
                    using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.ANSIX923;
                        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        aes.Clear();
                    }
                    decryptedBytes = ms.ToArray();
                }
                return decryptedBytes;
            }
            catch (System.Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
