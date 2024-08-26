using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzariaController : ControllerBase
{
    private readonly IPizzariaService _pizzaria;

    public PizzariaController(IPizzariaService pizzaria)
    {
        _pizzaria = pizzaria;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PizzariaDTO>>> GetPizzaAll()
    {
        var Pizza = await _pizzaria.GetPizzaAll();

        if (Pizza is null)
        {
            return BadRequest();
        }
        return Ok(Pizza); 
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaById(int id)
    {
        var PizzaId = await _pizzaria.GetPizzaById(id);
        if (PizzaId is null)
        {
            return NotFound("Não foi possível localizar essa pizza em nosso cardápio.");
        }
        return Ok($"Pizza encontrada em nosso cardápio. Confira: {PizzaId.Sabor}, {PizzaId.Descricao}");
    }

    [HttpGet("Cardápio/{string}")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaName(string sabor)
    {
        var pizzaName = await _pizzaria.GetPizzaNameByName(sabor);
        if (pizzaName is null)
        {
            return NotFound($"A Pizza de {sabor} não foi encontrada em nosso cardápio.");
        }
        return Ok($"Temos a Pizza {pizzaName.Sabor}, atualmente com o valor R${pizzaName.Price.ToString("F2")}.");

    }

    [HttpPost]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaCreate([FromBody]PizzariaDTO pizzariaDTO)
    {
        var pizzaNew = await _pizzaria.GetPizzaNew(pizzariaDTO);
        if (pizzaNew is null)
        {
            return NotFound("Pizza Inválida.");
        }
        return StatusCode(StatusCodes.Status201Created, $"A Pizza de sabor {pizzaNew.Sabor} foi adicionada ao nosso cardápio.");

    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> GetPizzaUpdate(int id, [FromBody]PizzariaDTO pizzariaDTO)
    {
        if (id != pizzariaDTO.Id)
        {
            return BadRequest($"A Pizza de ID = {id} é diferente da Pizza que deseja editar no cardápio.");
        }
        var PizzaAtt = await _pizzaria.GetPizzaEdit(id, pizzariaDTO);
        return StatusCode(StatusCodes.Status200OK, "Pizza alterada com sucesso em nosso cardápio.");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaRemove(int id)
    {
        var pizzaRemove = await _pizzaria.GetRemovePizza(id);
        if (pizzaRemove is null)
        {
            return NotFound("Pizza não encontrada");
        }
        return Ok($"A Pizza {pizzaRemove.Sabor} foi removida com súcesso do cardápio.");
    }
}
