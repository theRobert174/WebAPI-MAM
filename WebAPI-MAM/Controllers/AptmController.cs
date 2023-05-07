using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Appointments")]

    public class AptmCtrl : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AptmCtrl(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet] //Lista de todas las citas
        public async Task<ActionResult<List<Appointments>>> Get()
        {
            return await dbContext.Appointments.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult> Post(Appointments appointments)
        {
            dbContext.Add(appointments);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Appointments appointments, int id)
        {
            if (appointments.Id != id)
            {
                return BadRequest("El id de la cita no coincide en la URL");
            }
            dbContext.Update(appointments);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
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