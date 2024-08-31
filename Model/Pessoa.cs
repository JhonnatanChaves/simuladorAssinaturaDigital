using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Model
{
    public class Pessoa
    {
        public string? Nome {  get; set; }
        public string? CaminhoChavePublica { get; set; }
        public string? CaminhoChavePrivada { get; set; }
        public string? CaminhoChaveCompartilhada { get; set; }

        public void ReceberChaveCompartilhada(Pessoa pessoa)
        {
            CaminhoChaveCompartilhada = pessoa.CaminhoChavePublica;
        }

    }
}
