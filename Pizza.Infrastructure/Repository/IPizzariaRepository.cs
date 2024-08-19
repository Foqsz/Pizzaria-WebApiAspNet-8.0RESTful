using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Repository;

public interface IPizzariaRepository
{
    Task<PizzariaModel> GetById(int id);
    Task<PizzariaModel> GetByName(string name);
    Task<IEnumerable<PizzariaModel>> GetByAll();
    Task<PizzariaModel> GetPizzaUpdate(PizzariaModel pizzariaDTO);
    Task<PizzariaModel> GetPizzaRemove(int id);
}
