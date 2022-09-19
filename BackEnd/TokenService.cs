using Microsoft.IdentityModel.Tokens;
using AppReceitas.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppReceitas
{
    public static class TokenService
    {
        public static string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Idusuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Configuracoes.GetSegredo()), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static int GetIdFromToken(ClaimsPrincipal user)
        {
            return Int32.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}