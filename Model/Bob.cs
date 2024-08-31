using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace criptografiaTrabalho02.Model
{
    public class Bob : Pessoa
    {
       public void Criar()
       {
            Nome = "Bob";
            CaminhoChavePrivada = @"C:\Users\CriptoTrab02\privada\bob\chavePrivada.xml";
            CaminhoChavePublica = @"C:\Users\CriptoTrab02\publica\chavePublicaBob.xml";
            CaminhoChaveCompartilhada = " ";

        }
    }
}
