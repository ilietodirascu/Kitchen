using Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class Table
    {
        public int Number { get; set; }
        private static readonly Food[] _menu = new Food[]
        {
            new Food{Id = 1,Name = "Pizza", PreparationTime = 20, Complexity = 2, CookingAppratus = "Oven"},
            new Food{Id = 2, Name = "Salad", PreparationTime = 10, Complexity = 1, CookingAppratus = null },
            new Food{Id = 3, Name = "Zeama", PreparationTime = 7, Complexity = 1, CookingAppratus = "Stove" },
            new Food{Id = 4, Name = "Scallop Sashimi with Meyer Lemon Confit", PreparationTime = 32, Complexity = 3, CookingAppratus = null },
            new Food{Id = 5, Name = "Island Duck with Mulberry Mustard", PreparationTime = 35, Complexity = 3, CookingAppratus = "Oven" },
            new Food{Id = 6, Name = "Waffles", PreparationTime = 10, Complexity = 1, CookingAppratus = "Stove" },
            new Food{Id = 7, Name = "Aubergine", PreparationTime = 20, Complexity = 2, CookingAppratus = "Oven" },
            new Food{Id = 8, Name = "Lasagna", PreparationTime = 30, Complexity = 2, CookingAppratus = "Oven" },
            new Food{Id = 9, Name = "Burger", PreparationTime = 15, Complexity = 1, CookingAppratus = "Stove" },
            new Food{Id = 10, Name = "Gyros", PreparationTime = 15, Complexity = 1, CookingAppratus = null },
            new Food{Id = 11, Name = "Kebab", PreparationTime = 15, Complexity = 1, CookingAppratus = null },
            new Food{Id = 12, Name = "Unagi Maki", PreparationTime = 20, Complexity = 2, CookingAppratus = null },
            new Food{Id = 13, Name = "Tobacco Chicken", PreparationTime = 30, Complexity = 2, CookingAppratus = "Oven" },
        };
        public Table(int number)
        {
            Number = number;
        }
    }
}
