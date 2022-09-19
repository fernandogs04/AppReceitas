using System.Data;
using AppReceitas.ADONet;
using AppReceitas.Models;
using AppReceitas.ModelsDTO;
using Npgsql;

namespace AppReceitas.DAO
{
    public class DAOReceita
    {
        // Create
        public static void InsereReceitas(Receita receita) {
            using (NpgsqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL =
                "INSERT INTO Receitas (nomereceita, descricao, ingredientes, instrucoes, idusuario) VALUES (@Nome, @Descricao, @Ingredientes, @Instrucoes, @IdUsuario);";

                NpgsqlCommand command = new NpgsqlCommand(comandoSQL, connection);

                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@Nome", receita.Nomereceita);
                command.Parameters.AddWithValue("@Descricao", receita.Descricao);
                command.Parameters.AddWithValue("@Ingredientes", receita.Ingredientes);
                command.Parameters.AddWithValue("@Instrucoes", receita.Instrucoes);
                command.Parameters.AddWithValue("@IdUsuario", receita.Idusuario);

                NpgsqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }

        // Retrieve
        public static List<Receita> GetReceitas(NpgsqlCommand command) {
            List<Receita> listaReceitas = new List<Receita>();

            using (NpgsqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                command.Connection = connection;

                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Receita receita = new Receita()
                    {
                        Idreceita = reader.GetInt32(reader.GetOrdinal("IdReceita")),
                        Nomereceita = reader.GetString(reader.GetOrdinal("NomeReceita")),
                        Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                        Ingredientes = reader.GetString(reader.GetOrdinal("Ingredientes")),
                        Instrucoes = reader.GetString(reader.GetOrdinal("Instrucoes")),
                        Idusuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    };

                    listaReceitas.Add(receita);
                }

                connection.Close();
            }
            return listaReceitas;
        }

        // Pegar todas receitas
        public static List<Receita> GetReceitas() {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Receitas");

            return GetReceitas(command);
        }

        // Pegar todas receitas de um usuário
        public static List<Receita> GetReceitas(int IdUsuario) {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Receitas WHERE IdUsuario = @Id");

            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@Id", IdUsuario);

            return GetReceitas(command);
        }

        // Pegar uma receita específica
        public static ReceitaDTO GetReceitas(int IdUsuario, int IdReceita) {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Receitas WHERE IdUsuario = @IdUsuario AND IdReceita = @IdReceita");

            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
            command.Parameters.AddWithValue("@IdReceita", IdReceita);

            return GetReceitas(command) // Pegar receitas do usuario
                .Select(x => new ReceitaDTO(x)) // Transformar para DTO
                .FirstOrDefault(); // Transformar para apenas um objeto;
        }

        // Update
        public static void UpdateReceita(int IdReceita, ReceitaDTO receitaNova) {
            using (NpgsqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL = @"UPDATE Receitas 
                                      SET nomeReceita  = @Nome,
                                          descricao    = @Descricao,
                                          ingredientes = @Ingredientes,
                                          instrucoes   = @Instruções
                                      WHERE IdReceita  = @IdReceita";

                NpgsqlCommand command = new NpgsqlCommand(comandoSQL, connection);

                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@Nome", receitaNova.Nomereceita);
                command.Parameters.AddWithValue("@Descricao", receitaNova.Descricao);
                command.Parameters.AddWithValue("@Ingredientes", receitaNova.Ingredientes);
                command.Parameters.AddWithValue("@Instruções", receitaNova.Instrucoes);
                command.Parameters.AddWithValue("@IdReceita", IdReceita);

                NpgsqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }

        // Delete
        public static void DeleteReceita(int IdReceita) {
            using (NpgsqlConnection connection = ConnectionFactory.GetConnection())
            {
                connection.Open();

                string comandoSQL = @"DELETE FROM Receitas WHERE IdReceita = @IdReceita";

                NpgsqlCommand command = new NpgsqlCommand(comandoSQL, connection);

                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@IdReceita", IdReceita);

                NpgsqlDataReader reader = command.ExecuteReader();
                connection.Close();
            }
        }
    }
}