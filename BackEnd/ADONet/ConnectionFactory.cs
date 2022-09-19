using Npgsql;

namespace AppReceitas.ADONet
{
    public static class ConnectionFactory
    {
        public static NpgsqlConnection GetConnection() {

            NpgsqlConnection conexaoBD = null;

            try {
                conexaoBD = new NpgsqlConnection(Configuracoes.GetLocalBancoDados());
            }
            catch (NpgsqlException e) {
                System.Console.WriteLine("Connection Factory -", e);
            }
            
            return conexaoBD;
        }
    }
}