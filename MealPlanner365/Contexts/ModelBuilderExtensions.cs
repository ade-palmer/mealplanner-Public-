using MealPlanner365.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace MealPlanner365.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDefaultData(this ModelBuilder modelBuilder)
        {
            const string ADMIN_ROLE_ID = "f8bcb1e0-6746-4ac7-a967-fe0c3e5fff7c";
            const string ADMIN_USER_ID = "e5976876-269c-486e-aec0-7d90aae2b59d";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Administrator",
                    NormalizedName = "Administrator".ToUpper()
                }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = ADMIN_USER_ID,
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper(),
                    Email = "administrator@mealplanner365.com",
                    NormalizedEmail = "administrator@mealplanner365.com".ToUpper(),
                    EnrollmentDateTime = DateTimeOffset.Now,
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "ComplexPasswordHere!"),
                    SecurityStamp = string.Empty
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_USER_ID
                }
            );
        }

        public static void SeedTestData(this ModelBuilder modelBuilder)
        {
            const string CREATOR_ROLE_ID = "a23ba371-39dc-40de-b163-4d22c9dcdc93";
            const string CREATOR_USER_ID = "daaeb138-c239-4dd4-b83f-c08fbf8ead52";
            const string MEAL_PLAN_ID = "0c1574aa-a75a-4b57-bbfb-e63cac9c4a96";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = CREATOR_ROLE_ID,
                    Name = "Creator",
                    NormalizedName = "Creator".ToUpper()
                }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = CREATOR_USER_ID,
                UserName = "Bob",
                NormalizedUserName = "Bob".ToUpper(),
                Email = "bob.smith@mealplanner365.co.uk",
                NormalizedEmail = "bob.smith@mealplanner365.co.uk".ToUpper(),
                EnrollmentDateTime = DateTimeOffset.Now,
                MealPlanId = Guid.Parse(MEAL_PLAN_ID),
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "ComplexPasswordHere!"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = CREATOR_ROLE_ID,
                    UserId = CREATOR_USER_ID
                }
            );

            modelBuilder.Entity<MealPlan>().HasData(
                new MealPlan
                {
                    MealPlanId = Guid.Parse(MEAL_PLAN_ID),
                    Name = "Smith Dinners",
                    Description = "Smith Dinners",
                    WeekStartDay = DayOfWeek.Monday
                }
            );
        }
    }
}