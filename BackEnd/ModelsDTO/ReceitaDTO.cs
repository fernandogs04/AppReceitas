using AppReceitas.Models;

namespace AppReceitas.ModelsDTO
{
    public class ReceitaDTO
    {
        public ReceitaDTO()
        {
        }

        public ReceitaDTO(Receita receita)
        {
            (Idreceita,         Nomereceita,         Ingredientes,         Instrucoes,         Descricao) =
            (receita.Idreceita, receita.Nomereceita, receita.Ingredientes, receita.Instrucoes, receita.Descricao);
        }

        public int Idreceita { get; set; }
        public string Nomereceita { get; set; }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public string Instrucoes { get; set; }
    }
}