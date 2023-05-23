using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json.Serialization;
using WebAPI_MAM.DTO_s.Get;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.DTO_s.Update;
using WebAPI_MAM.Entities;
using WebAPI_MAM.Validators;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Doctores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsDoctor")]

    public class DocController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<DocController> logger;
        private readonly IMapper mapper;

        public DocController(ApplicationDbContext context, IMapper mapper, ILogger<DocController> logger)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.logger = logger;
        }
        
        //Get-----------------
        [HttpGet("GetAllDoctors")] //Lista de los doctores
        public async Task<ActionResult<List<Doctors>>> GetAll()
        {
            var getDoctor = await dbContext.Doctors.Include(x=> x.appointments).ToListAsync();
            logger.LogInformation("Si se pudo completar el proceso de obtener a los doctores");
            return mapper.Map<List<Doctors>>(getDoctor);

        }

        [HttpGet("GetAllDoctorswithAppointments")]
        public async Task<ActionResult<List<DoctorsDTOconCitas>>> Get()
        {

            var Doctor = await dbContext.Appointments.Where(x => x.Status == "Pendiente" || x.Status == "pendiente").
                Include(x => x.doctor).ToListAsync();

            return mapper.Map<List<DoctorsDTOconCitas>>(Doctor);
        }


        [HttpGet("GetAllAptmMedicDiag")]
        public async Task<ActionResult<List<Appointments>>> GetbyDoctor(int id)
        {

            var doctors = await dbContext.Appointments.Where(x => x.doctorId == id)
                .Include(x => x.doctor)
                .Include(x => x.patient)
                    .ThenInclude(x => x.medicInfo)
                .Include(x => x.diagnostic)
                .ToListAsync();
            return mapper.Map<List<Appointments>>(doctors);
        }

        [HttpGet("AppointmentsbyDate/{date:DateTime}")]
        public async Task<ActionResult<List<DoctorsDTOconCitas>>> GetbyDate(DateTime dateTime)
        {

            var aptm = await dbContext.Appointments.Where(x => x.Date.Date == dateTime.Date)
                .ToListAsync();
            return mapper.Map<List<DoctorsDTOconCitas>>(aptm);
        }

        //Post------------
        [HttpPost("NewDoctor")]
        public async Task<ActionResult> Post([FromBody] DoctorDTO doctordto)
        {
            var doctor = mapper.Map<Doctors>(doctordto);
            dbContext.Add(doctor);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        /* [HttpPost("NewAptm")]
         public async Task<ActionResult> Post([FromBody] AptmDTO aptmDTO)
         {




             var doctorId = await dbContext.Doctors
                 .Where(x => aptmDTO.DocId.Equals(x.Id))
                 .Select(x => x.Id).ToListAsync();

             var patientsId = await dbContext.Patients
                 .Where(x => aptmDTO.DocId.Equals(x.Id))
                 .Select(x => x.Id).ToListAsync();



             var aptm = mapper.Map<Appointments>(aptmDTO);
             dbContext.Add(aptm);
             await dbContext.SaveChangesAsync();
             return Ok();
         }*/

        

        [HttpPost("NewDiagnosticPatient /{id:int}")]
        public async Task<ActionResult> Post([FromBody] DiagnosisDTO diagnosisDTO)
         {
           
                // Crear el mapeado de Diagnosis con el DTO
                var diagnosis = mapper.Map<Diagnosis>(diagnosisDTO);

                // Agregar el nuevo diagnóstico al contexto de la base de datos
                dbContext.Diagnosis.Add(diagnosis);

                // Guardar los cambios en la base de datos
                await dbContext.SaveChangesAsync();

                return Ok(); // Devolver respuesta HTTP 200 (OK) si se creó el diagnóstico exitosamente
            
        }



        //Put--------------

        [HttpPut("UpdatebyId/{id:int}")]
        public async Task<ActionResult> PutDoctor(DoctorDTO doctorDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Doctors.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var doctor = mapper.Map<Doctors>(doctorDTO);
            //doctor.Id = doctor.id;

            dbContext.Update(doctor);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

       /* [HttpPut("UpdatePatient/{id}")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] UpPatientDTO uppatientDTO)
        {
            try
            {
                // Buscar el paciente por su ID
                var patient = await dbContext.Patients.FindAsync(id);
                if (patient == null)
                {
                    return NotFound(); // Devolver respuesta HTTP 404 si el paciente no se encuentra
                }

                // Actualizar las propiedades del paciente con los datos del DTO
                uppatientDTO.name = patient.name;
                uppatientDTO.phone = patient.phone;
                uppatientDTO.mail = patient.mail;
                uppatientDTO.cel = patient.cel;
                uppatientDTO.medicInfo = patient.medicInfo;

                // Guardar los cambios en la base de datos
                await dbContext.SaveChangesAsync();

                return Ok(); // Devolver respuesta HTTP 200 (OK) si se actualizó el paciente exitosamente
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver una respuesta HTTP 500 (Error interno del servidor) en caso de error
                return StatusCode(500, ex.Message);
            }
        }*/


        //Delete------------

        [HttpDelete("DeletebyId/ {id:int}")]
            public async Task<ActionResult> Delete(int id)
            {
                var exist = await dbContext.Doctors.AnyAsync(x => x.Id == id);

                    if(!exist)
                    {
                        return NotFound();
                    }

                dbContext.Remove(new Doctors(){
                        Id = id,
                     });

                await dbContext.SaveChangesAsync();
                return Ok();

             }

        

    }
    
}
