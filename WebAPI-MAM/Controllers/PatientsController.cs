using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet] //Lista de los doctores
        public async Task<ActionResult<List<Patients>>> Get()
        {
            return await dbContext.Patients.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Patients>> Post(Patients patients)
        {
            dbContext.Add(patients);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Patients patients, int id)
        {
            if (patients.Id != id)
            {
                return BadRequest("El id del paciente no coincide en la URL");
            }
            dbContext.Update(patients);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Patients.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Patients()
            {
                Id = id,
            });

            await dbContext.SaveChangesAsync();
            return Ok();

        }
    }
}
