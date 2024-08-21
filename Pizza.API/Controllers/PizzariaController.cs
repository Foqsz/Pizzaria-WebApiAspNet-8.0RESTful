using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;
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

    [HttpGet("Cardápio/{sabor}")]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaName(string sabor)
    {
        var pizzaName = await _pizzaria.GetPizzaNameByName(sabor);
        if (pizzaName is null)
        {
            return NotFound($"A Pizza de {sabor} não foi encontrada em nosso cardápio.");
        }
        return Ok($"Temos a Pizza {pizzaName.Sabor}, deseja comprar?");

    }

    [HttpPost]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaCreate(PizzariaModel pizzariaDTO)
    {
        var pizzaNew = await _pizzaria.GetPizzaNew(pizzariaDTO);
        if (pizzaNew is null)
        {
            return NotFound("Pizza Inválida.");
        }
        return Ok($"A Pizza de sabor {pizzaNew.Sabor} foi adicionada ao nosso cardápio.");

    }

    [HttpDelete("{id}")]
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
