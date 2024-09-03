using criptografiaTrabalho02.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Service
{
    public class PilaresDeSI
    {
        public static void GarantirAutenticidade(string? mensagem, Pessoa pessoa)
        {
            string assinatura = AssinaturaDigital.AssinarMensagem(mensagem, pessoa.CaminhoChavePrivada);

            string mensagemComAssinatura = mensagem + "<ASSINATURA>" + assinatura;

            string mensagemCifrada = ConversorDeMensagem.Cifrar(mensagemComAssinatura);

            ManipulacaoDeArquivo.SalvarMensagemNoArquivo(mensagemCifrada);
        }

        public static void GarantirConfidencialidade(string? mensagem, Pessoa pessoa)
        {
            string mensagemCifrada = ConversorDeMensagem.CifrarComChavePublica(mensagem, pessoa.CaminhoChaveCompartilhada);
            
            ManipulacaoDeArquivo.SalvarMensagemNoArquivo(mensagemCifrada);
        }
    }
}
