using AutoMapper;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
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
        var pizzaId = await _pizzariaRepository.GetById(id);
        return _mapper.Map<PizzariaDTO>(pizzaId);
    }
    public async Task<PizzariaDTO> GetPizzaNameByName(string sabor)
    {
        var pizzaName = await _pizzariaRepository.GetByName(sabor);
        return _mapper.Map<PizzariaDTO>(sabor);
    }

    public async Task<PizzariaDTO> GetPizzaNew(PizzariaModel pizzaDTO)
    {
        var pizzaCreate = await _pizzariaRepository.GetPizzaCreate(pizzaDTO);
        return _mapper.Map<PizzariaDTO>(pizzaCreate);
    }

    public async Task<PizzariaDTO> GetPizzaEdit(int id, PizzariaDTO pizzaDTO)
    { 
        var PizzaExist = await _pizzariaRepository.GetById(id);

        if (PizzaExist == null)
        {
            throw new KeyNotFoundException("Pizza não encontrada em nosso cardápio.");
        }

        //1 Atualizando com Dto
        var pizzaToUpdate = _mapper.Map(pizzaDTO, PizzaExist);

        //2 Atualizando no repositorio
        var updatedPizza = await _pizzariaRepository.GetPizzaUpdate(pizzaToUpdate);

        //3 Mapeando para Dto
        return _mapper.Map<PizzariaDTO>(updatedPizza);
    }

    public async Task<PizzariaDTO> GetRemovePizza(int id)
    {
        var RemovePizza = await _pizzariaRepository.GetPizzaRemove(id);
        return _mapper.Map<PizzariaDTO>(RemovePizza);
    }
}
