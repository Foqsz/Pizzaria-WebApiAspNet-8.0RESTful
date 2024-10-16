using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    private readonly ILogger _logger;

    public PizzaCategoriaController(IMapper mapper, IPizzaCategoriaService pizzaCategoria, ILogger logger)
    {
        _mapper = mapper;
        _pizzaCategoria = pizzaCategoria;
        _logger = logger;
    }

    #region Listar todas as categorias
    [Authorize(Roles = "User, Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PizzaCategoriaDTO>>> GetPizzaCategoriesAll()
    {
        var pizzaAll = await _pizzaCategoria.GetCategoriaAll();

        if (pizzaAll is null)
        {
            _logger.LogWarning("Não foi possivel listar todas as categorias");
            return StatusCode(StatusCodes.Status404NotFound, "Não foi possível localizar essa categoria no estoque");
        }

        _logger.LogWarning("Listagem de categorias feita com sucesso");
        return StatusCode(StatusCodes.Status200OK, pizzaAll);
    }
    #endregion

    #region Listar categoria por id
    [Authorize(Roles = "User, Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaId(int id)
    {
        var pizzaById = await _pizzaCategoria.GetCategoriaById(id);

        if (pizzaById is null)
        {
            _logger.LogWarning($"Não foi localizado a categoria de id {id} no bd");
            return StatusCode(StatusCodes.Status404NotFound, $"Olá. Não consta no estoque a categoria de pizza com o id {id}.");
        }

        _logger.LogInformation($"Categoria id {id} exibida com sucesso");
        return StatusCode(StatusCodes.Status200OK, pizzaById);
    }
    #endregion

    #region Criar uma nova categoria
    [Authorize(Roles = "Admin")]
    [HttpPost("NovaCategoria")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaCreate(PizzaCategoriaDTO pizzaCategoriaDto)
    {
        var pizzaCategoriaCreate = await _pizzaCategoria.GetCategoriaCreate(pizzaCategoriaDto);

        if (pizzaCategoriaCreate is null)
        {
            _logger.LogWarning("Dados nulos recebidos para criação de categoria, invalidado!");
            return StatusCode(StatusCodes.Status400BadRequest, "Não foi possível adicionar a categoria de pizza");
        }

        _logger.LogInformation("Categoria criada com sucesso");
        return StatusCode(StatusCodes.Status201Created, pizzaCategoriaCreate);
    }
    #endregion

    #region Atualizar uma categoria
    [Authorize(Roles = "Admin")]
    [HttpPut("AtualizarCategoria/{id}")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaUpdate(int id, [FromBody] PizzaCategoriaDTO pizzaDtoUpdate)
    {
        var PizzaCategoriaUpdate = await _pizzaCategoria.GetCategoriaUpdate(id, pizzaDtoUpdate);

        if (PizzaCategoriaUpdate is null)
        {
            _logger.LogWarning($"Não foi possivel atualizar a categoria id {id}, pois ela nao foi encontrada");
            return StatusCode(StatusCodes.Status404NotFound, "Não foi possível localizar essa categoria");
        }

        _logger.LogInformation($"Categoria id {id} atualizada com sucesso");
        return StatusCode(StatusCodes.Status200OK, $"Pizza de categoria {id} atualizada com sucesso.");
    }
    #endregion

    #region Deletar uma categoria
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeletarCategoria/{id}")]
    public async Task<ActionResult<PizzaCategoriaDTO>> GetPizzaCategoriaDelete(int id)
    {
        var PizzaCategoriaDelete = await _pizzaCategoria.GetCategoriaRemove(id);

        if (PizzaCategoriaDelete is null)
        {
            _logger.LogWarning($"Nao foi possivel a categoria id {id} para deletar...");
            return StatusCode(StatusCodes.Status404NotFound, $"Não foi possível localizar a categoria id {id}");
        }

        _logger.LogInformation("Categoria deletada com sucesso");
        return StatusCode(StatusCodes.Status200OK, $"Categoria {id} deleteda com sucesso.");
    }
    #endregion
}

