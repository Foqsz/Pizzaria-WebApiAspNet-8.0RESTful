namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs
{
    public class PizzaDTOUpdateResponse
    {
        public int Id { get; set; }
        public string? Sabor { get; set; }
        public decimal Price { get; set; }
        public string? Descricao { get; set; }
    }
}
