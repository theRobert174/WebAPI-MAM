using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebAPI_MAM.Entities;

namespace WebAPI_MAM.Controllers
{ 
    [ApiController]
    [Route("MAM/Doctores")]
   
        public class DocCtrl:ControllerBase
        {
            private readonly ApplicationDbContext dbContext;

            public DocCtrl(ApplicationDbContext context)
            {
                this.dbContext = context;
            }

            [HttpGet] //Lista de los doctores
            public async Task<ActionResult<List<Doctors>>> Get()
            {
                return await dbContext.Doctors.ToListAsync();
               
            }

            [HttpPost]
            public async Task<ActionResult<Doctors>> Post(Doctors doctors)
            {
                 dbContext.Add(doctors);
                 await dbContext.SaveChangesAsync();
                 return Ok();
            }

            [HttpPut]
            public async Task<ActionResult> Put(Doctors doctors, int id)
            {
                if(doctors.Id != id)
                {
                    return BadRequest("El id del doctor no coincide en la URL");
                }
                dbContext.Update(doctors);
                await dbContext.SaveChangesAsync();
                return Ok();
            }

            [HttpDelete]
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
