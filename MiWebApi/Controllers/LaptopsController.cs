using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiWebApi.Entidades;

namespace MiWebApi.Controllers
{
    [Route("api/laptops")]
    public class LaptopsController: ControllerBase 
    {
        private readonly ApplicationDbContext context;

        public LaptopsController(ApplicationDbContext context)//para usar entityframework
        {
            this.context = context;
        }

        //endpoints
        [HttpGet]
        public async Task<List<Laptop>> Get()
        {
            return await context.Laptops.ToListAsync();
        }
        //accion para traer una sola laptop
        [HttpGet("{id:int}" ,Name = "ObtenerLaptopPorId")]
        public async Task<ActionResult<Laptop>>Get(int id)
        {
            var laptop = await context.Laptops.FirstOrDefaultAsync(x => x.Id == id);//extrae el primar laptop con el id    

            if (laptop is null)
            {
                return NotFound();
            }
            return laptop;
        }

        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody] Laptop laptop)
        {
            context.Add(laptop); 
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerLaptopPorId", new { id = laptop.Id }, laptop);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] Laptop laptop)
        {
            var existeLaptop = await context.Laptops.AnyAsync(x => x.Id == id);
            if (!existeLaptop)
            {
                return NotFound();
            }
            context.Update(laptop);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id, Laptop laptop)
        {
            var filasBorradas = await context.Laptops.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (filasBorradas ==0)
            {
                return NotFound();
            }
            return NoContent();
        }
        


    }
}
