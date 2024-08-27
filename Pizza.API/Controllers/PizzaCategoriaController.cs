using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.API.Controllers;

[Authorize]
[Route("api/[controller]")]  
[ApiController]
public class PizzaCategoriaController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPizzaCategoriaService _pizzaCategoria;

    public PizzaCategoriaController(IMapper mapper, IPizzaCategoriaService pizzaCategoria)
    {
        _mapper = mapper;
        _pizzaCategoria = pizzaCategoria;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PizzaCategoriaDTO>>> GetPizzaCategoriesAll()
    {
        var pizzaAll = await _pizzaCategoria.GetCategoriaAll();

        if (pizzaAll is null)
        {
            return StatusCode(StatusCodes.Status404NotFound, "Não foi possível localizar essa categoria no estoque");
        }

        return StatusCode(StatusCodes.Status200OK, pizzaAll);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaId(int id)
    {
        var pizzaById = await _pizzaCategoria.GetCategoriaById(id);

        if (pizzaById is null)
        {
            return StatusCode(StatusCodes.Status404NotFound, $"Olá. Não consta no estoque a categoria de pizza com o id {id}.");
        }

        return StatusCode(StatusCodes.Status200OK, pizzaById);
    }

    [HttpPost("NovaCategoria")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaCreate(PizzaCategoriaDTO pizzaCategoriaDto)
    {
        var pizzaCategoriaCreate = await _pizzaCategoria.GetCategoriaCreate(pizzaCategoriaDto);

        if (pizzaCategoriaCreate is null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Não foi possível adicionar a categoria de pizza");
        }

        return StatusCode(StatusCodes.Status201Created, pizzaCategoriaCreate);
    }


    [HttpPut("AlterarCodigo/{id}")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaUpdate(int id, [FromBody] PizzaCategoriaDTO pizzaDtoUpdate)
    {
        var PizzaCategoriaUpdate = await _pizzaCategoria.GetCategoriaUpdate(id, pizzaDtoUpdate);

        if (PizzaCategoriaUpdate is null)
        {
            return StatusCode(StatusCodes.Status404NotFound, "Não foi possível localizar essa categoria");
        }

        return StatusCode(StatusCodes.Status200OK, $"Pizza de categoria {id} atualizada com sucesso.");
    }

    [HttpDelete("DeletarCategoria/{id}")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaDelete(int id)
    {
        var PizzaCategoriaDelete = await _pizzaCategoria.GetCategoriaRemove(id);

        if (PizzaCategoriaDelete is null)
        {
            return StatusCode(StatusCodes.Status404NotFound, $"Não foi possível localizar a categoria id {id}");
        }

        return StatusCode(StatusCodes.Status200OK, $"Categoria {id} deleteda com sucesso.");
    }
}

