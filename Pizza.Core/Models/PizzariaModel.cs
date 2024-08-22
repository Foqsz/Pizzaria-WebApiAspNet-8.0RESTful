using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

public class PizzariaModel
{
    public int Id { get; set; }
    public string? Sabor { get; set; }
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }
    public string? Descricao { get; set; }

    [JsonIgnore]
    public PizzaCategoriaModel? Categoria { get; set; }
    public int PizzaCategoriaId { get; set; }

}
