using criptografiaTrabalho02.Model;
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

        public static string CifrarComChavePublica(string? mensagem, string? caminhoChavePublica)
        {
            byte[] dados = Encoding.UTF8.GetBytes(mensagem);
            string chavePublica = File.ReadAllText(caminhoChavePublica);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(chavePublica);

                // Cifra os dados com a chave pública
                byte[] dadosCifrados = rsa.Encrypt(dados, false);

                // Converte os dados cifrados para base64 para facilitar o armazenamento/transmissão
                return Convert.ToBase64String(dadosCifrados);
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

        public static string DecifrarComChavePrivada(Pessoa? pessoa)
        {
            string? chavePrivada = File.ReadAllText(pessoa.CaminhoChavePrivada);

            // Lê a mensagem cifrada do arquivo
            string? ultimaMensagemCifrada = ManipulacaoDeArquivo.LerUltimaMensagemDoArquivo();

            byte[] dadosCifrados = Convert.FromBase64String(ultimaMensagemCifrada);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(chavePrivada);

                // Decifra os dados com a chave privada
                byte[] dadosDecifrados = rsa.Decrypt(dadosCifrados, false);

                return Encoding.UTF8.GetString(dadosDecifrados);
            }

        }

        public static string? MensagemDecifrada()
        {

            string? ultimaMensagemCifrada = ManipulacaoDeArquivo.LerUltimaMensagemDoArquivo();

            if (ultimaMensagemCifrada != null)
            {
                string mensagemDecifrada = ConversorDeMensagem.Decifrar(ultimaMensagemCifrada);
                return mensagemDecifrada;
            }

            return null;
        }
    }
}
