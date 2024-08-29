using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.IdentityModel.Tokens;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using StackExchange.Redis; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text; 

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUserModel> _userManager;
    private readonly SignInManager<ApplicationUserModel> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IDatabase _redisDatabase;

    public UserController(UserManager<ApplicationUserModel> userManager,
        SignInManager<ApplicationUserModel> signInManager,
        IConfiguration configuration,
        IConnectionMultiplexer redisConnection)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _redisDatabase = redisConnection.GetDatabase();
    }

    //_userManager: Gerencia a criação e manipulação de usuários.
    //_signInManager: Gerencia o processo de autenticação.
    //_configuration: Fornece a chave secreta para gerar tokens JWT.
    //_redisDatabase: Acesso ao banco de dados Redis

    [HttpGet]
    public ActionResult<string> Get()
    {
        return " << Controlador UsuariosController :: WebApiUsuarios >> ";
    }

    #region Criar conta e fornecer token com redis
    [HttpPost("Criar")]
    public async Task<ActionResult<UserTokenModel>> CreateUser([FromBody] UserInfoModel model)
    {
        var user = new ApplicationUserModel { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return await BuildAndStoreToken(model);
        }
        else
        {
            return BadRequest("Usuário ou senha inválidos");
        }
    }
    #endregion

    #region Efetuar login com token e redis
    [HttpPost("Login")]
    public async Task<ActionResult<UserTokenModel>> Login([FromBody] UserInfoModel userInfo)
    {
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
            isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            // Verificar se o token já está armazenado no Redis
            var existingToken = await _redisDatabase.StringGetAsync(userInfo.Email);

            if (existingToken.HasValue)
            {
                // Retornar o token existente
                return new UserTokenModel()
                {
                    Token = existingToken,
                    Expiration = DateTime.UtcNow.AddMinutes(2) // Defina a expiração do token conforme necessário
                };
            }
            else
            {
                // Gerar um novo token e armazená-lo no Redis
                return await BuildAndStoreToken(userInfo); //Armazena através do BuildAndStoreToken
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Login inválido.");
            return BadRequest(ModelState);
        }
    }
    #endregion

    #region Promover cargo Admin
    [HttpPost("AdicionarCargoAdmin")]
    public async Task<IActionResult> AssignAdminRole(string email)
    {
        var roleManagerService = new RoleManagerService(_userManager);
        var success = await roleManagerService.AssignRoleToUserAsync(email, "Admin");

        if (success)
        {
            return Ok("Role 'Admin' atribuída ao usuário com sucesso.");
        }
        else
        {
            return BadRequest("Não foi possível atribuir a role 'Admin' ao usuário.");
        }
    }
    #endregion

    #region Promover cargo User
    [HttpPost("AdicionarCargoUser")]
    public async Task<IActionResult> AssignUserRole(string email)
    {
        var roleManagerService = new RoleManagerService(_userManager);
        var success = await roleManagerService.AssignRoleToUserAsync(email, "User");

        if (success)
        {
            return Ok("Role 'User' atribuída ao usuário com sucesso.");
        }
        else
        {
            return BadRequest("Não foi possível atribuir a role 'User' ao usuário.");
        }
    }
    #endregion

    #region Fornecer token após login ou criação de conta
    private async Task<UserTokenModel> BuildAndStoreToken(UserInfoModel userInfo)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("meuValor", "oque voce quiser"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(2);
        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Armazenar token no Redis com a chave sendo o e-mail do usuário
        await _redisDatabase.StringSetAsync(userInfo.Email, tokenString, expiration - DateTime.UtcNow);

        return new UserTokenModel()
        {
            Token = tokenString,
            Expiration = expiration
        };
    }
    #endregion

}
