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
        public static List<Cook> Cooks = new() { new Cook { Name = "David Bowie", Proficiency = 4, Rank = 4 }, new Cook { Name = "Stevie Wonder", Proficiency = 3, Rank = 2 }, new Cook { Name = "Woodrow Wilson", Proficiency = 2, Rank = 2 }, new Cook { Name = "Jamie Jamison", Proficiency = 2, Rank = 1 } };

        //,new Cook { Name = "Stevie Wonder", Proficiency = 3, Rank = 2 }, new Cook { Name = "Woodrow Wilson", Proficiency = 2, Rank = 2 }, new Cook { Name = "Jamie Jamison", Proficiency = 2, Rank = 1 }  
        public void RunSimulation()
        {
            while (true)
            {
                foreach (var cook in Cooks)
                {
                    cook.DoWork();
                }
            }
            //while (true)
            //{
            //    if (!Utility.Orders.Any()) continue;
            //    Cooks.ForEach(x =>
            //    {
            //        x.DoWork();
            //    });
            //}
        }

    }
}
