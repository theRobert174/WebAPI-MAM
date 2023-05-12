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
        //No se puede agregar diagnosticos desde aqui, se tiene que modificando la cita
        /*[HttpPost]
        public async Task<ActionResult<Diagnosis>> Post([FromBody] Diagnosis diagnosis)
        {
            dbContext.Add(diagnosis);
            await dbContext.SaveChangesAsync();
            return Ok();
        }*/

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Diagnosis diagnosis, [FromHeader] int id)
        {
            if (diagnosis.Id != id)
            {
                return BadRequest("El id del diagnostico no coincide con el proporcionado el los Headers");
            }
            dbContext.Update(diagnosis);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromHeader] int id)
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
