using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Model
{
    public class Alice : Pessoa
    {
        public void Criar()
        {
            Nome = "Alice";
            CaminhoChavePrivada = @"C:\Users\CriptoTrab02\privada\alice\chavePrivada.xml";
            CaminhoChavePublica = @"C:\Users\CriptoTrab02\publica\chavePublicaAlice.xml";
            CaminhoChaveCompartilhada = " ";
        }
    }
}
