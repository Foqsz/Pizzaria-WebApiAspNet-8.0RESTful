using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

public class PizzaCategoriaModel
{
    [Key]
    public int PizzaId { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public ICollection<PizzariaModel>? Pizzaria { get; set; }

}
