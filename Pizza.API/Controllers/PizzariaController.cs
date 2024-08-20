using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services;

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

    [HttpPost]
    public async Task<ActionResult<PizzariaDTO>> GetPizzaById(int id)
    {
        var PizzaId = await _pizzaria.GetPizzaById(id);
        if (PizzaId is null)
        {
            return NotFound("Não foi possível localizar essa pizza em nosso cardápio.");
        }
        return Ok($"Pizza encontrada em nosso cardápio. Confira: {PizzaId}");
    }
}
