using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kitchen.Entities
{
    public class Cook
    {
        private int _startingProficiency = 0;
        public string Name { get; set; }
        public int Rank { get; set; }
        private int _profieciency;
        public int Proficiency { get { return _profieciency; } set { _startingProficiency = _startingProficiency == 0 ? value : _startingProficiency; _profieciency = value; } }
        public string CatchPhrase { get; set; } = "I like cooking multiple dishes at once";

        public void PrepareFood(Food food)
        {
            lock (Utility.Lock)
            {
                if (Proficiency >= _startingProficiency) Proficiency--;
                //Task.Delay(food.PreparationTime * 250).ContinueWith(_ =>
                //{
                //    Utility.AddFood(food);
                //    if (Proficiency < _proficiency) Proficiency++;
                //});
                Thread.Sleep(food.PreparationTime * 250);
                Utility.AddFood(food);
                if (Proficiency < _startingProficiency) Proficiency++;
            }
        }
    }
}
