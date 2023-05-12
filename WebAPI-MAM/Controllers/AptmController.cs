using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Appointments")]

    public class AptmController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AptmController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet] //Lista de todas las citas
        public async Task<ActionResult<List<Appointments>>> Get()
        {
            return await dbContext.Appointments.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Appointments appointments)
        {
            dbContext.Add(appointments);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Appointments appointments, [FromHeader] int id)
        {
            if (appointments.Id != id)
            {
                return BadRequest("El id de la cita no coincide con el proporcionado en los Headers");
            }
            dbContext.Update(appointments);
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