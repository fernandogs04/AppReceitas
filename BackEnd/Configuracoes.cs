using System.Text;
using System.Text.Json;

namespace AppReceitas
{
    public class ConfiguracoesModel
    {
        public string Segredo { get; set; }
        public string LocalBancoDados { get; set; }
    }

    public static class Configuracoes
    {
        private static ConfiguracoesModel GetDadosString()
        {
            string jsonData = File.ReadAllText("./configuracoes.json");
            ConfiguracoesModel ob = JsonSerializer.Deserialize<ConfiguracoesModel>(jsonData);
            return ob;
        }

        public static byte[] GetSegredo()
        {
            ConfiguracoesModel cfgObj = GetDadosString();

            return Encoding.ASCII.GetBytes(cfgObj.Segredo);
        }

        public static string GetLocalBancoDados()
        {
            ConfiguracoesModel cfgObj = GetDadosString();

            return cfgObj.LocalBancoDados;
        }
    }
}