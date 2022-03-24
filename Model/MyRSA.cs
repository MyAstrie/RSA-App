using System;
using System.Text;
using System.Security.Cryptography;
using System.Windows;

namespace RSA_App.Model
{
    public class RsaCrypt
    {
        public int KeyValue { get; set; }

        public (string, string?) GenerateKeyPair()
        {
            using var rsa = new RSACryptoServiceProvider(KeyValue);
            try
            {
                var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey()) ?? 
                                 throw new ArgumentNullException("Convert.ToBase64String(rsa.ExportRSAPrivateKey())");
                var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey()) ?? 
                                throw new ArgumentNullException("Convert.ToBase64String(rsa.ExportRSAPublicKey())");

                return (privateKey, publicKey);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        public string? Encrypt(string plainText, string publicKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            try
            {
                rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out int bytesRead);

                var data = Encoding.Unicode.GetBytes(plainText);
                var cipher = rsa.Encrypt(data, false);

                return Convert.ToBase64String(cipher);
            }
            catch (FormatException)
            {
                MessageBox.Show("Исключение формата");
                return null;
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Криптографическое исключение");
                return null;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        public string? Decrypt(string cipherText, string privateKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            try
            {
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out int bytesRead);

                var dataBytes = Convert.FromBase64String(cipherText);
                var plainText = rsa.Decrypt(dataBytes, false);

                return Encoding.Unicode.GetString(plainText);
            }
            catch (FormatException)
            {
                MessageBox.Show("Исключение формата");
                return null;
            }
            catch (CryptographicException)
            {
                MessageBox.Show("Не удалось расшифровать сообщение. \n " +
                                "Проверьте приватный ключ");
                return null;
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }
    }
}