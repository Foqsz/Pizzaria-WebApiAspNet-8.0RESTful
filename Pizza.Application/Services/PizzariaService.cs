using AutoMapper; 
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs; 
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infraestucture.Repository;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;

public class PizzariaService : IPizzariaService
{
    private readonly IMapper _mapper;
    private readonly PizzariaRepository _pizzariaRepository;

    public PizzariaService(IMapper mapper, IPizzariaRepository pizzariaRepository)
    {
        _mapper = mapper;
        _pizzariaRepository = (PizzariaRepository?)pizzariaRepository;
    }

    public async Task<IEnumerable<PizzariaDTO>> GetPizzaAll()
    {
        var pizza = await _pizzariaRepository.GetByAll();
        return _mapper.Map<IEnumerable<PizzariaDTO>>(pizza);
    }

    public async Task<PizzariaDTO> GetPizzaById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaDTO> GetPizzaEdit(PizzariaDTO pizzaDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaDTO> GetPizzaNameByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<PizzariaDTO> GetRemovePizza(int id)
    {
        throw new NotImplementedException();
    }
}
