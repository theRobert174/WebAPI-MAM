using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPI_MAM.DTO_s.Get;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.DTO_s.Update;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Patients")]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PatientsController> logger;
        private readonly IMapper mapper;

        public PatientsController(ApplicationDbContext context, ILogger<PatientsController> logger, IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet] //Lista de los pacientes
        public async Task<ActionResult<List<GetPatientDTO>>> Get()
        {
            var patients = await dbContext.Patients.Include(p => p.medicInfo).ToListAsync();
            var patientsDTO = mapper.Map<List<GetPatientDTO>>(patients);

            return patientsDTO;
        }
        
        [HttpGet("Get")]
        public async Task<ActionResult<GetPatientDTO>> GetById([FromQuery] int id)
        {
            var pacienteDB = await dbContext.Patients.Include(m => m.medicInfo).Include(p => p.appointments).ThenInclude(ap => ap.diagnostic).FirstOrDefaultAsync(p => p.Id == id);

            if (pacienteDB == null)
            {
                return NotFound($"El paciente con el id: {id} no existe");
            }

            var patientDTO = mapper.Map<GetPatientDTO>(pacienteDB);

            return patientDTO;
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> Post([FromBody] PatientDTO patientDTO)
        {
            //TODO: Comprobaciones pendientes

            var patient = mapper.Map<Patients>(patientDTO);
            dbContext.Add(patient);
            await dbContext.SaveChangesAsync();

            var getPatientDTO = mapper.Map<GetPatientDTO>(patient);
            return Ok(CreatedAtRoute("Patients", new {Id = patient.Id},getPatientDTO));
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpPatientDTO patientDTO, [FromHeader] int id)
        {
            var exist = await dbContext.Patients.AnyAsync(p => p.Id == id);
            if (!exist)
            {
                return NotFound($"Paciente con el id: {id} no existe");
            }

            var patient = await dbContext.Patients.Include(p => p.medicInfo).Include(p => p.appointments).ThenInclude(ap => ap.diagnostic).FirstOrDefaultAsync(p => p.Id == id);
            mapper.Map(patientDTO, patient);

            dbContext.Update(patient);
            await dbContext.SaveChangesAsync();
            return Ok(patient);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromHeader] int id)
        {
            var exist = await dbContext.Patients.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound($"Paceinte no encontrado con el id: {id}");
            }

            var pacienteDB = await dbContext.Patients.Include(p => p.medicInfo).Include(p => p.appointments).ThenInclude(ap => ap.diagnostic).FirstOrDefaultAsync(p => p.Id == id);

            foreach(var appointment in pacienteDB.appointments)
            {
                dbContext.Remove(appointment.diagnostic);
            }
            dbContext.RemoveRange(pacienteDB.appointments);
            dbContext.Remove(pacienteDB.medicInfo);
            dbContext.Remove(pacienteDB);

            await dbContext.SaveChangesAsync();
            return Ok($"El paciente : {pacienteDB} ha sido eliminado");

        }
    }
}
