using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;

public class PizzaCategoriaDTO
{
    public int PizzaId { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<PizzariaModel>? Pizzaria { get; set; }
}
