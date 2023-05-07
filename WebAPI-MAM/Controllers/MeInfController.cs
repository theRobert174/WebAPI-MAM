using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{
    [ApiController]
    [Route("MAM/MedicInfo")]
    public class MeInfController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public MeInfController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet] //Lista de los doctores
        public async Task<ActionResult<List<MedicInfo>>> Get()
        {
            return await dbContext.MedicInfo.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<MedicInfo>> Post(MedicInfo medicInfo)
        {
            dbContext.Add(medicInfo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(MedicInfo medicInfo, int id)
        {
            if (medicInfo.Id != id)
            {
                return BadRequest("El id de la información médica no coincide en la URL");
            }
            dbContext.Update(medicInfo);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
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
