using Microsoft.Data.Sqlite;
using ApiSimples.Models.UsuarioModels;

namespace ApiSimples.Repository.UsuarioRepository;

public class UsuarioRepository {
    private readonly string Caminho = "Data source=Repository/Usuarios.db";
    public bool CriarUsuario(string nome)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "INSERT INTO Usuarios (Nome) VALUES (@nome)";
        Command.Parameters.AddWithValue("@nome", nome);
        int r = Command.ExecuteNonQuery();
        Connection.Close();
        return r == 1;
    }
    public Usuario? ReceberUsuarioNome(string nome)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Usuarios WHERE Nome = @nome LIMIT 1";
        Command.Parameters.AddWithValue("@nome", nome);
        var r = Command.ExecuteReader();
        if (r.Read())
        {
            Usuario u = new Usuario();
            u.Nome = nome;
            u.Id = r.GetInt32(0);
            Connection.Close();
            return u;
        }
        Connection.Close();
        return null;
    }
    public Usuario? ReceberUsuarioId(int id)
    {
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Usuarios WHERE Id = @id LIMIT 1";
        Command.Parameters.AddWithValue("@id", id);
        var r = Command.ExecuteReader();
        if (r.Read())
        {
            Usuario u = new Usuario();
            u.Nome = r.GetString(1);
            u.Id = id;
            Connection.Close();
            return u;
        }
        Connection.Close();
        return null;
    }
    public List<Usuario>? Receber_Usuarios()
    {
        List<Usuario> lista = new();
        SqliteConnection Connection = new SqliteConnection(Caminho);
        Connection.Open();
        var Command = Connection.CreateCommand();
        Command.CommandText = "SELECT * FROM Usuarios ORDER BY Nome";
        var r = Command.ExecuteReader();
        while (r.Read())
        {
            Usuario u = new Usuario();
            u.Id = r.GetInt32(0);
            u.Nome = r.GetString(1);
            lista.Add(u);
        }
        return lista;
    }
    
}