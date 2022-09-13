using MealPlanner365.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<UserMeal> UserMeals { get; set; }
        public DbSet<Shopping> Shopping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MealItem>()
                .HasKey(k => new { k.MealId, k.ItemId });

            modelBuilder.Entity<MealItem>()
                .HasOne(pt => pt.Meal)
                .WithMany(p => p.MealItems)
                .HasForeignKey(pt => pt.MealId);

            modelBuilder.Entity<MealItem>()
                .HasOne(pt => pt.Item)
                .WithMany(t => t.MealItems)
                .HasForeignKey(pt => pt.ItemId);


            modelBuilder.Entity<UserMeal>()
                .HasKey(k => new { k.UserId, k.MealId });

            modelBuilder.Entity<UserMeal>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserMeals)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserMeal>()
                .HasOne(pt => pt.Meal)
                .WithMany(t => t.UserMeals)
                .HasForeignKey(pt => pt.MealId);
             
            // TODO: Decide what needs to be removed from cascade delete
            modelBuilder.Entity<FoodType>()
                .HasOne(b => b.MealPlan)
                .WithMany(a => a.FoodTypes)
                .OnDelete(DeleteBehavior.Restrict);

            //Create default groups and admin account
            modelBuilder.SeedDefaultData();

            //Add some test data
            modelBuilder.SeedTestData();
        }
    }
}
