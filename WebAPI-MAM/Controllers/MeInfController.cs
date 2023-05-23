using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.DTO_s.Update;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/MedicInfo")]
    public class MeInfController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<MeInfController> logger;

        public MeInfController(ApplicationDbContext context, IMapper mapper, ILogger<MeInfController> logger)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet] //Lista de datos medicos
        public async Task<ActionResult<List<MedicInfo>>> Get()
        {
            return await dbContext.MedicInfo.Include(p => p.patient).ToListAsync();

        }

        [HttpGet ("ByIdPatient")] //Lista de datos medicos
        public async Task<ActionResult<List<MedicInfo>>> Get(int id)
        {
            return await dbContext.MedicInfo.Include(p => p.patient).Where(m => m.patientId == id).ToListAsync();

        }

        [HttpGet("ByNss")]
        public async Task<ActionResult<List<MedicInfo>>> GetByNss([FromHeader] string nss)
        {
            return await dbContext.MedicInfo.Where(x=>x.nss==nss).Include(p => p.patient).ThenInclude(p => p.appointments).ToListAsync();

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
        public async Task<ActionResult> Post([FromBody] MedicInfoDTO medicInfoDTO)
        {
             var patientExist = await dbContext.Patients.AnyAsync(x => x.Id == medicInfoDTO.patientId);
            if (!patientExist)
            {
                return BadRequest("No existe paciente en la base de datos con ese Id");
            }
            var medicInfoDb = mapper.Map<MedicInfo>(medicInfoDTO);
            dbContext.Add(medicInfoDb);
            await dbContext.SaveChangesAsync();

            var patients = await dbContext.Patients.FirstOrDefaultAsync(x => x.Id == medicInfoDTO.patientId);

            patients.medicInfoId = medicInfoDb.Id;
            dbContext.Update(patients);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("EditMedicInfo")]
        public async Task<ActionResult> Put([FromBody] MedicInfoDTO medicInfoDTO, int id)
        {
            var exist = await dbContext.MedicInfo.AnyAsync(p => p.Id == id);
            if (!exist)
            {
                return NotFound($"La información medica con el id: {id} no existe");
            }

            var medicInfoDb = mapper.Map<MedicInfo>(medicInfoDTO);
            medicInfoDb.Id = id;
            dbContext.Update(medicInfoDb);

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
