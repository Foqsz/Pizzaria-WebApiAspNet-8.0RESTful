using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data
{
    public class PizzariaContext : IdentityDbContext<ApplicationUserModel>
    {
        public PizzariaContext(DbContextOptions<PizzariaContext> options) : base(options) { }

        public DbSet<PizzariaModel> Pizza { get; set; }
        public DbSet<PizzaCategoriaModel> CategoriaPizza { get; set; }
    }
}
