using MealPlanner365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services
{
    internal class MealItemComparer : IEqualityComparer<MealItem>
    {
        bool IEqualityComparer<MealItem>.Equals(MealItem x, MealItem y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;
            return (x.MealId.Equals(y.MealId) && x.ItemId.Equals(y.ItemId));
        }

        int IEqualityComparer<MealItem>.GetHashCode(MealItem obj)
        {
            if (obj == null) return 0;

            int MealIdHashCode = obj.MealId.GetHashCode();
            int ItemIdHashCode = obj.ItemId.GetHashCode();
            return MealIdHashCode ^ ItemIdHashCode;
        }
    }
}
