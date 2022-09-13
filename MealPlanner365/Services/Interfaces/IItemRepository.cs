using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface IItemRepository : IBaseRepository
    {
        Task<IEnumerable<ItemViewModel>> GetItems(Guid mealPlanId);  // TODO: Is it bad to have a View Model in a Repository?
        Task<bool> ItemExists(String name, Guid? foodTypeId, Guid mealPlanId);
        void AddItem(Item item);
        Task<Item> GetItemById(Guid itemId);
        Task<IEnumerable<Item>> GetSelectListItems(Guid mealPlanId);
        void UpdateItem(Item itemToUpdate);
        void DeleteItem(Item itemToDelete);
        Task<int> GetItemUsage(Guid itemId);
    }
}
