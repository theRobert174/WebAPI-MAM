using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Patients")]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PatientsController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet] //Lista de los pacientes
        public async Task<ActionResult<List<Patients>>> Get()
        {
            return await dbContext.Patients.Include(m => m.medicInfo).ToListAsync();

        }

        [HttpGet("Get")]
        public async Task<ActionResult<Patients>> GetById([FromQuery] int id)
        {
            var pacienteDB = await dbContext.Patients.Include(m => m.medicInfo).FirstOrDefaultAsync(p => p.Id == id);

            if (pacienteDB == null)
            {
                return NotFound();
            }

            return pacienteDB;
        }

        [HttpPost]
        public async Task<ActionResult<Patients>> Post([FromBody] Patients patients)
        {
            dbContext.Add(patients);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Patients patient, [FromHeader] int id)
        {
            //Si no se introduce un id en el body el valor por defecto es cero, lo mismo sucede con el header id
            if (patient.Id != id || id == 0 || patient.Id == 0)
            {
                return BadRequest();
            }

            var exist = await dbContext.Patients.AnyAsync(p => p.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            var pacienteDB = await dbContext.Patients.Include(m => m.medicInfo).FirstOrDefaultAsync(p => p.Id == id);
                
            Type tipoBody = patient.GetType();
            Type tipoDB = pacienteDB.GetType();

            //Aqui se modifican solo las propiedades existentes en el objeto del body
            foreach(PropertyInfo prop in tipoBody.GetProperties())
            {
                PropertyInfo propDB = tipoDB.GetProperty(prop.Name);
                
                if(propDB != null && propDB.CanWrite)
                {
                    propDB.SetValue(pacienteDB, prop.GetValue(patient));
                }
            }

            dbContext.Update(pacienteDB);
            await dbContext.SaveChangesAsync();
            //This does not work as it should be
            return Ok($"Se Guardo el nuevo paciente {pacienteDB.Id} {pacienteDB.name} {pacienteDB.password} {pacienteDB.mail} {pacienteDB.cel} {pacienteDB.phone} {pacienteDB.medicInfo.Id} {pacienteDB.medicInfoId} {pacienteDB.medicInfo.nss} {pacienteDB.medicInfo.weight} {pacienteDB.medicInfo.height} {pacienteDB.medicInfo.sicknessHistory}");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromHeader] int id)
        {
            var exist = await dbContext.Patients.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound($"existe {exist} id {id}");
            }

            var pacienteDB = await dbContext.Patients.Include(m => m.medicInfo).FirstOrDefaultAsync(p => p.Id == id);

            if(pacienteDB.medicInfo != null)
            {
                dbContext.Remove(pacienteDB.medicInfo);
            }

            dbContext.Remove(new Patients()
            {
                Id = id,
            });

            await dbContext.SaveChangesAsync();
            return Ok($"El paciente : {pacienteDB} ha sido eliminado");

        }
    }
}
