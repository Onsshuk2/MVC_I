using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pv311_mvc_project.Models;
using pv311_mvc_project.Models.Identity;

namespace pv311_mvc_project.Data.Initializer
{
    public static class Seeder
    {
        public static async Task SeedAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Виконуємо міграцію бази даних
                await context.Database.MigrateAsync();

                // Створюємо ролі, якщо їх немає
                if (!await roleManager.Roles.AnyAsync())
                {
                    var adminRole = new IdentityRole { Name = Settings.RoleAdmin };
                    var userRole = new IdentityRole { Name = Settings.RoleUser };

                    await roleManager.CreateAsync(adminRole);
                    await roleManager.CreateAsync(userRole);
                }

                // Створюємо користувачів, якщо їх немає
                if (!await userManager.Users.AnyAsync())
                {
                    var admin = new AppUser
                    {
                        Email = "admin@gmail.com",
                        UserName = "admin",
                        FirstName = "Admin",
                        LastName = "Admin",
                        EmailConfirmed = true
                    };

                    var user = new AppUser
                    {
                        Email = "user@gmail.com",
                        UserName = "user",
                        FirstName = "User",
                        LastName = "User",
                        EmailConfirmed = true
                    };

                    // Створюємо користувачів і додаємо паролі
                    var adminCreationResult = await userManager.CreateAsync(admin, "qwerty");
                    if (adminCreationResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, Settings.RoleAdmin); // Адміністратор
                    }

                    var userCreationResult = await userManager.CreateAsync(user, "qwerty");
                    if (userCreationResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Settings.RoleUser); // Звичайний користувач
                    }
                }

                // Створення категорій і продуктів, якщо їх немає
                if (!await context.Categories.AnyAsync())
                {
                    var categories = new List<Category>
                {
                    new Category{ Id = Guid.NewGuid().ToString(), Name = "Процесори" },
                    new Category{ Id = Guid.NewGuid().ToString(), Name = "Материнські плати" },
                    new Category{ Id = Guid.NewGuid().ToString(), Name = "Блоки живлення" },
                    new Category{ Id = Guid.NewGuid().ToString(), Name = "Оперативна пам'ять" },
                    new Category{ Id = Guid.NewGuid().ToString(), Name = "Відеокарти" },
                    new Category{ Id = Guid.NewGuid().ToString(), Name = "Накопичувачі" }
                };

                    await context.Categories.AddRangeAsync(categories);
                    await context.SaveChangesAsync();

                    var products = new List<Product>
                {
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Gigabyte GeForce RTX 4060 Gaming OC 8192MB", Price = 15799, Amount = 5, CategoryId = categories[4].Id },
                    new Product { Id = Guid.NewGuid().ToString(), Name = "Gigabyte B550M AORUS ELITE AX", Price = 4699, Amount = 10, CategoryId = categories[1].Id }
                };

                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

}
