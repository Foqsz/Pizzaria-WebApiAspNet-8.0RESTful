using AutoMapper; 
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs; 
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Repository;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;

public class PizzariaService : IPizzariaService
{
    private readonly IMapper _mapper;
    private readonly PizzariaRepository _pizzariaRepository;

    public PizzariaService(IMapper mapper, PizzariaRepository pizzariaRepository)
    {
        _mapper = mapper;
        _pizzariaRepository = pizzariaRepository;
    }

    public async Task<IEnumerable<PizzariaDTO>> GetByAll()
    {
        var pizza = await _pizzariaRepository.GetByAll();
        return _mapper.Map<IEnumerable<PizzariaDTO>>(pizza);
    }

    public Task<IEnumerable<PizzariaDTO>> GetPizzaAll()
    {
        throw new NotImplementedException();
    }

    public Task<PizzariaDTO> GetPizzaById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PizzariaDTO> GetPizzaEdit(PizzariaDTO pizzaDTO)
    {
        throw new NotImplementedException();
    }

    public Task<PizzariaDTO> GetPizzaNameByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<PizzariaDTO> GetRemovePizza(int id)
    {
        throw new NotImplementedException();
    }
}
