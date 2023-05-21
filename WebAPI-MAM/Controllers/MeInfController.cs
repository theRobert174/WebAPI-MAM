using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/MedicInfo")]
    public class MeInfController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public MeInfController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet] //Lista de datos medicos
        public async Task<ActionResult<List<MedicInfo>>> Get()
        {
            return await dbContext.MedicInfo.Include(p => p.patient).ToListAsync();

        }

        [HttpGet("ByNss")]
        public async Task<ActionResult<List<MedicInfo>>> GetByNss([FromHeader] string nss)
        {
            return await dbContext.MedicInfo.Where(x=>x.nss==nss).Include(p => p.patient).ToListAsync();

        }

        //No se puede agregar datos medicos desde aqui, tiene que ser desde la creacion de un paciente
        /*[HttpPost]
        public async Task<ActionResult<MedicInfo>> Post(MedicInfo medicInfo)
        {
            dbContext.Add(medicInfo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }*/

        [HttpPost]
        public async Task<ActionResult<Diagnosis>> Post([FromBody] MedicInfoDTO medicInfo, [FromHeader] int PaitientId )
        {
            var APTExists = await dbContext.Patients.AnyAsync(x => x.Id == PaitientId);
            if (!APTExists)
            {
                return BadRequest("No existe cita en la base de datos con ese Id");
            }
            var medicInfoDB = mapper.Map<Diagnosis>(medicInfo);
            dbContext.Add(medicInfoDB);
            await dbContext.SaveChangesAsync();

            var Paciente = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == PaitientId);

            Paciente.medicInfoId = medicInfoDB.Id;
            dbContext.Update(Paciente);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromHeader] int id)
        {
            var exist = await dbContext.MedicInfo.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new MedicInfo()
            {
                Id = id,
            });

            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
