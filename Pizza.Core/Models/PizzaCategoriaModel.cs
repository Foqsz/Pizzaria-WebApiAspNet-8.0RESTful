using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

public class PizzaCategoriaModel
{
    [Key]
    public int PizzaId { get; set; }
    public string? PizzaCategoria { get; set; }
    public int PizzaEstoque { get; set; }

    [JsonIgnore]
    public IEnumerable<PizzariaModel>? Pizzaria { get; set; }

}
