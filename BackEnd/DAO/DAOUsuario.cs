using System.Data;
using AppReceitas.ADONet;
using AppReceitas.Models;
using AppReceitas.ModelsDTO;
using Npgsql;

namespace AppReceitas.DAO
{
    public class DAOUsuario
    {
        public static List<Usuario> GetUsuarios() {
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (NpgsqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Usuarios", connection);

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        Idusuario = reader.GetInt32(reader.GetOrdinal("Idusuario")),
                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                        Senha = reader.GetString(reader.GetOrdinal("Senha"))
                    };

                    listaUsuarios.Add(usuario);
                }

                connection.Close();
            }
            return listaUsuarios;
        }
        
        public static void InsereUsuario(UsuarioDTO usuario) {
            using (NpgsqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL =
                "INSERT INTO Usuarios (nome, senha) VALUES (@Nome, @Senha);";

                NpgsqlCommand command = new NpgsqlCommand(comandoSQL, connection);

                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);

                NpgsqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }
    }
}