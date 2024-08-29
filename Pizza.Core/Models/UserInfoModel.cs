using System.Text.Json.Serialization;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

public class UserInfoModel
{
    public string? Email { get; set; }
    public string? Password { get; set; }

    [JsonIgnore]
    public List<string>? Roles { get; set; } // Adiciona a propriedade para as roles
}
