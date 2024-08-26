using Microsoft.EntityFrameworkCore;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Data;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository.Interfaces;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository;

public class PizzaCategoriaRepository : IPizzaCategoriaRepository
{
    private readonly PizzariaContext _context;

    public PizzaCategoriaRepository(PizzariaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PizzaCategoriaModel>> GetCategoriasPizza()
    {
        return await _context.CategoriaPizza.ToListAsync();
    }

    public async Task<PizzaCategoriaModel> GetCategoriaById(int id)
    {
        return await _context.CategoriaPizza.FirstOrDefaultAsync(c => c.PizzaId == id);
    }

    public async Task<PizzaCategoriaModel> GetCategoriaCreate(PizzaCategoriaModel pizzaCategoria)
    {
        _context.CategoriaPizza.AddAsync(pizzaCategoria);
        await _context.SaveChangesAsync();
        return pizzaCategoria;
    }

    public async Task<PizzaCategoriaModel> GetCategoriaUpdate(PizzaCategoriaModel pizzaAtt)
    {
        _context.CategoriaPizza.Update(pizzaAtt);
        await _context.SaveChangesAsync();
        return pizzaAtt;
    }

    public async Task<PizzaCategoriaModel> GetCategoriaRemove(int id)
    {
        try
        {
            var categoriaId = _context.CategoriaPizza.FirstOrDefault(c => c.PizzaId == id);
            _context.CategoriaPizza.Remove(categoriaId);
            await _context.SaveChangesAsync();
            return categoriaId;
        }
        catch (Exception)
        {
            throw new ArgumentException("Não foi encontrado essa pizza no cardápio.");
        }
    }
}
