using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;
using SQLitePCL;
class Database
{
    private const String caminho = "Data Source=Usuarios.db";
    public void Iniciar_banco()
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = @"CREATE TABLE IF NOT EXISTS usuarios (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nome TEXT UNIQUE, Idade INTEGER, Senha TEXT);";
        Command.ExecuteNonQuery();
        Connection.Close();
    }
    public void Inserir_Usuario(String nome_inserido, int idade_inserida, String senha_inserida)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = @"INSERT INTO Usuarios (Nome, Idade, Senha) VALUES ($nome_inserido, $idade_inserida, $senha_inserida);";
        Command.Parameters.AddWithValue("$nome_inserido", nome_inserido);
        Command.Parameters.AddWithValue("$idade_inserida", idade_inserida);
        Command.Parameters.AddWithValue("$senha_inserida", senha_inserida);
        Command.ExecuteNonQuery();
        Connection.Close();
    }
    public List<Usuario> Listar_Usuarios_banco()
    {
        List<Usuario> lista = new List<Usuario>();
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = @"SELECT * FROM usuarios;";
        var reader = Command.ExecuteReader();
        while (reader.Read())
            {
            Usuario u = new Usuario();
            u.Nome = reader.GetString(1);
            u.Idade = reader.GetInt32(2);
            u.Senha = reader.GetString(3);
            u.Id = reader.GetInt32(0);
            lista.Add(u);
        }
        return lista;
    }
    public bool Procurar_usuario_se_existe(String nome_procurado)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = @"SELECT 1 FROM usuarios WHERE Nome = $nome_procurado LIMIT 1";
        Command.Parameters.AddWithValue("$nome_procurado", nome_procurado);
        var resultado = Command.ExecuteScalar();
        return resultado != null;
    }
    public bool Remover_Banco(String Nome_deletar, int Idade_deletar, String Senha_deletar)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "DELETE FROM usuarios WHERE Nome = $Nome_deletar AND Idade = $Idade_deletar AND Senha = $Senha_deletar LIMIT 1";
        Command.Parameters.AddWithValue("$Nome_deletar", Nome_deletar );
        Command.Parameters.AddWithValue("$Idade_deletar", Idade_deletar);
        Command.Parameters.AddWithValue("$Senha_deletar", Senha_deletar);
        var conferir_delete = Command.ExecuteNonQuery();
        Connection.Close();
        return conferir_delete > 0;
    }
    public Usuario? Logar_Usuario_Banco(String Nome_Logar_Banco, String Senha_Logar_Banco)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM usuarios WHERE Nome = $Nome_Logar_Banco AND Senha = $Senha_Logar_Banco LIMIT 1";
        Command.Parameters.AddWithValue("$Nome_Logar_Banco", Nome_Logar_Banco);
        Command.Parameters.AddWithValue("$Senha_Logar_Banco", Senha_Logar_Banco);
        var Verificar_Login = Command.ExecuteReader();
        Usuario u = null!;
        if (Verificar_Login.Read())
        {
            u = new Usuario
            {
                Nome = Verificar_Login.GetString(1),
                Idade = Verificar_Login.GetInt32(2),
                Senha = Verificar_Login.GetString(3),
                Id = Verificar_Login.GetInt32(0)
            };
        }
        Connection.Close();
        return u;
    }
    public bool Alterar_Nome_Login(String Nome_Velho_Login, String Nome_Novo_Login)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "UPDATE usuarios SET Nome = $Nome_Novo WHERE Nome = $Nome_Velho AND NOT EXISTS (SELECT 1 FROM usuarios WHERE Nome = $Nome_Novo);";
        Command.Parameters.AddWithValue("$Nome_Velho", Nome_Velho_Login);
        Command.Parameters.AddWithValue("$Nome_Novo", Nome_Novo_Login);
        var Linhas_alteradas = Command.ExecuteNonQuery();
        Connection.Close();
        return Linhas_alteradas > 0;
    }
    public bool Alterar_Idade_Login(int Idade_Velha_Login, int Idade_Nova_Login, int Id_Login)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "UPDATE usuarios SET Idade = $Idade_Nova WHERE Idade = $Idade_Velha AND Id = $Id";
        Command.Parameters.AddWithValue("$Idade_Velha", Idade_Velha_Login);
        Command.Parameters.AddWithValue("$Idade_Nova", Idade_Nova_Login);
        Command.Parameters.AddWithValue("$Id", Id_Login);
        var Linhas_alteradas = Command.ExecuteNonQuery();
        Connection.Close();
        return Linhas_alteradas > 0;
    }
    public bool Alterar_Senha_Login(String Senha_Velha_Login, String Senha_Nova_Login, int Id_Login)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "UPDATE usuarios SET Senha = $Senha_Nova WHERE Senha = $Senha_Velha AND Id = $Id_Login";
        Command.Parameters.AddWithValue("$Senha_Velha", Senha_Velha_Login);
        Command.Parameters.AddWithValue("$Senha_Nova", Senha_Nova_Login);
        Command.Parameters.AddWithValue("$Id_Login", Id_Login);
        var Linhas_alteradas = Command.ExecuteNonQuery();
        Connection.Close();
        return Linhas_alteradas > 0;
    }
}
class Usuario
{
    public String? Nome{get; set;}
    public int Idade{get; set;}
    public String? Senha{get; set;}
    public int Id {get; set;}
}
class Program
{
    public static void Main(string[] args) // MAIN  
    {
        var db = new Database();
        db.Iniciar_banco();
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
                    Cadastrar_Usuario();
                    break;
                case 2:
                    Logar_Usuario();
                    break;
                case 3:
                    Listar_Usuarios();
                    break;
                case 4:
                    Listar_Maiores();
                    break;
                case 5:
                    Procurar_Usuario();
                    break;
                case 6:
                    Remover_Usuario();
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
    public static void Cadastrar_Usuario()
    {
        var db = new Database();
        bool Continuar_Cadastrando = true;
        while (Continuar_Cadastrando)
        {
            String Nome_Cadastrado = Perguntar_Nome();
            var usuario_que_ja_existe = db.Procurar_usuario_se_existe(Nome_Cadastrado);
            if (usuario_que_ja_existe) {
                Console.WriteLine("O nome inserido já é possuído por outro usuário, tente outro nome!");
                continue;
            }
            int Idade_Cadastrada = Perguntar_Idade();
            String Senha_Cadastrada = Perguntar_Senha();
            Usuario u = new Usuario();
            u.Nome = Nome_Cadastrado;
            u.Idade = Idade_Cadastrada;
            u.Senha = Senha_Cadastrada;
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
    public static void Logar_Usuario()
    {
        var db = new Database();
        bool Continuar_Logando = true;
        bool Logado;
        while (Continuar_Logando) {   
            String Nome_Logar = Perguntar_Nome();
            String Senha_Logar = Perguntar_Senha();
            var Usuario_Login = db.Logar_Usuario_Banco(Nome_Logar, Senha_Logar);
            if (Usuario_Login == null)
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
                Console.WriteLine("Olá, você logou no usuário " + Nome_Logar + "! ");
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
                            var Nome_Alterado = db.Alterar_Nome_Login(Alterar_Nome_Velho, Alterar_Nome_Novo);
                            if (Nome_Alterado)
                            {
                                Console.WriteLine("Nome alterado com sucesso, seja bem-vindo " + Alterar_Nome_Novo + "!");
                                
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
                            var Idade_Alterada = db.Alterar_Idade_Login(Alterar_Idade_Velha, Alterar_Idade_Nova, Usuario_Login.Id);
                            if (Idade_Alterada)
                            {
                                Console.WriteLine("Idade alterada com sucesso, idade: " + Alterar_Idade_Nova + ". Seja bem-vindo!");

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
                            var Senha_Alterada = db.Alterar_Senha_Login(Alterar_Senha_Velha, Alterar_Senha_Nova, Usuario_Login.Id);
                            if (Senha_Alterada)
                            {
                                Console.WriteLine("Senha alterada com sucesso!");
                                
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
    public static void Listar_Usuarios()
    {
        var db = new Database();
        var verificar_lista = db.Listar_Usuarios_banco();
        if (verificar_lista.Count == 0)
        {
            Console.WriteLine("Lista vazia!");
            return;
        }
        else
        {
            int i = 1;
            Console.WriteLine("Usuários: ");
            foreach (var us in verificar_lista)
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
    public static void Remover_Usuario()
    {
        var db = new Database();
        bool Continuar_Removendo = true;
        while (Continuar_Removendo)
        {
            String Nome_Remover = Perguntar_Nome();
            int Idade_Remover = Perguntar_Idade();
            String Senha_Remover = Perguntar_Senha();
            var foi_removido = db.Remover_Banco(Nome_Remover, Idade_Remover, Senha_Remover);
            if (!foi_removido)
            {
                Console.WriteLine("Usuário inexistente!");
            }
            else
            {
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
    public static void Listar_Maiores()
    {
        var db = new Database();
        var usuarios_maiores = db.Listar_Usuarios_banco().Where(n => n.Idade >= 18);
        if (!usuarios_maiores.Any())
        {
            Console.WriteLine("Lista vazia!");
            return;
        }
        else
        {
            int i = 1;
            Console.WriteLine("Usuários: ");
            foreach (var us_maior in usuarios_maiores)
            {
                Console.WriteLine("-- -- -- -- -- -- -- -- --");
                Console.WriteLine("Usuário " + i + ":");
                Console.WriteLine("Nome: " + us_maior.Nome + "; ");
                Console.WriteLine("Idade: " + us_maior.Idade + "; ");
                Console.WriteLine("-- -- -- -- -- -- -- -- --"); 
                i++;
            }
        }
    }
    public static void Procurar_Usuario()
    {
        bool Continuar_Procurando = true;
        while (Continuar_Procurando) {
            String Nome_Procurado = Perguntar_Nome();
            var db = new Database();
            var UsuarioExiste = db.Procurar_usuario_se_existe(Nome_Procurado);
            if (!UsuarioExiste)
            {
            Console.WriteLine("Usuário inexistente! ");
            }
            else
            {
            Console.WriteLine("O nome digitado pertence à um usuário que existe!");
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
            if (String.IsNullOrWhiteSpace(Perguntar_Idade_Usuario) || !int.TryParse(Perguntar_Idade_Usuario, out int Perguntar_Idade_Usuario_int) || Perguntar_Idade_Usuario_int < 0 || Perguntar_Idade_Usuario_int > 150)
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
                Console.WriteLine("Resposta inválida! ");
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
// LINHA 500 EXATO KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK