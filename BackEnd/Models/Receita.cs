using AppReceitas.ModelsDTO;

namespace AppReceitas.Models
{
    public partial class Receita
    {
        public Receita()
        {
        }

        public Receita(ReceitaDTO receitadto)
        {
            (Idreceita,            Nomereceita,            Ingredientes,            Instrucoes,            Descricao) =
            (receitadto.Idreceita, receitadto.Nomereceita, receitadto.Ingredientes, receitadto.Instrucoes, receitadto.Descricao);
        }

        public int Idreceita { get; set; }
        public string Nomereceita { get; set; }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public string Instrucoes { get; set; }
        public int Idusuario { get; set; }
    }
}