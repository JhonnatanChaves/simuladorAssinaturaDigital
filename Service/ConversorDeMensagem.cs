using criptografiaTrabalho02.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(chavePublica);

                // Cifra os dados com a chave pública
                byte[] dadosCifrados = rsa.Encrypt(dados, false);

                stopwatch.Stop();
                Console.WriteLine($"Tempo de cifração com chave pública: {stopwatch.ElapsedMilliseconds} ms");
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Lê a mensagem cifrada do arquivo
            string? ultimaMensagemCifrada = ManipulacaoDeArquivo.LerUltimaMensagemDoArquivo();

            byte[] dadosCifrados = Convert.FromBase64String(ultimaMensagemCifrada);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(chavePrivada);

                // Decifra os dados com a chave privada
                byte[] dadosDecifrados = rsa.Decrypt(dadosCifrados, false);
                stopwatch.Stop();
                Console.WriteLine($"Tempo de decifração com chave privada: {stopwatch.ElapsedMilliseconds} ms");

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

        public static string GerarHash(string mensagem)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            using (SHA256 sha256Hash = SHA256.Create())
            {
            
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(mensagem));
            
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Formato hexadecimal
                }
                stopwatch.Stop();
                Console.WriteLine($"Tempo para gerar o hash da mensagem: {stopwatch.ElapsedMilliseconds} ms");

                return builder.ToString();
            }
        }
    }
}
