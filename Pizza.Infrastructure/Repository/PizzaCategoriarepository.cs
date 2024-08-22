using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository.Interfaces;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository;

public class PizzaCategoriarepository : IPizzaCategoriaRepository
{
    public Task<PizzaCategoriaModel> GetCategoriaById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaModel> GetCategoriaCreate(PizzaCategoriaModel model)
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaModel> GetCategoriaRemove(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PizzaCategoriaModel>> GetCategoriasPizza()
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaModel> GetCategoriaUpdate(PizzaCategoriaModel pizza)
    {
        throw new NotImplementedException();
    }
}
