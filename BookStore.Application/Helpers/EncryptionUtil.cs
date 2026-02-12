using System;
using System.Security.Cryptography;
using System.Text;

namespace BookStore.Application.Helpers
{
    public static class EncryptionUtil
    {
        private static readonly String _password = "senha";

        public static string Encrypt(string message)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using var hashProvider = MD5.Create();
            using var tdesAlgorithm = TripleDES.Create();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(_password));
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            var dataToEncrypt = utf8.GetBytes(message);
            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            return Convert.ToBase64String(results);
        }

        public static string ChangeUrlSpecialCharset(string value)
        {
            return value.Replace("~", "010")
                .Replace("`", "020")
                .Replace("@", "030")
                .Replace("$", "040")
                .Replace("%", "050")
                .Replace("^", "060")
                .Replace("&", "070")
                .Replace("(", "080")
                .Replace(")", "090")
                .Replace("-", "0100")
                .Replace("+", "0110")
                .Replace("|", "0120")
                .Replace("\\", "0130")
                .Replace("}", "0140")
                .Replace("{", "0150")
                .Replace(":", "0160")
                .Replace(".", "0170")
                .Replace("?", "0180")
                .Replace(",", "0190")
                .Replace(">", "0200")
                .Replace("<", "0210")
                .Replace("/", "0220")
                .Replace("\'", "0230")
                .Replace("-", "0240")
                .Replace("_", "0250")
                .Replace("=", "0260")
                .Replace("*", "0270")
                .Replace(";", "0280")
                .Replace("\"", "0290")
                .Replace("!", "0300")
                .Replace(" ", "0310")
                .Replace("#", "0320")
                .Replace("'", "0330");
        }
    }
}
