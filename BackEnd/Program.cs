using System.Security.Claims;
using AppReceitas;
using AppReceitas.DAO;
using AppReceitas.Models;
using AppReceitas.ModelsDTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
    });

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Configuracoes.GetSegredo()),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

app.Urls.Add("http://localhost:5062");

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (HttpContext http) => 
{
    return Results.Ok(new {
        message = "Rodando!"
    });
});

app.MapPost("/", (HttpContext http) => 
{
    return Results.Ok(new {
        message = "Rodando!"
    });
});

// Usuario fará login e receberá
// NotFound caso o usuário não seja encontrado
// Ok e o token caso for encontrado
app.MapPost("/login", (Usuario usuario) =>
{
    // Irá procurar o usuario:
    // Caso não encontrar, será nulo
    // Caso encontrar, será o usuário
    Usuario procuraUsuario = DAOUsuario.GetUsuarios().FirstOrDefault(x => x.Nome == usuario.Nome && x.Senha == usuario.Senha);

    // Caso não encontrar, retorne
    if (procuraUsuario == null) return Results.NotFound(new { message = "Usuário ou senha inválidos"});

    // Gerar um token para o usuário
    var token = TokenService.GenerateToken(procuraUsuario);

    // Retornar o token
    return Results.Ok(new
    {
        token
    });
});

// Usuario fará cadastro e receberá
// BadRequest caso os dados estiverem inválidos ou o usuário já estiver ocupado
// Ok e o token caso o cadastro ocorrer com sucesso
app.MapPost("/registrar", (UsuarioDTO usuario) =>
{
    // Os dois campos devem estar preenchidos
    if (usuario.Nome == null || usuario.Senha == null) return Results.BadRequest();

    // Não deixar criar uma conta com o mesmo nome
    if (DAOUsuario.GetUsuarios().Any(x => x.Nome == usuario.Nome)) return Results.BadRequest();

    // Criar o usuário
    DAOUsuario.InsereUsuario(usuario);

    // Pegar o usuário criado com o ID
    Usuario novoUsuario = DAOUsuario.GetUsuarios().FirstOrDefault(x => x.Nome == usuario.Nome && x.Senha == usuario.Senha);

    // Gerar um token para o usuário
    var token = TokenService.GenerateToken(novoUsuario);

    // Retornar o token
    return Results.Ok(new
    {
        token
    });
});

// Verificar a autenticação
app.MapGet("/authenticated", (ClaimsPrincipal user) =>
{ 
    return Results.Ok(new
    {
        //                            Nome do Usuário         Id do Usuário
        message = $"Authenticated as {user.Identity.Name} {TokenService.GetIdFromToken(user)}"
    });
}).RequireAuthorization(); // Apenas usuarios logados podem acessar

// Pegar receitas do usuario logado
app.MapGet("/receitas", (ClaimsPrincipal user) =>
{
    // Pegar o id de usuário do token
    int id = TokenService.GetIdFromToken(user);

    var listaReceitas = DAOReceita.GetReceitas(id) // Pegar receitas do usuario
        .Select(x => new ReceitaDTO(x)) // Transformar para DTO
        .ToList(); // Transformar para List<>

    // Retornar a lista de receitas
    return Results.Ok(new
    {
        listaReceitas
    });
}).RequireAuthorization();

// Usuario Adicionando uma receita
app.MapPost("/receitas", (ClaimsPrincipal user, ReceitaDTO receita) =>
{
    // Criar o novo objeto de receita
    Receita novaReceita = new Receita(receita);
    
    // Pegar id do usuário a partir do jwt
    novaReceita.Idusuario = TokenService.GetIdFromToken(user);

    // Adicionar receita
    DAOReceita.InsereReceitas(novaReceita);

    // Retornar mensagem
    return Results.Ok(new
    {
        message = "Receita criada com sucesso",
        novaReceita
    });
}).RequireAuthorization(); // Apenas usuarios logados podem acessar

// Pegar receita específica do usuario logado
app.MapGet("/receitas/{id}", (ClaimsPrincipal user, int id) =>
{
    // Pegar o id de usuário do token
    int IdUsuario = TokenService.GetIdFromToken(user);

    // Pegar a receita que corresponde ao id do usuário e da receita
    var receita = DAOReceita.GetReceitas(IdUsuario, id);

    return Results.Ok(receita);
}).RequireAuthorization();

// Atualizar receita
app.MapPut("/receitas/{id}", (ClaimsPrincipal user, int id, ReceitaDTO receitaNova) =>
{
    // Atualizar os dados da receita
    DAOReceita.UpdateReceita(id, receitaNova);

    return Results.Ok(receitaNova);
}).RequireAuthorization();

// Deletar receita
app.MapDelete("/receitas/{id}", (ClaimsPrincipal user, int id) =>
{
    // Atualizar os dados da receita
    DAOReceita.DeleteReceita(id);

    return Results.Ok();
}).RequireAuthorization();

app.Run();