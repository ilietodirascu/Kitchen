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
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60500/LogInfo", new { Message = $"Kitchen to dining hall Table:{order.Table.Number} our cooks finished your order " +
                $"of {Utility.GetItems(order)}" });
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60500/AddOrder", order);
        }
    }
}
