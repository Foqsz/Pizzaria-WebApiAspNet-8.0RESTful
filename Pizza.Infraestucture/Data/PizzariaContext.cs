using Microsoft.EntityFrameworkCore;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data
{
    public class PizzariaContext : DbContext
    {
        public PizzariaContext(DbContextOptions<PizzariaContext> options) : base(options) { }
    }
}
