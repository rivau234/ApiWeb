using Microsoft.EntityFrameworkCore;
using MiWebApi.Entidades;

namespace MiWebApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //creacion de la base de datos
        public DbSet<Laptop> Laptops { get; set; }
    }
} 
