using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Cook
    {
        private int _proficiency = 4;
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Proficiency { get; set; }
        public string CatchPhrase { get; set; } = "I like cooking multiple dishes at once";

        public void PrepareFood(Food food)
        {
            if (Proficiency >= _proficiency) Proficiency--;
            Task.Delay(food.PreparationTime * 250).ContinueWith(_ =>
            {
                Utility.AddFood(food);
                if (Proficiency < _proficiency) Proficiency++;
            });
        }
    }
}
