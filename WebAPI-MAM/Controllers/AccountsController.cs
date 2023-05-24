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
using WebAPI_MAM.Validators;
using Microsoft.EntityFrameworkCore;

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
                Doctors doctorDTO = new Doctors();

                doctorDTO.Name = credencialsUser.Name;
                doctorDTO.Mail = credencialsUser.Email;
                doctorDTO.password = credencialsUser.Password;
                doctorDTO.Role = credencialsUser.Role;

                var doctor = mapper.Map<Doctors>(doctorDTO);
                dbContext.Add(doctor);
                await dbContext.SaveChangesAsync();

                DoctorPass doctorPass = new DoctorPass();
                doctorPass.Email = credencialsUser.Email;
                doctorPass.Password = credencialsUser.Password;

                if(doctorDTO.Role == "doctor")
                {
                    var userDoctor = await userManager.FindByEmailAsync(doctorDTO.Mail);
                    await userManager.AddClaimAsync(userDoctor, new Claim("IsDoctor", "1"));
                }

                //Se retorna el Jwt (Json Web Token) especifica el formato del token que hay que devolverle a los clientes
                return await ConstruirToken(doctorPass);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //-----------------------------------
        [HttpPost("login")]
        public async Task<ActionResult<ResponseAutentication>> Login(DoctorPass doctorPass)
        {

            var doctor = await userManager.FindByNameAsync(doctorPass.Email);
            if (doctor == null)
            {
                return NotFound("Esta cuenta no existe");
            }
            var doctorEmail = doctor.Email;
            var result = await signInManager.PasswordSignInAsync(doctorEmail,
                doctorPass.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await ConstruirToken(doctorPass);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ResponseAutentication>> Renovar(UpAdminDTO upAdminDTO)
        {
            //var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var emailClaim = await userManager.FindByNameAsync(upAdminDTO.Email);
            var email = emailClaim.Email;

            var doctorPass = new DoctorPass()
            {
                Email = email
            };

            return await ConstruirToken(doctorPass);

        }

        private async Task<ResponseAutentication> ConstruirToken(DoctorPass doctorPass)
        {
            //Informacion del usuario en la cual podemos confiar
            //En los claim se pueden declarar cualquier variable, sin embargo, no debemos de declarar informacion
            //del cliente sensible como pudiera ser una Tarjeta de Credito o contraseña

            var claims = new List<Claim>
            {
                new Claim("email", doctorPass.Email),
               // new Claim("claimprueba", "Este es un claim de prueba")
            };

            var usuario = await userManager.FindByEmailAsync(doctorPass.Email);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsDoctor")]
        public async Task<ActionResult> HacerAdmin(UpAdminDTO upAdminDTO)
        {

            var exist = await dbContext.Doctors.AnyAsync(x => x.Mail == upAdminDTO.Email);
            if(!exist)
            {
                return NotFound("Este usuario no existe");
            }

            var aplicationDoctor = await dbContext.Doctors.FirstOrDefaultAsync(x => x.Mail == upAdminDTO.Email);
            aplicationDoctor.Role = "doctor";
            dbContext.Update(aplicationDoctor);
            await dbContext.SaveChangesAsync();

            var usuario = await userManager.FindByEmailAsync(upAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("IsDoctor", "1"));

            return NoContent();
        }

        [HttpPost("RemoverDoctor")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsDoctor")]
        public async Task<ActionResult> RemoverAdmin(UpAdminDTO upAdminDTO)
        {

            var exist = await dbContext.Doctors.AnyAsync(x => x.Mail == upAdminDTO.Email);
            if (!exist)
            {
                return NotFound("Este usuario no existe");
            }

            var aplicationDoctor = await dbContext.Doctors.FirstOrDefaultAsync(x => x.Mail == upAdminDTO.Email);
            aplicationDoctor.Role = "paciente";
            dbContext.Update(aplicationDoctor);
            await dbContext.SaveChangesAsync();

            var usuario = await userManager.FindByEmailAsync(upAdminDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("IsDoctor", "1"));

            return NoContent();


        }
    }
}
