using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPI_MAM.DTO_s.Get;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Diagnosis")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsDoctor")]
    public class DiagController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DiagController(ApplicationDbContext context,  IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;

        }

        [HttpGet] //Lista de los diagnosticos y sus citas
        public async Task<ActionResult<List<GetDiagDTO>>> Get()
        {
            var dia =  await dbContext.Diagnosis.ToListAsync();
            return mapper.Map<List<GetDiagDTO>>(dia);

        }


        [HttpGet ("DiagbyId")] //Lista de los diagnosticos y sus citas
        public async Task<ActionResult<List<GetDiagDTO>>> GetbyId(int id)
        {
            
            var dia = await dbContext.Diagnosis.Where(x => x.Id == id).ToListAsync();
            return mapper.Map<List<GetDiagDTO>>(dia);

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DiagnosisDTO diagnosisDTO)
        {
            var APTExists = await dbContext.Appointments.AnyAsync(x => x.Id == diagnosisDTO.appointmentId);
            if (!APTExists)
            {
                return BadRequest("No existe cita en la base de datos con ese Id");
            }
            //
            var diagnosisDB = mapper.Map<Diagnosis>(diagnosisDTO);
            dbContext.Add(diagnosisDB);
            await dbContext.SaveChangesAsync();

            var Apointment = await dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == diagnosisDTO.appointmentId);

            Apointment.diagId = diagnosisDB.Id;
            dbContext.Update(Apointment);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Diagnosis diagnosis, [FromHeader] int id)
        {
            if (diagnosis.Id != id)
            {
                return BadRequest("El id del diagnostico no coincide con el proporcionado el los Headers");
            }
            dbContext.Update(diagnosis);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromHeader] int id)
        {
            var exist = await dbContext.Diagnosis.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Diagnosis()
            {
                Id = id,
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
