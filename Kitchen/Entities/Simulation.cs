using Kitchen.ExtensionMethods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Simulation
    {
        public static List<CookingApparatus> CookingApparatuses { get; set; }
        public static List<Cook> Cooks { get; set; }
        static Simulation()
        {
            using StreamReader cr = new(@"cooks.json");
            string cooks = cr.ReadToEnd();
            Cooks = JsonConvert.DeserializeObject<List<Cook>>(cooks);
            using StreamReader ca = new(@"cookingApparatuses.json");
            string cookingApps = ca.ReadToEnd();
            CookingApparatuses = JsonConvert.DeserializeObject<List<CookingApparatus>>(cookingApps);
        }
        public void RunSimulation()
        {
            while (true)
            {
                foreach (var cook in Cooks)
                {
                    cook.DoWork();
                }
            }
        }

    }
}
