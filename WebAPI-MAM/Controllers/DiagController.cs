using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Diagnosis")]
    public class DiagController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DiagController(ApplicationDbContext context,  IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;

        }

        [HttpGet] //Lista de los doctores
        public async Task<ActionResult<List<Diagnosis>>> Get()
        {
            return await dbContext.Diagnosis.ToListAsync();

        }
        

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DiagnosisDTO diagnosisDTO, [FromHeader] int idCita)
        {
            var APTExists = await dbContext.Appointments.AnyAsync(x => x.Id == idCita);
            if (!APTExists)
            {
                return BadRequest("No existe cita en la base de datos con ese Id");
            }
            //
            var diagnosisDB = mapper.Map<Diagnosis>(diagnosisDTO);
            dbContext.Add(diagnosisDB);
            await dbContext.SaveChangesAsync();

            var Apointment = await dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == idCita);

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
