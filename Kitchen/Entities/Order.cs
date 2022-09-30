using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int[] Items { get; set; }
        public int Priority { get; set; }
        public int MaxWait { get; set; }
        public bool IsBeingCooked { get; set; }
        public Table Table { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
    }
}
