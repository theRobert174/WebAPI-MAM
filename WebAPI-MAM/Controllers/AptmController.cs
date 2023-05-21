using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebAPI_MAM.DTO_s.Get;
using WebAPI_MAM.DTO_s.Set;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Appointments")]

    public class AptmController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AptmController(ApplicationDbContext context,  IMapper mapper)
        {
            this.dbContext = context;
            this.mapper=mapper;
        }

        [HttpGet] //Lista de todas las citas
        public async Task<ActionResult<List<GetAptmDTO>>> Get()
        {
           var aptm= await dbContext.Appointments.Include(x=>x.doctor).Include(x => x.patient).ToListAsync();
            return mapper.Map<List<GetAptmDTO>>(aptm);

        }
        [HttpGet("DcotorIdAndDate")]//Lista de todas las citas en x dia
        public async Task<ActionResult<List<GetAptmDTO>>> GetIdDate([FromHeader]int Doctorid, [FromHeader] string date)
        {
            var parsedDate = DateTime.Parse(date);

            var InicioDiA = new DateTime(parsedDate.Year, parsedDate.Month, parsedDate.Day, 0, 0, 1);
            var FinalDia = new DateTime(parsedDate.Year, parsedDate.Month, parsedDate.Day, 23, 59, 59);

            var aptm = await dbContext.Appointments.Where(x => x.Date > InicioDiA && x.Date < FinalDia && x.doctorId==Doctorid).Include(x => x.doctor).Include(x => x.patient).ToListAsync();
            return mapper.Map<List<GetAptmDTO>>(aptm);

        }

        [HttpGet("DoctorIdPatientNameDate")] //BUSCAR POR NOMBRE ID MEDICO Y FECHA
        public async Task<ActionResult<GetAptmDTO>> GetporID(int idMedico, string nombrePaciente, DateTime fecha)
        {
            // PARA VALIDAR COSAS CON DOCUTORIES USA AUTHORIZA IS DOCTOR O CHECA SI TIENE EL CLAIM DOCTOR
            //var DOCTORClaim = HttpContext.User.Claims.Where(claim => claim.Type == "IsDoctor").FirstOrDefault();

            //var email = DoctorClaim.Value;
            //var usuario = await userManager.FindByEmailAsync(email);

            var aptm = await dbContext.Appointments.Where(x=> x.doctorId==idMedico && x.patient.name==nombrePaciente && x.Date==fecha)
                .Include(x => x.doctor).Include(x => x.patient).FirstOrDefaultAsync();
            return mapper.Map<GetAptmDTO>(aptm);

        }


        //[AllowAnonymous]
        [HttpGet("{PatientId:int}")]
        public async Task<ActionResult<List<GetAptmDTO>>> GetByIdEventReminder(int PatientId)
        {

            var PatientExists = await dbContext.Appointments.AnyAsync(x => x.Id == PatientId);
            if (!PatientExists)
            {
                return BadRequest("No existe paciente");
            }

            var diaDespues = DateTime.Now.AddDays(1);
            var hoy = DateTime.Now;

            var UserEvents = await dbContext.Appointments.Where(x => x.patientId == PatientId && x.Date < diaDespues && x.Date > hoy).ToListAsync();

            return mapper.Map<List<GetAptmDTO>>(UserEvents);

        }

        [HttpPost("NewAppointment")]
        public async Task<ActionResult> Post([FromBody] AptmDTO aptmDTO)
        {

            //Una disculpa por los comentarios pero solo así entenderé paso a paso lo que hago 
           
                // Verificar si el doctor existe
                var docExists = await dbContext.Doctors.AnyAsync(x => x.Id == aptmDTO.doctorId);
                if (!docExists)
                {
                    return BadRequest("No existen doctores en la base de datos con ese Id");
                }
                // Verificar si el paciente existe
                var PatientExists = await dbContext.Patients.AnyAsync(x => x.Id == aptmDTO.PatientId);
                if (!PatientExists)
                {
                    return BadRequest("No existen Pacientes en la base de datos con ese Id");
                }

            //Verificar que no este ocupada la hora y dia
            var CitaOcupada = await dbContext.Appointments.AnyAsync(x => x.Date == aptmDTO.Date);
            if (!CitaOcupada)
            {
                return BadRequest("Cita ocupada");
            }

            //CREACION DE CITA
            var appointment = mapper.Map<Appointments>(aptmDTO);
                dbContext.Add(appointment);
                await dbContext.SaveChangesAsync();
                return Ok();
            
        }




        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AptmDTO aptmDTO, [FromHeader] int id)
        {

            // Verificar si el cita existe
            var aptExists = await dbContext.Appointments.AnyAsync(x => x.Id == id);
            if (!aptExists)
            {
                return BadRequest("No existe cita en la base de datos con ese Id");
            }
            // Verificar si el doctor existe
            var docExists = await dbContext.Doctors.AnyAsync(x => x.Id == aptmDTO.doctorId);
            if (!docExists)
            {
                return BadRequest("No existen doctores en la base de datos con ese Id");
            }
            // Verificar si el paciente existe
            var PatientExists = await dbContext.Patients.AnyAsync(x => x.Id == aptmDTO.PatientId);
            if (!PatientExists)
            {
                return BadRequest("No existen Pacientes en la base de datos con ese Id");
            }
            var CitaOcupada = await dbContext.Appointments.AnyAsync(x => x.Date == aptmDTO.Date);
            if (!CitaOcupada)
            {
                return BadRequest("Cita ocupada");
            }


            //MODIFICACION DE CITA

            var appointment = mapper.Map<Appointments>(aptmDTO);
            appointment.Id = id;
            dbContext.Update(appointment);

            await dbContext.SaveChangesAsync();


            
            
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromHeader] int id)
        {
            var exist = await dbContext.Appointments.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Appointments()
            {
                Id = id,
            });

            await dbContext.SaveChangesAsync();
            return Ok();

        }

    }

}