﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public int Complexity { get; set; }
        public string CookingAppratus { get; set; }
    }
}
