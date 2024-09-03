using System;
using System.Collections.Generic;
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
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Carrega a chave privada
                rsa.FromXmlString(File.ReadAllText(caminhoChavePrivada));

                // Converte a mensagem em bytes
                byte[] mensagemEmBytes = Encoding.UTF8.GetBytes(mensagem);

                // Assina a mensagem usando SHA256
                byte[] bytesAssinados = rsa.SignData(mensagemEmBytes, CryptoConfig.MapNameToOID("SHA256"));

                // Retorna a assinatura como uma string base64
                return Convert.ToBase64String(bytesAssinados);
            }
        }

        public static bool VerificarAssinatura( string? caminhoChaveCompartilhada)
        {

            String[] vetorComSeparacao = SepararMensagemEAssinatura();
            string mensagem = vetorComSeparacao[0];
            string assinatura = vetorComSeparacao[1];

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(File.ReadAllText(caminhoChaveCompartilhada));

                byte[] mensagemEmBytes = Encoding.UTF8.GetBytes(mensagem);
                byte[] bytesDeAssinatura = Convert.FromBase64String(assinatura);

                return rsa.VerifyData(mensagemEmBytes, CryptoConfig.MapNameToOID("SHA256"), bytesDeAssinatura);
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
