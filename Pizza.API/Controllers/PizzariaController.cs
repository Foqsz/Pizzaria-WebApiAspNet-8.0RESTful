using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

//O controlador recebe as requisições do Swagger e chama o service para aplicar as chamadas//

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PizzariaController : ControllerBase
{
    private readonly IPizzariaService _pizzaria;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public PizzariaController(IPizzariaService pizzaria, IMapper mapper, ILogger<PizzariaController> logger)
    {
        _pizzaria = pizzaria;
        _mapper = mapper;
        _logger = logger;
    }

    #region Fornecer todas as pizzas do cardápio
    [Authorize(Roles = "User, Admin")]
    [HttpGet("Cardapio")]
    public async Task<ActionResult<IEnumerable<PizzariaDTO>>> GetPizzaAll()
    {
        var Pizza = await _pizzaria.GetPizzaAll();

        if (Pizza is null)
        {
            _logger.LogWarning("Nenhuma pizza encontrada.");
            return StatusCode(StatusCodes.Status404NotFound, "Não foi localizado nenhuma pizza no cardápio.");
        }
        _logger.LogInformation("Listagem de pizzas feita com sucesso");
        return StatusCode(StatusCodes.Status200OK, Pizza);
    }
    #endregion

    #region Fornecer uma pizza pelo seu ID
    [Authorize(Roles = "User, Admin")]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaById(int id)
    {
        var PizzaId = await _pizzaria.GetPizzaById(id);
        if (PizzaId is null)
        {
            _logger.LogWarning($"Não foi encontrada a pizza de id {id} no cardápio...");
            return StatusCode(StatusCodes.Status404NotFound, "Não foi possível localizar essa pizza em nosso cardápio.");
        }
        _logger.LogInformation($"Pizza id {id} encontrada com sucesso na busca.");
        return StatusCode(StatusCodes.Status200OK, $"Pizza encontrada em nosso cardápio. Confira: {PizzaId.Sabor}, {PizzaId.Descricao}");
    }
    #endregion

    #region Fornece uma pizza pelo seu nome
    [Authorize(Roles = "User, Admin")]
    [HttpGet("Cardapio/PesquisarSabor")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaName(string sabor)
    {
        var pizzaName = await _pizzaria.GetPizzaNameByName(sabor);
        if (pizzaName is null)
        {
            _logger.LogWarning($"A busca pela pizza de sabor {sabor} não foi localizada.");
            return StatusCode(StatusCodes.Status404NotFound, $"Não foi possível localizar {sabor} em nosso cardápio.");
        }
        _logger.LogInformation($"Pizza de sabor {sabor} localizada com sucesso");
        return StatusCode(StatusCodes.Status200OK, $"Temos no cardápio a: {pizzaName.Sabor}, atualmente com o valor R${pizzaName.Price.ToString("F2")}");
    }
    #endregion

    #region Adicionar nova pizza no cardápio
    [Authorize(Roles = "Admin")]
    [HttpPost("NovaPizza")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaCreate([FromBody] PizzariaDTO pizzariaDTO)
    {
        var pizzaNew = await _pizzaria.GetPizzaNew(pizzariaDTO);
        if (pizzaNew is null)
        {
            _logger.LogWarning("Não foi possível adicionar a nova pizza");
            return StatusCode(StatusCodes.Status404NotFound, "Pizza Inválida.");
        }
        _logger.LogInformation("Pizza adicionada com sucesso");
        return StatusCode(StatusCodes.Status201Created, $"A Pizza de sabor {pizzaNew.Sabor} foi adicionada ao nosso cardápio.");

    }
    #endregion

    #region Atualizar uma pizza do cardápio
    [Authorize(Roles = "Admin")]
    [HttpPut("AtualizarPizza/{id:int}")]
    public async Task<ActionResult> GetPizzaUpdate(int id, [FromBody] PizzariaDTO pizzariaDTO)
    {
        if (id != pizzariaDTO.Id)
        {
            _logger.LogWarning("O id informado para atualizar é diferente do Id contido no Banco de dados");
            return StatusCode(StatusCodes.Status400BadRequest, $"A Pizza de ID = {id} é diferente da Pizza que deseja editar no cardápio.");
        }
        var PizzaAtt = await _pizzaria.GetPizzaEdit(id, pizzariaDTO);
        _logger.LogInformation("Pizza atualizada com sucesso");
        return StatusCode(StatusCodes.Status200OK, "Pizza alterada com sucesso em nosso cardápio.");
    }
    #endregion

    #region Atualizar a pizza parcialmente no cardápio
    [Authorize(Roles = "Admin")]
    [HttpPatch("AtualizacaoParcial/{id:int}")]
    public async Task<ActionResult<PizzaDTOUpdateResponse>> GetPizzaPatch(int id, JsonPatchDocument<PizzaDTOUpdateRequest> pizzaPatchDTO)
    {
        if (pizzaPatchDTO is null || id <= 0)
        {
            _logger.LogWarning("Pizza é nula.");
            return StatusCode(StatusCodes.Status404NotFound, "Pizza é nula ou ID não localizado.");
        }

        // Buscar a pizza existente no service
        var pizzaExistente = await _pizzaria.GetPizzaById(id);

        if (pizzaExistente is null)
        {
            _logger.LogWarning("Pizza não localizada para atualização parcial.");
            return NotFound("Pizza não encontrada.");
        }

        var pizzaDTOUpdate = _mapper.Map<PizzaDTOUpdateRequest>(pizzaExistente);

        // Aplicar o patch ao DTO existente
        pizzaPatchDTO.ApplyTo(pizzaDTOUpdate, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(pizzaDTOUpdate))
        {
            var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();

            return StatusCode(StatusCodes.Status400BadRequest, $"Pizza inválida: {string.Join(", ", errorMessages)}");
        }

        _mapper.Map(pizzaDTOUpdate, pizzaExistente);

        // Atualizar a pizza parcialmente via o service
        var pizzaAtualizada = await _pizzaria.GetPizzaPatch(pizzaExistente);

        _logger.LogInformation("Pizza atualizada parcialmente.");
        return StatusCode(StatusCodes.Status200OK, $"A Pizza foi atualizada parcialmente");
    }

    #endregion

    #region Deletar uma pizza do cardápio
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeletarPizza/{id:int}")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaRemove(int id)
    {
        var pizzaRemove = await _pizzaria.GetRemovePizza(id);
        if (pizzaRemove is null)
        {
            _logger.LogWarning("Não foi possível encontrar a pizza para deletar.");
            return StatusCode(StatusCodes.Status404NotFound, "Pizza não encontrada");
        }
        _logger.LogInformation("Pizza removida com sucesso");
        return StatusCode(StatusCodes.Status200OK, $"A Pizza {pizzaRemove.Sabor} foi removida com súcesso do cardápio.");
    }
    #endregion
}
