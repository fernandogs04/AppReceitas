using AppReceitas.Models;

namespace AppReceitas.ModelsDTO
{
    public class UsuarioDTO
    {
        public UsuarioDTO()
        {
        }

        public UsuarioDTO(Usuario usuario)
        {
            (Nome,         Senha) =
            (usuario.Nome, usuario.Senha);
        }

        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}