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
        return await _context.Pizza.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PizzariaModel> GetByName(string sabor)
    {
        return await _context.Pizza.FirstOrDefaultAsync(p => p.Sabor == sabor);
    }

    public async Task<PizzariaModel> GetPizzaCreate(PizzariaModel pizzariaDTO)
    {
        _context.Pizza.AddAsync(pizzariaDTO);
        await _context.SaveChangesAsync();

        return pizzariaDTO;
    }
    public async Task<PizzariaModel> GetPizzaUpdate(PizzariaModel pizzariaDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaModel> GetPizzaRemove(int id)
    {
        try
        {
            var PizzaCheck = _context.Pizza.FirstOrDefault(p => p.Id == id);
            _context.Remove(PizzaCheck);
            await _context.SaveChangesAsync();
            return PizzaCheck;
        }
        catch (Exception)
        {
            {
                throw new ArgumentException("Não foi encontrado essa pizza no cardápio.");
            }

        }
    }
}
