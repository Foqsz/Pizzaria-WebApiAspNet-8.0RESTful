using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.Services.Interfaces;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.API.Controllers;

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


}
