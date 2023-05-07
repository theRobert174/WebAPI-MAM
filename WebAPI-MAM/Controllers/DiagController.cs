using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/Diagnosis")]
    public class DiagController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public DiagController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet] //Lista de los doctores
        public async Task<ActionResult<List<Diagnosis>>> Get()
        {
            return await dbContext.Diagnosis.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Diagnosis>> Post(Diagnosis diagnosis)
        {
            dbContext.Add(diagnosis);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Diagnosis diagnosis, int id)
        {
            if (diagnosis.Id != id)
            {
                return BadRequest("El id del diagnostico no coincide en la URL");
            }
            dbContext.Update(diagnosis);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
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
