using Kitchen.ExtensionMethods;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Utility
    {
        public static readonly Object Lock = new object();
        public static Random Random { get; set; } = new();
        public static List<Order> Orders { get; set; } = new();
        public static ConcurrentDictionary<int, List<Food>> PreparedFoods { get; set; } = new();
        public static ConcurrentQueue<Food> FoodsToPrepare{ get; set; } = new();
        public static HttpClient Client { get; set; } = new();
        public static readonly Food[] Menu = new Food[]
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
        public static int GetPreparationTime(int id)
        {
            return Menu.First(x => x.Id == id).PreparationTime;
        }
        public static void AddFood(Food food)
        {
            lock (Lock)
            {
                AddToDict(food);
                var orders = new List<Order>(Orders);
                orders.ForEach(o =>
                {
                    var allFoods = new List<Food>();
                    if (IsSubList(o.Items, PreparedFoods.Values.SelectMany(x => x).ToList().Select(x => x.Id).ToArray()))
                    {
                        o.Items.ToList().ForEach(i =>
                        {
                            var preparedFood = PreparedFoods.First(y => y.Key == i).Value.First();
                            RemoveFromDict(preparedFood);
                        });
                        o.SendOrder();
                        Orders.Remove(o);
                        
                    }
                });
            }
        }
        public static void AddToDict(Food food)
        {
            if (PreparedFoods.ContainsKey(food.Id))
            {
                PreparedFoods[food.Id].Add(food);
                return;
            }
            PreparedFoods.TryAdd(food.Id, new List<Food>() { food });
        }
        public static void RemoveFromDict(Food food)
        {
            if (PreparedFoods[food.Id].Count == 1)
            {
                PreparedFoods.Remove(food.Id, out _);
                return;
            }
            PreparedFoods[food.Id].Remove(food);
        }
        public static bool IsSubList(IEnumerable<int> sub, IEnumerable<int> super)
        {
            var list = super.ToList();
            foreach (var item in sub)
            {
                if (!list.Remove(item))
                    return false;
            }
            return true;
        }
        public static void AddOrder(Order order)
        {
            lock (Lock)
            {
                Orders.Add(order);
                Orders.OrderByDescending(x => x.Priority).First().Items.ToList().ForEach(y => FoodsToPrepare.Enqueue(Menu.FirstOrDefault(x => x.Id == y)));
            }
        }
        public static Food GetFood(int id)
        {
            return Menu.First(x => x.Id == id);
        }
        public static string GetItems(Order order)
        {
            var result = new List<string>();
            order.Items.ToList().ForEach(x => result.Add(Menu.Where(y => y.Id == x).Select(z => z.Name).First().ToString()));
            return String.Join(",", result);
        }
    }
}
