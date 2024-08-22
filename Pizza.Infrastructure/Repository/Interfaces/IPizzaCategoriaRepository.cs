using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository.Interfaces;

public interface IPizzaCategoriaRepository
{
    Task<IEnumerable<PizzaCategoriaModel>> GetCategoriasPizza();
    Task<PizzaCategoriaModel> GetCategoriaById(int id);
    Task<PizzaCategoriaModel> GetCategoriaCreate(PizzaCategoriaModel model);
    Task<PizzaCategoriaModel> GetCategoriaUpdate(PizzaCategoriaModel pizza);
    Task<PizzaCategoriaModel> GetCategoriaRemove(int id);

}
