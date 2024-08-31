using criptografiaTrabalho02.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Service
{
    public class GeradorDeChaves
    {
        public static void GerarChaves(Pessoa pessoa)
        {
    
            if(pessoa.CaminhoChavePrivada != null && pessoa.CaminhoChavePublica != null)
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
                {
                    // Exporta e salva a chave privada
                    string chavePrivada = rsa.ToXmlString(true);
                    File.WriteAllText(pessoa.CaminhoChavePrivada, chavePrivada);

                    // Exporta e salva a chave pública
                    string chavePublica = rsa.ToXmlString(false);
                    File.WriteAllText(pessoa.CaminhoChavePublica, chavePublica);

                    Console.WriteLine("Par de chaves gerado e salvo com sucesso.");

                }

            }
        }
    }
}
