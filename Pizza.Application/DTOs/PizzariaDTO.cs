using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;
using System.Text.Json.Serialization;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs;

public class PizzariaDTO
{
    public int Id { get; set; }
    public string? Sabor { get; set; }
    public decimal Price { get; set; }
    public string? Descricao { get; set; }

    [JsonIgnore]
    public PizzaCategoriaModel? Categoria { get; set; }
    public int PizzaCategoriaId { get; set; }
}
