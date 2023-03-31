using System.Text;
using System.Security.Cryptography;

namespace AESEncryption
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            string name = "";
            
            Console.WriteLine("[+] Hello What is your name : ");
            name = Console.ReadLine();
            byte[] nameByte = Encoding.UTF8.GetBytes(name);

            Console.WriteLine($"[+] Hello {name} We are encrypting your name using AES.\n\n");

            byte[] nameByteEncoded = Encrypt.AES_Encrypt(nameByte, "password");

            Console.WriteLine(Encoding.UTF8.GetString(nameByteEncoded));

            Console.WriteLine("\nWe are now decrypting your name \n");
            byte[] nameByteDencoded = Encrypt.AES_Decrypt(nameByteEncoded, "password");
            Console.WriteLine(Encoding.UTF8.GetString(nameByteDencoded));
            Console.ReadKey();
        }
    }
}