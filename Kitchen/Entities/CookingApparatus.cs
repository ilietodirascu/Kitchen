using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class CookingApparatus
    {
        public string Name { get; set; }
        public bool IsFull { get; set; }
        public CookingApparatus(string name)
        {
            Name = name;
        }

    }
}
