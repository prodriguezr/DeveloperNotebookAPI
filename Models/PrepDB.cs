using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace DeveloperNotebookAPI.Models
{
    public static class PrepDB
    {
        public static void Population(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<MyDbContext>());
            }
        }

        public static void SeedData(MyDbContext context)
        {
            DateTime vNow = DateTime.Now;

            try {
                System.Console.WriteLine("Applying Migrations...");

                context.Database.Migrate();

                if (context.Users.Any())
                    System.Console.WriteLine("Already have Users - Not seeding");
                else
                {
                    System.Console.WriteLine("Adding User - Seeding...");

                    context.Users.Add(new User() { Username = "Admin", Password = Helpers.Security.Encrypt("Testing1122", 30)});
                    context.Users.Add(new User() { Username = "prodrigu", Password = Helpers.Security.Encrypt("Testing1122", 30)});
                }

                if (context.Categories.Any())
                    System.Console.WriteLine("Already have Categories - Not seeding");
                else
                {
                    System.Console.WriteLine("Adding Categories - Seeding...");

                    context.Categories.AddRange(
                        new Category() {Name = "Category 1", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Category() {Name = "Category 3", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Category() {Name = "Category 4", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Category() {Name = "Category 2", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Category() {Name = "Category 5", CreationDate = vNow, UserId = 1, ActiveRecord = true}
                    ); 
                }

                if (context.Platforms.Any())
                    System.Console.WriteLine("Already have Platforms - Not seeding");
                else
                {
                    System.Console.WriteLine("Adding Platforms - Seeding...");

                    context.Platforms.AddRange(
                        new Platform() {Name = "Unix", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Platform() {Name = "Windows", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Platform() {Name = "Linux", CreationDate = vNow, UserId = 1, ActiveRecord = true},
                        new Platform() {Name = "MAC OS", CreationDate = vNow, UserId = 1, ActiveRecord = true}
                    ); 
                }
                
                context.SaveChanges();  

                System.Console.WriteLine("Migration (s) and data population have been successfully completed");         
            }
            catch(System.Exception ex) {
                System.Console.WriteLine("Ha ocurrido la siguiente excepción no controlada: {0}", ex.Message);
            }
        }
    }
}