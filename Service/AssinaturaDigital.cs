using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Service
{
    public class AssinaturaDigital
    {
        public static string AssinarMensagem(string? mensagem, string? caminhoChavePrivada)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            string mensagemHasheada = ConversorDeMensagem.GerarHash(mensagem);

            byte[] mensagemEmBytes = Encoding.UTF8.GetBytes(mensagem);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(File.ReadAllText(caminhoChavePrivada));

                // Calcular o hash da mensagem manualmente
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashDaMensagem = sha256.ComputeHash(mensagemEmBytes);

                    // Assinar o hash
                    byte[] assinatura = rsa.SignHash(hashDaMensagem, CryptoConfig.MapNameToOID("SHA256"));

                    stopwatch.Stop();
                    Console.WriteLine($"Tempo para assinar a mensagem: {stopwatch.ElapsedMilliseconds} ms");

                    return Convert.ToBase64String(assinatura); // Assinatura base64
                }
            }

            //using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            //{
            //    // Carrega a chave privada
            //    rsa.FromXmlString(File.ReadAllText(caminhoChavePrivada));

            //    // Converte a mensagem em bytes
            //    byte[] mensagemEmBytes = Encoding.UTF8.GetBytes(mensagemHasheada);

            //    // Assina a mensagem usando SHA256
            //    byte[] bytesAssinados = rsa.SignData(mensagemEmBytes, CryptoConfig.MapNameToOID("SHA256"));


            //    stopwatch.Stop();
            //    Console.WriteLine($"Tempo para assinar a mensagem: {stopwatch.ElapsedMilliseconds} ms");
            //    // Retorna a assinatura como uma string base64
            //    return Convert.ToBase64String(bytesAssinados);
            //}
        }

        public static bool VerificarAssinatura( string? caminhoChaveCompartilhada)
        {
            String[] vetorComSeparacao = SepararMensagemEAssinatura();
            string mensagem = vetorComSeparacao[0];
            string assinatura = vetorComSeparacao[1];

            //string mensagemHasheada = ConversorDeMensagem.GerarHash(mensagem);

            //stopwatch.Start();
            //using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            //{
            //    rsa.FromXmlString(File.ReadAllText(caminhoChaveCompartilhada));

            //    byte[] mensagemEmBytes = Encoding.UTF8.GetBytes(mensagemHasheada);
            //    byte[] bytesDeAssinatura = Convert.FromBase64String(assinatura);

            //    stopwatch.Stop();
            //    Console.WriteLine($"Tempo de verificação da assinatura: {stopwatch.ElapsedMilliseconds} ms");

            //    return rsa.VerifyData(mensagemEmBytes, CryptoConfig.MapNameToOID("SHA256"), bytesDeAssinatura);
            //}

            // Iniciar o cronômetro para medir o tempo
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Carregar a chave pública para a verificação
                rsa.FromXmlString(File.ReadAllText(caminhoChaveCompartilhada));

                // Converter a assinatura de base64 para bytes
                byte[] bytesDeAssinatura = Convert.FromBase64String(assinatura);

                // Calcular o hash da mensagem manualmente
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] mensagemEmBytes = Encoding.UTF8.GetBytes(mensagem);
                    byte[] hashDaMensagem = sha256.ComputeHash(mensagemEmBytes);

                    // Parar o cronômetro e exibir o tempo
                    stopwatch.Stop();
                    Console.WriteLine($"Tempo de verificação da assinatura: {stopwatch.ElapsedMilliseconds} ms");

                    // Verificar o hash e a assinatura
                    return rsa.VerifyHash(hashDaMensagem, CryptoConfig.MapNameToOID("SHA256"), bytesDeAssinatura);
                }
            }
        }

        public static String[] SepararMensagemEAssinatura()
        {
            string? mensagemCifradaComAssinatura = ConversorDeMensagem.MensagemDecifrada();

            String[]? parts = mensagemCifradaComAssinatura.Split(new string[] { "<ASSINATURA>" }, StringSplitOptions.None);
            string? mensagemOriginal = parts[0];
            string? assinatura = parts[1];

            return [mensagemOriginal, assinatura];
        }

    }
}
