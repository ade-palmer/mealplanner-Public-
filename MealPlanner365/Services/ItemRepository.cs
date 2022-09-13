using MealPlanner365.Contexts;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        public ItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            dbContext.Add(item);
        }

        public void DeleteItem(Item itemToDelete)
        {
            if (itemToDelete == null)
            {
                throw new ArgumentNullException(nameof(itemToDelete));
            }

            dbContext.Remove(itemToDelete);
        }

        public async Task<Item> GetItemById(Guid itemId)
        {
            var item = await dbContext.Items
                .AsNoTracking()
                .Include(t => t.FoodType)
                .SingleOrDefaultAsync(i => i.ItemId == itemId);

            return item;

        }

        public async Task<IEnumerable<ItemViewModel>> GetItems(Guid mealPlanId)
        {
            var items = await dbContext.Items
                .Where(p => p.MealPlanId == mealPlanId)
                .Include(t => t.FoodType)
                .Select(i => new ItemViewModel
                {
                    ItemId = i.ItemId,
                    ItemName = i.Name,
                    FoodTypeId = i.FoodType.FoodTypeId,
                    FoodTypeName = i.FoodType.Name
                })
                .OrderBy(i => i.FoodTypeName)
                    .ThenBy(i => i.ItemName)
                .ToListAsync();

            return items;
        }

        public async Task<int> GetItemUsage(Guid itemId)
        {
            var itemUsageCount = await dbContext.Items
                .Include(mi => mi.MealItems)
                    .ThenInclude(m => m.Meal)
                .Where(i => i.ItemId == itemId)
                .SelectMany(mi => mi.MealItems)
                .CountAsync();

            return itemUsageCount;
        }

        public async Task<IEnumerable<Item>> GetSelectListItems(Guid mealPlanId)
        {
            var selectListItems = await dbContext.Items
                .AsNoTracking()
                .Where(p => p.MealPlanId == mealPlanId)
                .Include(t => t.FoodType)
                .OrderBy(t => t.FoodType.Name)
                    .ThenBy(t => t.Name)
                .ToListAsync();
                
            return selectListItems;
        }

        public async Task<bool> ItemExists(string name, Guid? foodTypeId, Guid mealPlanId)
        {
            var itemExists = await dbContext.Items
                .AsNoTracking()
                .AnyAsync(n => n.Name.ToLower() == name.ToLower() &&
                               n.FoodTypeId == foodTypeId &&
                               n.MealPlanId == mealPlanId);

            return itemExists;
        }

        public void UpdateItem(Item itemToUpdate)
        {
            if (itemToUpdate == null)
            {
                throw new ArgumentNullException(nameof(itemToUpdate));
            }

            dbContext.Update(itemToUpdate);
        }
    }
}
