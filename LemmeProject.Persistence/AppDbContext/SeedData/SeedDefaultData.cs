using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LemmeProject.Persistence.AppDbContext.SeedData
{
    public static class SeedDefaultData
    {
        private static AboutApp aboutApp = new AboutApp()
        {
            Id = 1,
            AppName = "Lemme",
            Site = "www.lemme.az",
            AppVersion = "1.0.1",
            Content = "Lemme Product Searching Application"
        };

        private static List<AppRole> roles = new List<AppRole>()
        {
            new AppRole(){
                Id = 1,
                Name="Admin",
                NormalizedName="ADMIN"
            },
            new AppRole(){
                Id = 2,
                Name="Default",
                NormalizedName = "DEFAULT"
            },
            new AppRole(){
                Id = 3,
                Name="User",
                NormalizedName = "USER"
            },


        };

        private static List<AppUser> users = new List<AppUser>()
        {
            new AppUser(){
                Id = 1,
                FirstName="Arzu",
                LastName="Teymurova",
                UserName="Arzu",
                PasswordHash= new PasswordHasher<IdentityUser>().HashPassword(null,"12345"),
                Email="arzu@gmail.com",

            }
        };

        private static List<IdentityUserRole<int>> identityUserRoles = new List<IdentityUserRole<int>>()
        {
            new IdentityUserRole<int>()
            {
               UserId = 1,
               RoleId=1,
            },
            new IdentityUserRole<int>()
            {
                UserId=1,
                RoleId=2
            },
            new IdentityUserRole<int>()
            {
                UserId=1,
                RoleId=3,
            }

        };

        private static List<Store> stores = new List<Store>()
        {
            new Store()
            { 
                Id=1,
                Name="Olivia",
                Adress="Nizami küçəsi, 24"
            },
            new Store()
            {
                Id=2,
                Name="Real Beauty",
                Adress="Nizami küçəsi, 24"
            },
            new Store()
            {
                Id=3,
                Name="Bravo",
                Adress="H.Eliyev prospekti, 94"
            },
        };

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutApp>().HasData(aboutApp);

            modelBuilder.Entity<AppUser>().HasData(users);
            modelBuilder.Entity<AppRole>().HasData(roles);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(identityUserRoles);

            modelBuilder.Entity<Store>().HasData(stores);
        }
    }
}
