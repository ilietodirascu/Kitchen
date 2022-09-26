using Kitchen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cook
    {
        public int Rank { get; set; }
        public int Proficiency { get; set; }
        public string? Name { get; set; }
        public string CatchPhrase { get; set; } = "I like cooking multiple dishes at the same time";
        public Order CookFood(Order order)
        {
            return order;
        }
    }
}
