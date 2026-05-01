using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;
using SQLitePCL;
class Tarefa
{
    public int Usuario_id {get; set;}
    public int Numero_Tarefa {get; set;}
    public String? Nome_Tarefa {get; set;}
    public bool Marcacao_Tarefa {get; set;}
}
class Usuario
{
    public int Id {get; set;}
    public String? Nome {get; set;}
    public String? Senha {get; set;}
}
class Database
{
    private String caminho = Environment.CurrentDirectory + "/UsuariosTarefas.db";
    public void Iniciar_Banco()
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "CREATE TABLE IF NOT EXISTS Tarefas (Usuario_id INTEGER, Numero_Tarefa PRIMARY KEY AUTOINCREMENT DEFAULT 1, Nome_Tarefa TEXT, Marcacao_Tarefa INTEGER DEFAULT 0); CREATE IF NOT EXISTS Usuarios (Id INTEGER PRIMARY KEY AUTOINCREMENT DEFAULT 1, Nome TEXT UNIQUE, Senha TEXT);";
        Command.ExecuteNonQuery();
        Connection.Close();
    }
    public bool Cadastrar_Usuario_Banco(String nome, String senha)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "INSERT INTO Usuarios (Nome, Senha) VALUES ($nome, $senha);";
        Command.Parameters.AddWithValue("$nome", nome);
        Command.Parameters.AddWithValue("$senha", senha);
        var Executado = Command.ExecuteNonQuery();
        Connection.Close();
        return Executado > 0;
    }
    public Usuario? Logar_Usuario_Banco(String nome, String senha)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Usuarios WHERE Nome = $nome AND Senha = $senha LIMIT 1";
        Command.Parameters.AddWithValue("$nome", nome);
        Command.Parameters.AddWithValue("$senha", senha);
        var reader = Command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }
        return new Usuario
        {
            Id = reader.GetInt32(0),
            Nome = reader.GetString(1),
            Senha = reader.GetString(2)
        };
    }
    public bool Procurar_Usuario_Banco(String nome)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT 1 FROM Usuarios WHERE Nome = $nome LIMIT 1;";
        Command.Parameters.AddWithValue("$nome", nome);
        var Procurado = Command.ExecuteScalar();
        Connection.Close();
        return Procurado != null;
    }
    public bool Remover_Usuario_Banco(String nome, String senha)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "DELETE FROM Usuarios WHERE Nome = $nome AND Senha = $senha LIMIT 1";
        var Executado = Command.ExecuteNonQuery();
        Connection.Close();
        return Executado > 0;
    }
    public bool Criar_Tarefa_Banco(String nome_tarefa, int UsuarioId)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "INSERT INTO Tarefas (Nome_Tarefa, Usuario_id) VALUES ($nome_tarefa, $UsuarioId)";
        Command.Parameters.AddWithValue("$nome_tarefa", nome_tarefa);
        Command.Parameters.AddWithValue("$UsuarioId", UsuarioId);
        var Criar_tarefa_exucutada = Command.ExecuteNonQuery();
        Connection.Close();
        return Criar_tarefa_exucutada > 0;
    }
    public List<Tarefa> Listar_Tarefas_Banco(int UsuarioId)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Tarefas WHERE Usuario_id = $UsuarioId ORDER BY Numero_Tarefa";
        Command.Parameters.AddWithValue("$UsuarioId", UsuarioId);
        var reader = Command.ExecuteReader();
        List<Tarefa> Tarefas = new List<Tarefa>();
        while (reader.Read())
        {
            Tarefa u = new Tarefa
            {
                Nome_Tarefa = reader.GetString(2),
                Numero_Tarefa = reader.GetInt32(1),
                Usuario_id = reader.GetInt32(0),
                Marcacao_Tarefa = reader.GetBoolean(3)
            };
            Tarefas.Add(u);
        }
        return Tarefas;
    }
    public bool Excluir_Tarefa_Banco(int UsuarioId, int Numero_tarefa)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "DELETE FROM Tarefas WHERE Numero_Tarefa = $Numero_tarefa AND Usuario_id = $UsuarioId LIMIT 1";
        Command.Parameters.AddWithValue("$Numero_tarefa", Numero_tarefa);
        Command.Parameters.AddWithValue("$UsuarioId", UsuarioId);
        var Deletou = Command.ExecuteNonQuery();
        Connection.Close();
        return Deletou > 0;
    }
    public bool Marcar_Tarefa_Banco(int MARCACAO, int NUMERACAO, int ID)
    {
        var Connection = new SqliteConnection(caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "UPDATE Tarefas SET Marcacao_Tarefa = $MARCACAO WHERE Numero_Tarefa = $NUMERACAO AND Usuario_id = $ID";
        Command.Parameters.AddWithValue("$MARCACAO", MARCACAO);
        Command.Parameters.AddWithValue("$NUMERACAO", NUMERACAO);
        Command.Parameters.AddWithValue("$ID", ID);
        var Executado = Command.ExecuteNonQuery();
        Connection.Close();
        return Executado > 0;
    }
}
class Program {
    public static void Main(String[] args)
    {
        var db = new Database();
        db.Iniciar_Banco();
        bool Continuar = true;
        while (Continuar)
        {
            System.Console.WriteLine("Olá, você está no programa de tarefas! Escolha uma das opções à seguir: ");
            System.Console.WriteLine("1 - Cadastrar usuário");
            System.Console.WriteLine("2 - Logar usuário");
            System.Console.WriteLine("3 - Remover usuário");
            System.Console.WriteLine("4 - Procurar usuário");
            System.Console.WriteLine("5 - Sair do programa (dados serão salvos!)");
            int resposta = Pegar_Int();
            switch (resposta)
            {
                case 1:
                    Cadastrar_Usuario();
                    break;
                case 2:
                    Logar_Usuario();
                    break;
                case 3:
                    Remover_Usuario();
                    break;
                case 4:
                    Procurar_Usuario();
                    break;
                case 5:
                    Continuar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida! ");
                    break;
            }
        }
    }
    public static String Pegar_String()
    {
        while (true) {
            String Resposta_String = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Resposta_String))
            {
                Console.WriteLine("Ação inválida! ");
                continue;
            }
            return Resposta_String;
        }
    }
    public static int Pegar_Int()
    {
        while (true) {
            String Resposta_String = Console.ReadLine()!;
            if (String.IsNullOrWhiteSpace(Resposta_String) || !int.TryParse(Resposta_String, out int Resposta_Int))
            {
                Console.WriteLine("Ação inválida! ");
                continue;
            }
            return Resposta_Int;
        }
    }
    public static bool Continuar_loop()
    {
        while (true) {
            Console.WriteLine("Deseja continuar? (S/N)");
            String Continuar_loop = Console.ReadLine()!.ToLower();
            if (Continuar_loop == "s" || Continuar_loop == "sim")
            {
                return true;
            }
            else if (Continuar_loop == "n" || Continuar_loop == "não" || Continuar_loop == "nao")
            {
                return false;
            }
            else
            {
                continue;
            }
        }
    }
    public static void Cadastrar_Usuario()
    {
        var db = new Database();
        bool Continuar_Cadastrando = true;
        while (Continuar_Cadastrando)
        {
            Console.Write("Digite o nome do usuário: ");
            String Nome_Cadastrar = Pegar_String();
            Console.Write("Digite a senha do usuário: ");
            String Senha_Cadastrar = Pegar_String();
            var Execucao_Cadastro = db.Cadastrar_Usuario_Banco(Nome_Cadastrar, Senha_Cadastrar);
            if (Execucao_Cadastro)
            {
                Console.WriteLine("O cadastro foi realizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Cadastro falhou!");
            }
            Continuar_Cadastrando = Continuar_loop();
        }
    }
    public static void Logar_Usuario()
    {
        int tentativas = 0;
        var db = new Database();
        bool Continuar_Logando = true;
        while (Continuar_Logando)
        {
            Console.Write("Digite o nome do usuário: ");
            String Nome_Logar = Pegar_String();
            Console.Write("Digite a senha para confirmar: ");
            String Senha_Logar = Pegar_String();
            var Usuario_Logar = db.Logar_Usuario_Banco(Nome_Logar, Senha_Logar);
            if (Usuario_Logar == null)
            {
                Console.WriteLine("Usuário não encontrado!");
                Continuar_Logando = Continuar_loop();
                tentativas++;
                if (tentativas == 3)
                {
                    Console.WriteLine("Limite de tentativas atingidas!");
                    Continuar_Logando = false;
                }
            }
            else
            {
               Console.WriteLine("Você logou no usuário " + Usuario_Logar.Nome + "! "); 
               Console.WriteLine("|------------------------------------|");
               Console.WriteLine("| Usuário:");
               Console.WriteLine("|         *" + Usuario_Logar.Nome);
               Console.WriteLine("|------------------------------------|");
               bool LOGADO = true;
               while (LOGADO)
                {
                    Console.WriteLine("Selecione uma das opções à seguir: ");
                    Console.WriteLine("1 - Criar tarefas");
                    Console.WriteLine("2 - Listar tarefas");
                    Console.WriteLine("3 - Marcar tarefar como concluída");
                    Console.WriteLine("4 - Marcar tarefa como não conclída");
                    Console.WriteLine("5 - Deletar tarefa");
                    Console.WriteLine("6 - Sair da conta");
                    Console.WriteLine("7 - Encerrar programa");
                    int Resposta_Logado = Pegar_Int();
                    switch (Resposta_Logado)
                    {
                        case 1:
                            Criar_Tarefa(Usuario_Logar.Id);
                            break;
                        case 2:
                            Listar_Tarefas(Usuario_Logar.Id);
                            break;
                        case 3:
                            Marcar_Tarefa(Usuario_Logar.Id, 1);
                            break;
                        case 4:
                            Marcar_Tarefa(Usuario_Logar.Id, 0);
                            break;
                        case 5:
                            Deletar_Tarefa(Usuario_Logar.Id);
                            break;
                        case 6:
                            LOGADO = false;
                            break;
                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    public static void Remover_Usuario()
    {
        var db = new Database();
        bool Continuar_Deletando = true;
        while (Continuar_Deletando)
        {
            Console.Write("Digite o nome do usuário: ");
            String Nome_Excluido = Pegar_String();
            Console.Write("Digite a senha para concluir: ");
            String Senha_Excluido = Pegar_String();
            var Execucao_Exclusao = db.Remover_Usuario_Banco(Nome_Excluido, Senha_Excluido);
            if (Execucao_Exclusao)
            {
                Console.WriteLine("Usuário excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Usuário não encontrado!");
            }
            Continuar_Deletando = Continuar_loop();
        }
    }
    public static void Procurar_Usuario()
    {
        var db = new Database();
        bool Continuar_Procurando = true;
        while (Continuar_Procurando) {
            Console.Write("Digite o nome do usuário: ");
            String Nome_Procurado = Pegar_String();
            var Existe = db.Procurar_Usuario_Banco(Nome_Procurado);
            if (Existe)
            {
                Console.WriteLine("Usuário " + Nome_Procurado + " encontrado!");
            }
            else
            {
                Console.WriteLine("Usuário não encontrado!");
            }
            Continuar_Procurando = Continuar_loop();
        }
    }
    public static void Criar_Tarefa(int UsuarioId)
    {
        var db = new Database();
        Console.Write("Digite a tarefa: ");
        String Tarefa_Criada = Pegar_String();
        var Execucao_Criar_Tarefa = db.Criar_Tarefa_Banco(Tarefa_Criada, UsuarioId);
        if (Execucao_Criar_Tarefa)
        {
            Console.WriteLine("Tarefa: " + Tarefa_Criada);
            Console.WriteLine("Criada com sucesso!");
        }
    }
    public static void Listar_Tarefas(int UsuarioId)
    {
        var db = new Database();
        var Lista_Tarefas = db.Listar_Tarefas_Banco(UsuarioId);
        if (Lista_Tarefas.Count == 0)
        {
            Console.WriteLine("Sem tarefas!");
            return;
        }
        int i = 1;
        Console.WriteLine("Tarefas:");
        Console.WriteLine("-- -- -- -- -- -- -- -- -- -- --");
        foreach (var tar in Lista_Tarefas)
        {
            String marcacao;
            if (tar.Marcacao_Tarefa)
            {
                marcacao = "Realizada";
                Console.WriteLine(i + " - " + tar.Nome_Tarefa + " - " + marcacao);
                i++;
            }
            else
            {
                marcacao = "Incompleta";
                Console.WriteLine(i + " - " + tar.Nome_Tarefa + " - " + marcacao);
                i++;
            }
        }
        Console.WriteLine("-- -- -- -- -- -- -- -- -- -- --");
    }
    public static void Marcar_Tarefa(int UsuarioId, int Marcacao_desejada)
    {
        var db = new Database();
        Console.WriteLine("Escolha a tarefa que deseja marcar: ");
        var Tarefas = db.Listar_Tarefas_Banco(UsuarioId);
        if (Tarefas.Count <= 0)
        {
            Console.WriteLine("Sem tarefas!");
            return;
        }
        Console.Write("Digite o número: ");
        int Escolha_Numero = Pegar_Int();
        if (Escolha_Numero < 1 || Escolha_Numero > Tarefas.Count)
        {
            Console.WriteLine("Opção inválida!");
            return;
        }
        var marcado = db.Marcar_Tarefa_Banco(Marcacao_desejada, Escolha_Numero - 1, UsuarioId);
        if (marcado)
        {
            if (Escolha_Numero == 0)
            {
                Console.WriteLine("A tarefa foi marcada como incompleta!");
                return;
            }
            if (Escolha_Numero == 1)
            {
                Console.WriteLine("A tarefa foi marcada como completa!");   
                return;             
            }
        }
    }
    public static void Deletar_Tarefa(int UsuarioId)
    {
        var db = new Database();
        Console.WriteLine("Escolha a tarefa que deseja deletar: ");
        var Tarefas = db.Listar_Tarefas_Banco(UsuarioId);
        if (Tarefas.Count <= 0)
        {
            Console.WriteLine("Sem tarefas!");
            return;
        }
        Console.Write("Digite o número: ");
        int Escolha_Numero = Pegar_Int();
        if (Escolha_Numero < 1 || Escolha_Numero > Tarefas.Count)
        {
            Console.WriteLine("Opção inválida!");
            return;
        }
        var marcado = db.Excluir_Tarefa_Banco(UsuarioId, Escolha_Numero - 1);
        if (marcado)
        {
            Console.WriteLine("Tarefa deletada com sucesso!");
            return;
        }
    }
}