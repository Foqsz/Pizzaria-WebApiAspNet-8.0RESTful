using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;

public interface IPizzaCategoriaService
{
    Task<IEnumerable<PizzaCategoriaDTO>> GetCategoriaAll();
    Task<PizzaCategoriaDTO> GetCategoriaById(int id); 
    Task<PizzaCategoriaDTO> GetCategoriaCreate(PizzaCategoriaDTO pizzaCategoriaDTO);
    Task<PizzaCategoriaDTO> GetCategoriaUpdate(PizzaCategoriaDTO categoriaDTO);
    Task<PizzaCategoriaDTO> GetCategoriaRemove(int id);
}
