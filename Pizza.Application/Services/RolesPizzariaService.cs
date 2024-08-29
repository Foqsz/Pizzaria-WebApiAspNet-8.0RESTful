using Microsoft.AspNetCore.Identity;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

public class RoleManagerService
{
    private readonly UserManager<ApplicationUserModel> _userManager;

    public RoleManagerService(UserManager<ApplicationUserModel> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> AssignRoleToUserAsync(string email, string roleName)
    {
        // Encontre o usuário pelo email
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return false; // Usuário não encontrado
        }

        // Verifique se a role já está atribuída ao usuário
        if (await _userManager.IsInRoleAsync(user, roleName))
        {
            return true; // Usuário já está na role
        }

        // Adicione o usuário à role
        var result = await _userManager.AddToRoleAsync(user, roleName);

        return result.Succeeded; // Retorna true se a role foi atribuída com sucesso
    }

    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

}
