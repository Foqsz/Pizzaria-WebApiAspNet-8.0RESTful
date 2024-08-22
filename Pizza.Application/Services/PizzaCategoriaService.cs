using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;

public class PizzaCategoriaService : IPizzaCategoriaService
{
    public Task<IEnumerable<PizzaCategoriaDTO>> GetCategoriaAll()
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaDTO> GetCategoriaById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaDTO> GetCategoriaCreate(PizzaCategoriaDTO pizzaCategoriaDTO)
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaDTO> GetCategoriaRemove(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PizzaCategoriaDTO> GetCategoriaUpdate(PizzaCategoriaDTO categoriaDTO)
    {
        throw new NotImplementedException();
    }
}
