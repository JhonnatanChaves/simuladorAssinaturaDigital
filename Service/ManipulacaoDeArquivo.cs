using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Service
{
    public class ManipulacaoDeArquivo
    {
        private static readonly string ArquivoDeTrocaDeMensagens = @"C:\Users\CriptoTrab02\mensagens.txt";

        public static void SalvarMensagemNoArquivo(string mensagemCifrada)
        {
            using (StreamWriter sw = new StreamWriter(ArquivoDeTrocaDeMensagens, true))
            {
                sw.WriteLine(mensagemCifrada);
            }
            Console.WriteLine("A mensagem cifrada foi salva no arquivo!");
        }

        private static string? LerUltimaMensagemDoArquivo()
        {
            if (File.Exists(ArquivoDeTrocaDeMensagens))
            {
                string[] todasAsLinhas = File.ReadAllLines(ArquivoDeTrocaDeMensagens);
                if (todasAsLinhas.Length > 0)
                {
                    return todasAsLinhas[todasAsLinhas.Length - 1];
                }
            }
            return null;
        }
      
        public static string? MensagemDecifrada()
        {

            string? ultimaMensagemCifrada = LerUltimaMensagemDoArquivo();

            if(ultimaMensagemCifrada != null)
            {
                    string mensagemDecifrada = ConversorDeMensagem.Decifrar(ultimaMensagemCifrada);
                    return mensagemDecifrada;
            }

            return null;
        }
    }
}
