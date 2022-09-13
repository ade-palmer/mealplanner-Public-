using MealPlanner365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface ICategoryRepository : IBaseRepository
    {
        Task<IEnumerable<FoodType>> GetCategories(Guid mealPlanId);
        Task<bool> CategoryExists(String name, Guid mealPlanId);
        void AddCategory(FoodType category);
        Task<FoodType> GetCategoryById(Guid categoryId);
        void UpdateCategory(FoodType categoryToUpdate);
        void DeleteCategory(FoodType categoryToDelete);

        Task<int> GetCategoriesIncludingItemsCount(Guid categoryId);
        Task<int> GetCategoriesIncludingMealCount(Guid categoryId);
    }
}
