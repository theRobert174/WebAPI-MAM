using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Azure.Identity;
using WebAPI_MAM.DTO_s.Get;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.DTO_s.Update;
using WebAPI_MAM;
using WebAPI_MAM.Entities;

namespace WebApiAlumnosSeg.Controllers
{
    [ApiController]
    [Route("Accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IMapper mapper;

        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext dbContext, IMapper mapper)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<ResponseAutentication>> Registrar(CredencialsUser credencialsUser)
        {
            var user = new IdentityUser { UserName = credencialsUser.Email, Email = credencialsUser.Email };
            var result = await userManager.CreateAsync(user, credencialsUser.Password);

            if (result.Succeeded)
            {
                //Se retorna el Jwt (Json Web Token) especifica el formato del token que hay que devolverle a los clientes
                return await ConstruirToken(credencialsUser);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseAutentication>> Login(CredencialsUser credencialsUser)
        {
            var result = await signInManager.PasswordSignInAsync(credencialsUser.Email,
                credencialsUser.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await ConstruirToken(credencialsUser);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseAutentication>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var credenciales = new CredencialsUser()
            {
                Email = email
            };

            return await ConstruirToken(credenciales);

        }

        private async Task<ResponseAutentication> ConstruirToken(CredencialsUser credencialsUser)
        {
            //Informacion del usuario en la cual podemos confiar
            //En los claim se pueden declarar cualquier variable, sin embargo, no debemos de declarar informacion
            //del cliente sensible como pudiera ser una Tarjeta de Credito o contraseña

            var claims = new List<Claim>
            {
                new Claim("email", credencialsUser.Email),
                new Claim("claimprueba", "Este es un claim de prueba")
            };

            var usuario = await userManager.FindByEmailAsync(credencialsUser.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new ResponseAutentication()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }

        [HttpPost("HacerDoctor")]
        public async Task<ActionResult> HacerAdmin(UpAdminDTO upAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(upAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("IsDoctor", "1"));

            return NoContent();
        }

        [HttpPost("RemoverDoctor")]
        public async Task<ActionResult> RemoverAdmin(UpAdminDTO upAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(upAdminDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("IsDoctor", "1"));

            return NoContent();
        }
    }
}
