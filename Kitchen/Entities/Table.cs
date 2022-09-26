using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Table
    {
        public int Number { get; set; }
        public Table(int number)
        {
            Number = number;
        }
    }
}
