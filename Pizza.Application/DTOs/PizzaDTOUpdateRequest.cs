using System.ComponentModel.DataAnnotations;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs
{
    // Classe que representa a solicitação de atualização de uma pizza
    public class PizzaDTOUpdateRequest : IValidatableObject
    { 
        public string? Sabor { get; set; }
         
        public decimal Price { get; set; }
         
        public string? Descricao { get; set; }

        // Método que implementa a validação do objeto
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Valida se o sabor foi fornecido
            if (string.IsNullOrEmpty(Sabor))
            {
                // Retorna um erro caso o sabor esteja vazio ou nulo
                yield return new ValidationResult("O sabor da pizza é obrigatório.", new[] { nameof(Sabor) });
            }

            // Valida se o preço é maior que zero
            if (Price <= 0)
            {
                // Retorna um erro caso o preço não seja válido
                yield return new ValidationResult("O preço deve ser maior que zero.", new[] { nameof(Price) });
            }

            // Valida a descrição se ela for fornecida
            if (Descricao != null && Descricao.Length > 500)
            {
                // Retorna um erro caso a descrição exceda 500 caracteres
                yield return new ValidationResult("A descrição não pode exceder 500 caracteres.", new[] { nameof(Descricao) });
            }
        }
    }
}
