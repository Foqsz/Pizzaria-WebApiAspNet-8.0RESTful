using Microsoft.EntityFrameworkCore; 
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Repository;

public class PizzariaRepository : IPizzariaRepository
{
    private readonly PizzariaContext _context;

    public PizzariaRepository(PizzariaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PizzariaModel>> GetByAll()
    {
        return await _context.Pizza.ToListAsync();
    }

    public async Task<PizzariaModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaModel> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaModel> GetPizzaRemove(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaModel> GetPizzaUpdate(PizzariaModel pizzariaDTO)
    {
        throw new NotImplementedException();
    }
}
