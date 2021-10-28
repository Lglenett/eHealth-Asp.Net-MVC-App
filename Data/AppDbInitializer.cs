using eHealth.Data.Static;
using eHealth.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eHealth.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Policosonal 10m 60 Tablets",
                            Description = "GNC Policosonal 10m 60 Tablets is a cholesterol support supplement",
                            Price = 119.00,
                            ImageURL = "https://clicks.co.za/medias/?context=bWFzdGVyfHByb2R1Y3QtaW1hZ2VzfDE4NDIyfGltYWdlL2pwZWd8cHJvZHVjdC1pbWFnZXMvaGFiL2g4Yi8xMDIzODUxMjA3MDY4Ni5qcGd8YjRiMjM5ZWZmZjU4NjA3YzY1NTM2OWRjYjgzOTUyNWY5ZjdiNmZiMjY3OTFmNDlhNTYxNGQzZTYyNGM5YjQ5YQ",
                            ProductCategory = ProductCategory.Supplements
                        },
                        new Product()
                        {
                            Name = "Benylin",
                            Description = "Children s Wet Cough Syrup Mucus Relief Ages 2 to 12 200ml",
                            Price = 84.99,
                            ImageURL = "https://clicks.co.za/medias/?context=bWFzdGVyfHByb2R1Y3QtaW1hZ2VzfDQ1NDM1fGltYWdlL2pwZWd8cHJvZHVjdC1pbWFnZXMvaDdjL2gzNC85NTQ0MjA5MjM1OTk4LmpwZ3xkZjMyNmNhODU0MWUyNTNjZGVhMzJjNzk1NmY4NDRhOTg4M2EzZTllZGMyNjdhNmI3OGE3N2QzMzkwNTJkNGNm",
                            ProductCategory = ProductCategory.Cough
                        },
                        new Product()
                        {
                            Name = "Mentat Syrup 200ml",
                            Description = "Himalaya Herbal Healthcare Mentat Syrup 200ml improves memory ",
                            Price = 149.00,
                            ImageURL = "https://clicks.co.za/medias/?context=bWFzdGVyfHByb2R1Y3QtaW1hZ2VzfDM3MDU2fGltYWdlL2pwZWd8cHJvZHVjdC1pbWFnZXMvaGYwL2gxMy85NzQ2NzExODM4NzUwLmpwZ3wxYWEwNmQ0ZWE4MzAxYzcwMWI5MjUzMjhkYTFhOGY5MzgzMWYyYjk0YmRjYTdiNjUzNjA2OWE2OWM2MzcyYzA1",
                            ProductCategory = ProductCategory.Vitamins
                        },
                        new Product()
                        {
                            Name = "E45 Cream 500g",
                            Description = "E45 Cream 500g moisturises and relieves dry skin conditions such as eczema",
                            Price = 225.00,
                            ImageURL = "https://clicks.co.za/medias/?context=bWFzdGVyfHByb2R1Y3QtaW1hZ2VzfDI1OTk3fGltYWdlL2pwZWd8cHJvZHVjdC1pbWFnZXMvaDk3L2gyZi84ODQ1MDQ0NzQ0MjIyLmpwZ3w4NDU2MDk3OGY2MGIwZmFiMGI1NjI2YTA2NzRiYjAwZWUyYmJjYjk4ZTUwMzA2YjNlYWI4NzU4MmZmNzk1NDNm",
                            ProductCategory = ProductCategory.Antimalarials
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndrolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles Section
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager< IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));


                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Admin-User
                var UserManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@ehealth.com";
                var adminUser = await UserManager.FindByEmailAsync(adminUserEmail);
                if(adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await UserManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await UserManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //App-User
                string appUserEmail = "user@ehealth.com";
                var appUser = await UserManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await UserManager.CreateAsync(newAppUser, "Coding@1234?");
                    await UserManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
