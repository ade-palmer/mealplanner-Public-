using MealPlanner365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services
{
    internal class UserMealComparer : IEqualityComparer<UserMeal>
    {
        bool IEqualityComparer<UserMeal>.Equals(UserMeal x, UserMeal y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;
            return (x.MealId.Equals(y.MealId) && x.UserId.Equals(y.UserId));
        }

        int IEqualityComparer<UserMeal>.GetHashCode(UserMeal obj)
        {
            if (obj == null) return 0;

            int MealIdHashCode = obj.MealId.GetHashCode();
            int UserIdHashCode = obj.UserId.GetHashCode();
            return MealIdHashCode ^ UserIdHashCode;
        }
    }
}
