using Microsoft.Data.Sqlite;
using ApiSimples.Models.Entities.UsuarioEntities;
using ApiSimples.Models.DTOs.Request.CriarUsuarioRequest;
using ApiSimples.Models.DTOs.Request.DeletarUsuarioRequest;
using Microsoft.AspNetCore.Mvc;

namespace ApiSimples.Repository.UsuarioRepository;

public class UsuarioRepository {
    private readonly string Caminho = "Data source=Repository/Usuarios.db";
    public bool Criar_Usuario_Repository(Criar_Usuario_Request Criar_Usuario_Request)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        SqliteCommand Command = Connection.CreateCommand();
        Command.CommandText = "INSERT INTO Usuarios (Nome, Senha, Idade) VALUES (@nome, @senha, @idade);"; 
        Command.Parameters.AddWithValue("@nome", Criar_Usuario_Request.Nome);       
        Command.Parameters.AddWithValue("@senha", Criar_Usuario_Request.Senha);       
        Command.Parameters.AddWithValue("@idade", Criar_Usuario_Request.Idade);
        int resultado = Command.ExecuteNonQuery();
        Connection.Close();
        return resultado > 0;
    }
    public bool Deletar_Usuario_Repository(Deletar_Usuario_Request Deletar_Usuario_Request)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        SqliteCommand Command = Connection.CreateCommand();
        Command.CommandText = "DELETE FROM Usuarios WHERE Nome = @nome AND Senha = @senha"; 
        Command.Parameters.AddWithValue("@nome", Deletar_Usuario_Request.Nome);       
        Command.Parameters.AddWithValue("@senha", Deletar_Usuario_Request.Senha);       
        int resultado = Command.ExecuteNonQuery();
        Connection.Close();
        return resultado > 0;
    }
    public UsuarioEntity? Procurar_Usuario_Nome_Repository(String Nome)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        SqliteCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Usuarios WHERE Nome = @nome ORDER BY Nome;";
        Command.Parameters.AddWithValue("@nome", Nome);
        SqliteDataReader leitor = Command.ExecuteReader();
        if (leitor.Read())
        {
            UsuarioEntity? User_Entity = new UsuarioEntity(leitor.GetInt32(0), leitor.GetString(1), leitor.GetString(2), leitor.GetInt16(3));
            return User_Entity;
        }
        Connection.Close();
        return null;
    }
    public UsuarioEntity? Procurar_Usuario_Id_Repository(int Id)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        SqliteCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Usuarios WHERE Id = @id ORDER BY Nome;";
        Command.Parameters.AddWithValue("@id", Id);
        SqliteDataReader leitor = Command.ExecuteReader();
        if (leitor.Read())
        {
            UsuarioEntity? User_Entity = new UsuarioEntity(leitor.GetInt32(0), leitor.GetString(1), leitor.GetString(2), leitor.GetInt16(3));
            return User_Entity;
        }
        Connection.Close();
        return null;
    }
    public List<UsuarioEntity>? Obter_Todos_Usuarios_Repository()
    {
        List<UsuarioEntity> Lista = new();
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        SqliteCommand Command = Connection.CreateCommand();
        Command.CommandText = "SELECT Id, Nome, Senha, Idade FROM Usuarios ORDER BY Nome;";
        SqliteDataReader leitor = Command.ExecuteReader();
        while (leitor.Read())
        {
            UsuarioEntity User_Entity = new UsuarioEntity(
                leitor.GetInt32(0),
                leitor.GetString(1),
                leitor.GetString(2),
                leitor.GetInt32(3));
            Lista.Add(User_Entity);
        }
        Connection.Close();
        return Lista;
    }
}