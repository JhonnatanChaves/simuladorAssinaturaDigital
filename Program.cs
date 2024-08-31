// See https://aka.ms/new-console-template for more information


using criptografiaTrabalho02.Model;
using criptografiaTrabalho02.Service;
using criptografiaTrabalho02.Util;

Bob bob = new Bob();
bob.Criar();

Alice alice = new Alice();
alice.Criar();

GeradorDeChaves.GerarChaves(bob);
GeradorDeChaves.GerarChaves(alice);

bob.ReceberChaveCompartilhada(alice);
alice.ReceberChaveCompartilhada(bob);

Pessoa perfil = new Pessoa();
string? nome;
string? opcoesMenu1;
string? opcoesMenu2;
string? mensagem;

do
{
    Console.WriteLine("_______________________________MENU_______________________________");
    Console.WriteLine("1 - Escrever mensagem");
    Console.WriteLine("2 - Ler mensagem");
    Console.WriteLine("3 - Sair");
    opcoesMenu1 = Console.ReadLine();

    Console.WriteLine("Qual perfil você deseja acessar?(Bob ou Alice)");
    nome = Console.ReadLine();

    perfil = (nome == bob.Nome) ? bob : alice;

    if (opcoesMenu1 == "1")
    {
        Console.WriteLine("__________________________________________________________________");

        Console.WriteLine("O que você deseja garantir?");
        Console.WriteLine("1 - Autenticidade");
        Console.WriteLine("2 - Confidencialidade");


        opcoesMenu2 = Console.ReadLine();

        Console.WriteLine("Digite uma mensagem:");
        mensagem = Console.ReadLine();

        if (opcoesMenu2 == "1")
        {
            PilaresDeSI.GarantirAutenticidade(mensagem, perfil);
        }
        else
        {
            PilaresDeSI.GarantirConfidencialidade(mensagem, perfil);
        }
    }

    if (opcoesMenu1 == "2")
    {        


        if(AssinaturaDigital.VerificarAssinatura(perfil.CaminhoChaveCompartilhada) == true)
        {
            Console.WriteLine("A mensagem foi escrita por " + DonoDaChaveCompartilhada.BuscarNome(perfil.CaminhoChaveCompartilhada));
            Console.WriteLine("A última mensagem decifrada do arquivo é : " + AssinaturaDigital.SepararMensagemEAssinatura().First());
        }

    }

} while (opcoesMenu1 != "3");