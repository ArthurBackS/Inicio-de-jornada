using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
class Usuario
{
    public String Nome{get; set;}
    public int Idade{get; set;}
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
    public static void Main(string[] args) // MAIN  
    {
        List<Usuario> Usuarios = new List<Usuario>();
        bool Continuar = true;
        while (Continuar)
        {
            Console.WriteLine("Selecione uma das opções a seguir(por número): ");
            Console.WriteLine("1 - Cadastro");
            Console.WriteLine("2 - Logar em usuário");
            Console.WriteLine("3 - Listar usuários");
            Console.WriteLine("4 - Listar maiores de idade");
            Console.WriteLine("5 - Procurar por usuário");
            Console.WriteLine("6 - Remover usuário");
            Console.WriteLine("7 - Sair");
            int Resposta_oficial = Perguntar_Resposta();
            switch (Resposta_oficial)
            {
                case 1:
                    Cadastrar_Usuario(Usuarios);
                    break;
                case 2:
                    Logar_Usuario(Usuarios);
                    break;
                case 3:
                    Listar_Usuarios(Usuarios);
                    break;
                case 4:
                    Listar_Maiores(Usuarios);
                    break;
                case 5:
                    Procurar_Usuario(Usuarios);
                    break;
                case 6:
                    Remover_Usuario(Usuarios);
                    break;
                case 7:
                    Console.WriteLine("Obrigado por contar conosco!");
                    Continuar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }
    public static void Cadastrar_Usuario(List<Usuario> Usuarios)
    {
        bool Continuar_Cadastrando = true;
        while (Continuar_Cadastrando)
        {
            String Nome_Cadastrado = Perguntar_Nome();
            int Idade_Cadastrado = Perguntar_Idade();
            String Senha_Cadastrado = Perguntar_Senha();
            Usuarios.Add(new Usuario(Nome_Cadastrado, Idade_Cadastrado, Senha_Cadastrado));
            Console.WriteLine("Usuário cadastrado! ");
            if (!Continuar_no_Loop())
            {
                Continuar_Cadastrando = false;
            }
            else
            {
                continue;
            }
        }
    }
    public static void Logar_Usuario(List<Usuario> Usuarios)
    {
        bool Continuar_Logando = true;
        bool Logado;
        while (Continuar_Logando) {   
            String Nome_Logar = Perguntar_Nome();
            int Idade_Logar = Perguntar_Idade();
            String Senha_Logar = Perguntar_Senha();
            var Usuario_logar = Usuarios.FirstOrDefault(n => n.Nome == Nome_Logar && n.Idade == Idade_Logar && n.Senha == Senha_Logar);
            if (Usuario_logar == null)
            {
                Console.WriteLine("Usuário não encontrado! ");
                bool Continuar_Logar = Continuar_no_Loop();
                if (!Continuar_Logar)
                {
                    Continuar_Logando = false;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                Console.WriteLine("Olá, você logou no usuário " + Usuario_logar.Nome + " de " + Usuario_logar.Idade + " anos de idade! ");
                Logado = true;
                while (Logado)
                {
                    Console.WriteLine("Selecione um dos números a seguir: ");
                    Console.WriteLine("1 - Alterar nome");
                    Console.WriteLine("2 - Alterar idade");
                    Console.WriteLine("3 - Alterar senha");
                    Console.WriteLine("4 - Sair");
                    int Resposta_Logado = Perguntar_Resposta();
                    switch (Resposta_Logado)
                    {
                        case 1:
                            Console.WriteLine("Digite o novo nome de usuário");
                            String Alterar_Nome_Novo = Perguntar_Nome();
                            Console.WriteLine("Digite o seu nome de usuário antiga para confirmar ");
                            String Alterar_Nome_Velho = Perguntar_Nome();
                            if (Alterar_Nome_Velho == Usuario_logar.Nome)
                            {
                                Console.WriteLine("Nome alterado com sucesso, seja bem vindo " + Usuario_logar.Nome + "!");
                                Usuario_logar.Nome = Alterar_Nome_Novo;
                            }
                            else
                            {
                                Console.WriteLine("Nome incorreto!");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Digite sua nova idade de usuário");
                            int Alterar_Idade_Nova = Perguntar_Idade();
                            Console.WriteLine("Digite a sua idade de usuário antiga para confirmar ");
                            int Alterar_Idade_Velha = Perguntar_Idade();
                            if (Alterar_Idade_Velha == Usuario_logar.Idade)
                            {
                                Console.WriteLine("Idade alterada com sucesso, seja bem vindo " + Usuario_logar.Nome + " de " + Usuario_logar.Idade + " anos!");
                                Usuario_logar.Idade = Alterar_Idade_Nova;
                            }
                            else
                            {
                                Console.WriteLine("Idade incorreta!");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Digite sua nova senha de usuário");
                            String Alterar_Senha_Nova = Perguntar_Senha();
                            Console.WriteLine("Digite o sua senha usuário antiga para confirmar ");
                            String Alterar_Senha_Velha = Perguntar_Senha();
                            if (Alterar_Senha_Velha == Usuario_logar.Senha)
                            {
                                Console.WriteLine("Senha alterada com sucesso!");
                                Usuario_logar.Senha = Alterar_Senha_Nova;
                            }
                            else
                            {
                                Console.WriteLine("Senha incorreta!");
                            }
                            break;
                        case 4:
                            Logado = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida! ");
                            break;
                    }
                }
            }
        }
    }
    public static void Listar_Usuarios(List<Usuario> Usuarios)
    {
        if (Usuarios.Count == 0)
        {
            Console.WriteLine("Lista vazia! ");
        }
        else
        {
            int i = 1;
            foreach (var us in Usuarios)
            {
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                Console.WriteLine("Usuário " + i + ":");
                Console.WriteLine("Nome: " + us.Nome + "; ");
                Console.WriteLine("Idade: " + us.Idade + "; ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                i++;
            }
        }
    }
    public static void Remover_Usuario(List<Usuario> Usuarios)
    {
        bool Continuar_Removendo = true;
        while (Continuar_Removendo)
        {
            String Nome_Remover = Perguntar_Nome();
            int Idade_Remover = Perguntar_Idade();
            String Senha_Remover = Perguntar_Senha();
            var Removido = Usuarios.FirstOrDefault(n => n.Nome == Nome_Remover && n.Idade == Idade_Remover && n.Senha == Senha_Remover);
            if (Removido == null)
            {
                Console.WriteLine("Usuário inexistente!");
            }
            else
            {
                Usuarios.Remove(Removido);
                Console.WriteLine("Usuário removido! ");
                if (!Continuar_no_Loop())
                {
                    Continuar_Removendo = false;
                }
                else
                {
                    continue;
                }
            }
        }
    }
    public static void Listar_Maiores(List<Usuario> Usuarios)
    {
        var Maiores = Usuarios.Where(u => u.Idade >= 18);
        if (!Maiores.Any())
        {
            Console.WriteLine("Lista vazia! ");
        }
        else
        {
            int i = 1;
            foreach (var us_maior in Maiores)
            {
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                Console.WriteLine("Usuário " + i + ":");
                Console.WriteLine("Nome: " + us_maior.Nome + "; ");
                Console.WriteLine("Idade: " + us_maior.Idade + "; ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --"); 
            }
        }
    }
    public static void Procurar_Usuario(List<Usuario> Usuarios)
    {
        bool Continuar_Procurando = true;
        while (Continuar_Procurando) {
            String Nome_Procurado = Perguntar_Nome();
            int Idade_Procurado = Perguntar_Idade();
            var Procurado = Usuarios.FirstOrDefault(n => n.Nome == Nome_Procurado && n.Idade == Idade_Procurado);
            if (Procurado == null)
            {
            Console.WriteLine("Usuário inexistente! ");
            }
            else
            {
            Console.WriteLine("Usuário " + Procurado.Nome + " de " + Procurado.Idade + " anos, existe!");
            Continuar_Procurando = Continuar_no_Loop();
            }
        }
    } 
    public static String Perguntar_Nome()
    {
        while (true) {
            Console.Write("Digite o nome do usuário: ");
            String Perguntar_Nome_Usuario = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Perguntar_Nome_Usuario))
            {
                Console.WriteLine("Nome inválido! ");
                Console.WriteLine("Tente novamente! ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                continue;
            }
            else
            {
                return Perguntar_Nome_Usuario;
            }
        }
    }
    public static int Perguntar_Idade()
    {
        while (true) {
            Console.Write("Digite a idade do usuário: ");
            String Perguntar_Idade_Usuario = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Perguntar_Idade_Usuario) || !int.TryParse(Perguntar_Idade_Usuario, out int Perguntar_Idade_Usuario_int) || Perguntar_Idade_Usuario_int < 0)
            {
                Console.WriteLine("Idade inválida! ");
                Console.WriteLine("Tente novamente! ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                continue;
            }
            else
            {
                return Perguntar_Idade_Usuario_int;
            }
        }
    }
    public static String Perguntar_Senha()
    {
        while (true) {
            Console.Write("Digite a senha do usuário: ");
            String Perguntar_Senha_Usuario = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Perguntar_Senha_Usuario))
            {
                Console.WriteLine("Senha inválida! ");
                Console.WriteLine("Tente novamente! ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                continue;
            }
            else
            {
                return Perguntar_Senha_Usuario;
            }
        }
    }
    public static bool Continuar_no_Loop()
    {
        while (true) {
            Console.WriteLine("Deseja continuar? (S/N)");
            String CONTINUAR_LOOP = Console.ReadLine()!.ToLower();
            if (CONTINUAR_LOOP == "sim" || CONTINUAR_LOOP == "si" || CONTINUAR_LOOP == "s")
            {
                return true;
            }
            else if (CONTINUAR_LOOP == "n" || CONTINUAR_LOOP == "nao" || CONTINUAR_LOOP == "no" || CONTINUAR_LOOP == "not" || CONTINUAR_LOOP == "não")
            {
                return false;
            }
            else
            {
                continue;
            }
        }
    }
    public static int Perguntar_Resposta()
    {
        while (true) {
            Console.Write("Resposta: ");
            String Perguntar_Resposta_Usuario = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Perguntar_Resposta_Usuario) || !int.TryParse(Perguntar_Resposta_Usuario, out int Perguntar_Resposta_Usuario_int) || Perguntar_Resposta_Usuario_int < 0)
            {
                Console.WriteLine("Idade inválida! ");
                Console.WriteLine("Tente novamente! ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                continue;
            }
            else
            {
                return Perguntar_Resposta_Usuario_int;
            }
        }
    }
}