using AutoMapper;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Infrastructure.Repository.Interfaces;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;

public class PizzaCategoriaService : IPizzaCategoriaService
{
    private readonly IMapper _mapper;
    private readonly PizzaCategoriaRepository _pizzaCategoriaRepository;

    public PizzaCategoriaService(IMapper mapper, IPizzaCategoriaRepository pizzaCategoriaRepository)
    {
        _mapper = mapper;
        _pizzaCategoriaRepository = (PizzaCategoriaRepository?)pizzaCategoriaRepository;
    }

    public async Task<PizzaCategoriaDTO> GetCategoriaById(int id)
    {
        var pizzaCategoriaCheck = await _pizzaCategoriaRepository.GetCategoriaById(id);
        return _mapper.Map<PizzaCategoriaDTO>(pizzaCategoriaCheck);
    }

    public async Task<IEnumerable<PizzaCategoriaDTO>> GetCategoriaAll()
    {
        var categoriasAll = await _pizzaCategoriaRepository.GetCategoriasPizza();
        return _mapper.Map<IEnumerable<PizzaCategoriaDTO>>(categoriasAll);
    }

    public async Task<PizzaCategoriaDTO> GetCategoriaCreate(PizzaCategoriaDTO pizzaCategoriaDTO)
    {
        var categoriaNew = _mapper.Map<PizzaCategoriaModel>(pizzaCategoriaDTO);
        var categoriaNewEnviar = await _pizzaCategoriaRepository.GetCategoriaCreate(categoriaNew);
        return _mapper.Map<PizzaCategoriaDTO>(categoriaNewEnviar);
    }

    public async Task<PizzaCategoriaDTO> GetCategoriaUpdate(int id, PizzaCategoriaDTO categoriaDTO)
    {
        var categoriaExist = await _pizzaCategoriaRepository.GetCategoriaById(id);

        if (categoriaExist == null)
        {
            throw new KeyNotFoundException("Categoria de Pizza não encontrada em nosso Estoque.");
        }

        var categoriaAttMap = _mapper.Map(categoriaDTO, categoriaExist);

        var categoriaUpdate = await _pizzaCategoriaRepository.GetCategoriaUpdate(categoriaAttMap);

        return _mapper.Map<PizzaCategoriaDTO>(categoriaUpdate);
    }

    public async Task<PizzaCategoriaDTO> GetCategoriaRemove(int id)
    {
        var cattegoriaRemove = await _pizzaCategoriaRepository.GetCategoriaRemove(id);
        return _mapper.Map<PizzaCategoriaDTO>(cattegoriaRemove);
    }
}
