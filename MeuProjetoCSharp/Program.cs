using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

class Usuario
{
    public String Nome{get; set;}
    public int Idade{get;set;}
    public String Senha{get; set;}
    public Usuario(String nome, int idade, String senha)
    {
        Nome = nome;
        Idade = idade;
        Senha = senha;
    }
}
class Program
{
    public static void Main() {
        List<Usuario> Usuarios = new List<Usuario>();
        bool Continuar = true;
        while (Continuar)
        {
            bool continuarmenor = true;
            Console.WriteLine("Selecione uma das opções a seguir(por número): ");
            Console.WriteLine("1 - Cadastrar");
            Console.WriteLine("2 - Entrar em conta");
            Console.WriteLine("3 - Listar usuários existentes");
            Console.WriteLine("4 - Listar maiores de idade");
            Console.WriteLine("5 - Remover usuário");
            Console.WriteLine("6 - Procurar usuário");
            Console.WriteLine("7 - Sair");
            String Resposta = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Resposta) || !int.TryParse(Resposta, out int respostainteira))
            {
                Console.WriteLine("Resposta inválida! ");
                Console.WriteLine("Tente novamente! ");
                Console.WriteLine("- - - - - - - - - - - -");
                continue;
            }
            if (respostainteira == 1)
            {
                while (continuarmenor) {
                    String NOME;
                    while (true) {
                        Console.WriteLine("Qual seu nome? ");
                        String NOMEINPUT = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(NOMEINPUT))
                        {
                            Console.WriteLine("Nome vazio! ");
                            Console.WriteLine("Tente novamente! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                            continue;
                        }
                        else
                        {
                            NOME = NOMEINPUT;
                            break;
                        }
                    }
                    int IDADECERTA;
                    while (true) {
                        Console.WriteLine("Qual sua idade? ");
                        String IDADE = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(IDADE) || !int.TryParse(IDADE, out int IDADECORRETA) || IDADECORRETA < 0)
                        {
                            Console.WriteLine("Idade vazia ou inválida!");
                            Console.WriteLine("Tente novamente! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                            continue;
                        }
                        else
                        {
                            IDADECERTA = IDADECORRETA;
                            break;
                        }
                    }
                    String SENHA;
                    while (true) {
                        Console.WriteLine("Qual senha será da sua conta? ");
                        String SENHAINPUT = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(SENHAINPUT))
                        {
                            Console.WriteLine("Senha vazia! ");
                            Console.WriteLine("Tente novamente! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                            continue;
                        }
                        else
                        {
                            SENHA = SENHAINPUT;
                            break;
                        }
                    }
                    Usuarios.Add(new Usuario(NOME, IDADECERTA, SENHA));
                    Console.WriteLine("Usuário criado, deseja continuar?(S/N)");
                    String desejo = Console.ReadLine()!.ToLower();
                    if (desejo == "s")
                    {
                        continuarmenor = true;
                    }
                    else
                    {
                        continuarmenor = false;
                    }
                }
            }
            else if (respostainteira == 2)
            {
                bool LOGADO = true;
                String NOMELOGINREAL;
                int IDADELOGINREAL;
                String SENHALOGINREAL;
                while(continuarmenor)
                {
                    Console.Write("Digite seu nome: ");
                    String NOMELOGIN = Console.ReadLine()!;
                    if (String.IsNullOrWhiteSpace(NOMELOGIN))
                    {
                        Console.WriteLine("Nome vazio! ");
                        Console.WriteLine("Tente novamente! ");
                        continue;
                    }
                    NOMELOGINREAL = NOMELOGIN;
                    Console.WriteLine("Qual sua idade? ");
                    String IDADELOGINSTRING = Console.ReadLine()!;
                    if (String.IsNullOrWhiteSpace(IDADELOGINSTRING) || !int.TryParse(IDADELOGINSTRING, out int IDADELOGININT))
                    {
                        Console.WriteLine("Idade inválida! ");
                        Console.WriteLine("Tente novamente! ");
                        Console.WriteLine("- - - - - - - - - - - -");
                        continue;
                    }
                    IDADELOGINREAL = IDADELOGININT;
                    Console.Write("Digite sua senha: ");
                    String SENHALOGIN = Console.ReadLine()!;
                    if (String.IsNullOrWhiteSpace(SENHALOGIN))
                    {
                        Console.WriteLine("Senha vazia!");
                        Console.WriteLine("Tente novamente! ");
                        Console.WriteLine("- - - - - - - - - - - -");
                        continue;
                    }
                    SENHALOGINREAL = SENHALOGIN;
                    var login = Usuarios.FirstOrDefault(n => n.Nome == NOMELOGINREAL && n.Idade == IDADELOGINREAL && n.Senha == SENHALOGINREAL);
                    if (login == null)
                    {
                        LOGADO = false;
                        Console.WriteLine("Usuário não encontrado, deseja continuar mesmo assim? (S/N)");
                        String TENTARLOGAR = Console.ReadLine()!.ToLower();
                        if (TENTARLOGAR == "s")
                        {
                            continuarmenor = true;
                        }
                        else
                        {
                            continuarmenor = false;
                        }
                    }
                    else
                    {
                        LOGADO = true;
                        while (LOGADO)
                        {
                            Console.WriteLine("Você logou na conta " + login.Nome + ", você tem as seguintes opções: ");
                            Console.WriteLine("1 - Alterar nome");
                            Console.WriteLine("2 - Alterar idade");
                            Console.WriteLine("3 - Alteral senha");
                            Console.WriteLine("4 - Sair");
                            String RESPOSTALOGIN = Console.ReadLine()!;
                            if (String.IsNullOrWhiteSpace(RESPOSTALOGIN) || !int.TryParse(RESPOSTALOGIN, out int RESPOSTALOGINCERTA))
                            {
                                Console.WriteLine("Resposta inválida! ");
                                continue;
                            }
                            if (RESPOSTALOGINCERTA == 1)
                            {
                                Console.WriteLine("Qual nome de usuário deseja? ");
                                String NOMELOGADO = Console.ReadLine()!;
                                if (String.IsNullOrWhiteSpace(NOMELOGADO))
                                {
                                    Console.WriteLine("Nome em branco!");
                                    Console.WriteLine("Tente novamente! ");
                                    Console.WriteLine("- - - - - - - - - - - -");
                                }
                                else
                                {
                                    login.Nome = NOMELOGADO;
                                }
                            }
                            else if (RESPOSTALOGINCERTA == 2)
                            {
                               Console.WriteLine("Qual idade de usuário deseja? ");
                                String IDADELOGADO = Console.ReadLine()!;
                                if (String.IsNullOrWhiteSpace(IDADELOGADO) || !int.TryParse(IDADELOGADO, out int IDADELOGADOCERTO) || IDADELOGADOCERTO < 0)
                                {
                                    Console.WriteLine("Idade incorreta!");
                                    Console.WriteLine("Tente novamente! ");
                                    Console.WriteLine("- - - - - - - - - - - -");     
                                }
                                else
                                {
                                    login.Idade = IDADELOGADOCERTO;
                                } 
                            }
                            else if (RESPOSTALOGINCERTA == 3)
                            {
                                Console.WriteLine("Digite a sua nova senha: ");
                                String SENHALOGADO = Console.ReadLine()!;
                                if (String.IsNullOrWhiteSpace(SENHALOGADO))
                                {
                                    Console.WriteLine("Senha vazia!");
                                    Console.WriteLine("Tente novamente!");
                                    Console.WriteLine("- - - - - - - - - - - -");
                                    continue;
                                }
                                Console.Write("Digite sua senha antiga: ");
                                String SENHA_ANTIGA_LOGADO_TENTATIVA = Console.ReadLine()!;
                                if (String.IsNullOrWhiteSpace(SENHA_ANTIGA_LOGADO_TENTATIVA))
                                {
                                    Console.WriteLine("Senha vazia!");
                                    Console.WriteLine("Tente novamente!");
                                    Console.WriteLine("- - - - - - - - - - - -");
                                    continue;
                                }
                                if (SENHA_ANTIGA_LOGADO_TENTATIVA != login.Senha)
                                {
                                    Console.WriteLine("Senha incorreta!");
                                    Console.WriteLine("Tente novamente!");
                                    Console.WriteLine("- - - - - - - - - - - -");
                                    continue;
                                }
                                else
                                {
                                    login.Senha = SENHALOGADO;
                                }
                            }
                            else if (RESPOSTALOGINCERTA == 4)
                            {
                                LOGADO = false;
                            }
                            else
                            {
                                Console.WriteLine("Resposta incorreta! ");
                                continue;
                            }
                        }
                    }
                }
            }
            else if (respostainteira == 3)
            {
                if (Usuarios.Count == 0)
                {
                    Console.WriteLine("A lista está vazia!");
                }
                else
                {
                    foreach (var us in Usuarios)
                    {
                    Console.WriteLine("- - - - - - - - - - - -");
                    Console.WriteLine("Usuário " + us.Nome + ": ");
                    Console.WriteLine("Nome: " + us.Nome);
                    Console.WriteLine("Idade: " + us.Idade);
                    Console.WriteLine("- - - - - - - - - - - -");
                    }
                }
            }
            else if (respostainteira == 4)
            {
                var maioresdeidade = Usuarios.Where(n => n.Idade >= 18);
                foreach (var maiores in maioresdeidade)
                {
                    Console.WriteLine("- - - - - - - - - - - -");
                    Console.WriteLine("Usuário " + maiores.Nome + ": " );
                    Console.WriteLine("Nome: " + maiores.Nome);
                    Console.WriteLine("Idade: " + maiores.Idade);
                    Console.WriteLine("- - - - - - - - - - - -");
                }
            }
            else if (respostainteira == 5)
            {
                while (continuarmenor) {
                    String NOMEREMOVIDOREAL;
                    while (true) {
                        Console.WriteLine("Qual nome do usuário que deseja remover?");
                        String NOMEREMOVIDO = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(NOMEREMOVIDO))
                        {
                            Console.WriteLine("Nome vazio! ");
                            Console.WriteLine("Tente novamente, senão, digite 'cancelar' para sair! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                        }
                        else
                        {
                            NOMEREMOVIDOREAL = NOMEREMOVIDO;
                            break;
                        }
                    }
                    int IDADEREMOVIDOREAL;
                    while (true) {
                        Console.WriteLine("Qual idade do usuário que deseja remover? ");
                        String IDADEREMOVIDO = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(IDADEREMOVIDO) || !int.TryParse(IDADEREMOVIDO, out int IDADEREMOVIDOCONFERIDA) || IDADEREMOVIDOCONFERIDA < 0)
                        {
                            Console.WriteLine("Idade inválida! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                        }
                        else
                        {
                            IDADEREMOVIDOREAL = IDADEREMOVIDOCONFERIDA;
                            break;
                        }
                    }
                    String SENHAREMOVIDOREAL;
                    while (true)
                    {
                        Console.Write("Digite a senha: ");
                        String SENHAREMOVIDO = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(SENHAREMOVIDO))
                        {
                            Console.WriteLine("Senha vazia! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                        }
                        else
                        {
                            SENHAREMOVIDOREAL = SENHAREMOVIDO;
                            break;
                        }
                    }
                    var REMOVIDOAGORA = Usuarios.FirstOrDefault(n => n.Nome == NOMEREMOVIDOREAL && n.Idade == IDADEREMOVIDOREAL && n.Senha == SENHAREMOVIDOREAL);
                    if (REMOVIDOAGORA == null)
                    {
                        Console.WriteLine("Usuário não encontrado! ");
                        Console.WriteLine("- - - - - - - - - - - -");
                    }
                    else
                    {
                        Usuarios.Remove(REMOVIDOAGORA);
                        Console.WriteLine("Usuário " + REMOVIDOAGORA.Nome + " de " + REMOVIDOAGORA.Idade + " anos de idade foi removido com sucesso, deseja continuar removendo usuários? (S/N)");
                        String DESEJO = Console.ReadLine()!.ToLower();
                        if (DESEJO == "s")
                        {
                            continuarmenor = true;
                        }
                        else
                        {
                            continuarmenor = false;
                        }
                    }
                }
            }
            else if (respostainteira == 6)
            {
                while (continuarmenor)
                {
                    String NOMEPROCURADOREAL;
                    while (true)
                    {
                        Console.WriteLine("Qual nome do usuário que deseja procurar? ");
                        String Procurado = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(Procurado))
                        {
                            Console.WriteLine("Nome vazio!");
                            Console.WriteLine("Tente novamente! ");
                            Console.WriteLine("- - - - - - - - - - - -");
                            continue;
                        }
                        else
                        {
                            NOMEPROCURADOREAL = Procurado;
                            break;
                        }
                    }
                    int IDADEPROCURADOREAL;
                    while (true)
                    {
                        Console.WriteLine("Qual idade da pessoa que deseja procurar?");
                        String IDADEPROCURADO = Console.ReadLine()!;
                        if (String.IsNullOrWhiteSpace(IDADEPROCURADO) || !int.TryParse(IDADEPROCURADO, out int IDADEPROCURADOCERTA))
                        {
                            Console.WriteLine("Idade inválida!");
                            Console.WriteLine("Tente novamente!");
                            Console.WriteLine("- - - - - - - - - - - -");
                            continue;
                        }
                        else
                        {
                            IDADEPROCURADOREAL = IDADEPROCURADOCERTA;
                            break;
                        }
                    }
                    var Usuario_certo = Usuarios.FirstOrDefault(n => n.Nome == NOMEPROCURADOREAL && n.Idade == IDADEPROCURADOREAL);
                    if (Usuario_certo == null)
                    {
                        Console.WriteLine("Usuário não existe");
                        Console.WriteLine("Tente novamente! ");
                        Console.WriteLine("- - - - - - - - - - - -");
                        continue;
                    }
                    else
                    {
                      Console.WriteLine("Usuário existe");
                      Console.WriteLine("Deseja pesquisar novamente? (S/N)");
                      String DESEJOSEJO = Console.ReadLine()!.ToLower();
                      if (DESEJOSEJO == "s")
                        {
                            continuarmenor = true;
                        }
                        else
                        {
                            continuarmenor = false;
                        }
                    }
                }
            }
            else if (respostainteira == 7)
            {
                Continuar = false;
            }
            else
            {
                Console.WriteLine("Opção inválida! ");
                Console.WriteLine("- - - - - - - - - - - -");
            }
        }
    }
} // UM A MAIS DO QUE CHUTEI, KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK // voltei e coloquei mais 9