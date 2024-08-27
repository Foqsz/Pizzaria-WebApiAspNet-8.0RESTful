using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using StackExchange.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.API.Controllers
{
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

        [HttpGet]
        public ActionResult<string> Get()
        {
            return " << Controlador UsuariosController :: WebApiUsuarios >> ";
        }

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

        [HttpPost("Login")]
        public async Task<ActionResult<UserTokenModel>> Login([FromBody] UserInfoModel userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildAndStoreToken(userInfo);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "login inválido.");
                return BadRequest(ModelState);
            }
        }

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
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddMinutes(5);

            JwtSecurityToken token = new JwtSecurityToken(
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
    }
}
