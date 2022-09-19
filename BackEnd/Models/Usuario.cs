using AppReceitas.ModelsDTO;

namespace AppReceitas.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
        }

        public Usuario(UsuarioDTO usuario)
        {
            (Nome,         Senha) =
            (usuario.Nome, usuario.Senha);
        }

        public int Idusuario { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}