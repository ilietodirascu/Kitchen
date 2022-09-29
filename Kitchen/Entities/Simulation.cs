using Kitchen.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Simulation
    {
        public static List<Cook> Cooks = new() { new Cook { Name = "David Bowie", Proficiency = 3, Rank = 3 }, new Cook { Name = "Stevie Wonder", Proficiency = 2, Rank = 3 }, new Cook { Name = "Woodrow Wilson", Proficiency = 4, Rank = 3 } };

        public void RunSimulation()
        {
            while (true)
            {
                if (!Utility.Orders.Any()) continue;
                while (Utility.FoodsToPrepare.Any())
                {
                    Cooks.ForEach(x =>
                    {
                        DoWork(x);
                    });
                }
            }
        }
        public static void DoWork(Cook cook)
        {
            while (cook.Proficiency > 0)
            {
                if (Utility.FoodsToPrepare.TryDequeue(out Food food) && cook.Rank >= food.Complexity)
                {
                    new Thread(() =>
                    {
                        cook.PrepareFood(food);
                    }).Start();
                }
            }
        }
    }
}
