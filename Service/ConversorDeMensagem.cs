using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Service
{
    public class ConversorDeMensagem
    {
        private static readonly byte[] chave = Encoding.UTF8.GetBytes("segurancacifradachavesecreta1234");
        private static readonly byte[] vetor = Encoding.UTF8.GetBytes("vetorseguranca16");

        public static string Cifrar(string textoSimples)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = chave;
                aes.IV = vetor;

                ICryptoTransform cifrador = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, cifrador, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(textoSimples);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decifrar(string textoCifrado)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = chave;
                aes.IV = vetor;

                ICryptoTransform decifrador = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(textoCifrado)))
                using (CryptoStream cs = new CryptoStream(ms, decifrador, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
