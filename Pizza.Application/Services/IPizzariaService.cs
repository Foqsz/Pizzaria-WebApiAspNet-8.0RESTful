using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;

public interface IPizzariaService
{
    Task<IEnumerable<PizzariaDTO>> GetPizzaAll();
    Task<PizzariaDTO> GetPizzaNameByName(string name);
    Task<PizzariaDTO> GetPizzaById(int id);
    Task<PizzariaDTO> GetPizzaNew(PizzariaModel pizzaDTO);
    Task<PizzariaDTO> GetPizzaEdit(int id, PizzariaDTO pizzaDTO);
    Task<PizzariaDTO> GetRemovePizza(int id);
}
