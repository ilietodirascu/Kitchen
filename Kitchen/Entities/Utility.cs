using Kitchen.ExtensionMethods;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
        public static ConcurrentQueue<Food> LowComplexityFoodsToPrepare { get; set; } = new();
        public static ConcurrentQueue<Food> HighComplexityFoodsToPrepare { get; set; } = new();
        public static HttpClient Client { get; set; } = new();
        public static Food[] Menu { get; set; }
        static Utility()
        {
            using StreamReader u = new(@"menu.json");
            string foods = u.ReadToEnd();
            Menu = JsonConvert.DeserializeObject<Food[]>(foods);
        }
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
            Orders.Add(order);
            if (!LowComplexityFoodsToPrepare.Any() || !HighComplexityFoodsToPrepare.Any())
            {
                order = Orders.OrderByDescending(x => x.Priority).ThenBy(x => x.TimeOfCreation).First(); 
                order.Items.ToList().ForEach(y =>
                {
                    var food = Menu.FirstOrDefault(x => x.Id == y);
                    if (food.Complexity >= 2)
                    {
                        HighComplexityFoodsToPrepare.Enqueue(food);
                    }
                    else
                    {
                        LowComplexityFoodsToPrepare.Enqueue(food);
                    }
                });
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
