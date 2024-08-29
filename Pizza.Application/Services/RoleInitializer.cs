using Microsoft.AspNetCore.Identity;

public class RoleInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                // Cria a role
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
