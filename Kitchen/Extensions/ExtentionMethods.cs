using Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Kitchen.ExtensionMethods
{
    public static class ExtentionMethods
    {
        public static void SendOrder(this Order order)
        {
            
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60500/AddOrder", order);
        }
    }
}
