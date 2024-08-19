using System.ComponentModel.DataAnnotations;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

public class PizzaCategoriaModel
{
    [Key]
    public int PizzaId { get; set; }
    public string? Name { get; set; }

}
