using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Util
{
    public class DonoDaChaveCompartilhada
    {

        public static string BuscarNome(string? caminhoChaveCompartilhada)
        {
            string? nome = System.IO.Path.GetFileNameWithoutExtension(caminhoChaveCompartilhada);
            string? nomeExtraido = nome.Replace("chavePublica", "");
            
            return nomeExtraido;
        }
    }
}
