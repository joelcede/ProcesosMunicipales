using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace Commons.Cryptography
{
    public static class EncryptionHelper
    {
        //private static readonly string Key = "J10SK4lC0RP_FUNDATION_KEY"; // Debe ser de 16, 24 o 32 bytes para AES-128, AES-192 o AES-256
        //private static readonly string IV = "IV_J10SK4l_FUNDATION_IV";   // Debe ser de 16 bytes
        //esto vale
        private static readonly string EncryptionKey = "J10SK4lC0RP_FUNDATION_KEY_123456";
        private static readonly string IV_Key = "J10SK4lC0RP_FUNDATION_KEY_123456";
        // Clave de 32 bytes (256 bits) para AES-256
        //private static readonly byte[] encryptionKey = Encoding.UTF8.GetBytes("J10SK4lC0RP_FUNDATION_KEY_12345");

        //// IV de 16 bytes (128 bits)
        //private static readonly byte[] iv = Encoding.UTF8.GetBytes("IV_J10SK4l_FUND");

        private static byte[] key = { };
        private static byte[] IV = { };
        //aqui termina
        //private readonly byte[] encryptionKey = Encoding.UTF8.GetBytes("thisisasecretkey1234567890!");
        //private readonly byte[] iv;
        //private static readonly byte[] encryptionKey = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        //private static readonly byte[] iv = Encoding.UTF8.GetBytes("IV_J10SK4l_FUND");

        public static string EncryptString(string plainText)
        {
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                using (Aes aesAlg = Aes.Create())
                {
                    key = Encoding.UTF8.GetBytes(EncryptionKey);
                    IV = Encoding.UTF8.GetBytes(IV_Key);
                    aesAlg.Key = key;
                    aesAlg.IV = IV;
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                        }
                        byte[] encryptedBytes = msEncrypt.ToArray();
                        string encryptedBase64 = Convert.ToBase64String(encryptedBytes);
                        var xd = encryptedBase64.Length;
                        // Comprimir el resultado para reducir la longitud
                        string compressedBase64 = CompressString(encryptedBase64);
                        return compressedBase64.Length > 100 ? compressedBase64.Substring(0, 100) : compressedBase64;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
                Console.WriteLine($"Error encriptando: {ex.Message}");
                return string.Empty;
            }
        }

        public static string DecryptString(string compressedBase64)
        {
            try
            {
                string decompressedBase64 = DecompressString(compressedBase64);
                byte[] encryptedBytes = Convert.FromBase64String(decompressedBase64);

                using (Aes aesAlg = Aes.Create())
                {
                    key = Encoding.UTF8.GetBytes(EncryptionKey);
                    IV = Encoding.UTF8.GetBytes(IV_Key);
                    aesAlg.Key = key;
                    aesAlg.IV = IV;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
                Console.WriteLine($"Error desencriptando: {ex.Message}");
                return string.Empty;
            }
        }
        //public static string EncryptString(string plainText)
        //{
        //    try
        //    {
        //        using (Aes aesAlg = Aes.Create())
        //        {
        //            key = Encoding.UTF8.GetBytes(EncryptionKey);
        //            IV = Encoding.UTF8.GetBytes(IV_Key);
        //            aesAlg.Key = key;
        //            aesAlg.IV = IV;
        //            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //            using (MemoryStream msEncrypt = new MemoryStream())
        //            {
        //                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //                {
        //                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                    {
        //                        var text = plainText;
        //                        for (int i = 0; i < 5; i++)
        //                        {
        //                            swEncrypt.Write(plainText);
        //                        }

        //                    }
        //                }
        //                return Convert.ToBase64String(msEncrypt.ToArray());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar excepción
        //        Console.WriteLine($"Error encriptando: {ex.Message}");
        //        return string.Empty;
        //    }
        //}

        //public static string DecryptString(string cipherText)
        //{
        //    try
        //    {
        //        byte[] fullCipher = Convert.FromBase64String(cipherText);
        //        using (Aes aesAlg = Aes.Create())
        //        {
        //            key = Encoding.UTF8.GetBytes(EncryptionKey);
        //            IV = Encoding.UTF8.GetBytes(IV_Key);
        //            aesAlg.Key = key;
        //            aesAlg.IV = IV;
        //            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //            using (MemoryStream msDecrypt = new MemoryStream(fullCipher))
        //            {
        //                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //                {
        //                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                    {
        //                        return srDecrypt.ReadToEnd();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar excepción
        //        Console.WriteLine($"Error desencriptando: {ex.Message}");
        //        return string.Empty;
        //    }
        //}

        private static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    zip.Write(buffer, 0, buffer.Length);
                }
                ms.Position = 0;
                byte[] compressed = new byte[ms.Length];
                ms.Read(compressed, 0, compressed.Length);

                byte[] gzBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
                return Convert.ToBase64String(gzBuffer);
            }
        }

        private static string DecompressString(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return Encoding.UTF8.GetString(buffer);
            }
        }
    }

    //public static string EncryptString(string plainText)
    //{
    //    string returnstring = "";
    //    try
    //    {
    //        key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
    //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
    //        Byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);
    //        MemoryStream ms = new MemoryStream();
    //        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
    //        cs.Write(inputByteArray, 0, inputByteArray.Length);
    //        cs.FlushFinalBlock();
    //        returnstring = Convert.ToBase64String(ms.ToArray());


    //        return returnstring;
    //    }
    //    catch (Exception ex)
    //    {
    //        return "";
    //    }

    //}

    //public static string DecryptString(string cipherText)
    //{
    //    Byte[] inputByteArray = new Byte[cipherText.Length];
    //    try
    //    {
    //        key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
    //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
    //        inputByteArray = Convert.FromBase64String(cipherText);
    //        MemoryStream ms = new MemoryStream();
    //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
    //        cs.Write(inputByteArray, 0, inputByteArray.Length);
    //        cs.FlushFinalBlock();
    //        Encoding encoding = Encoding.UTF8;


    //        return encoding.GetString(ms.ToArray());
    //    }
    //    catch (Exception ex)
    //    {
    //        return "";
    //    }

    //}
}
