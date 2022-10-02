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
        private static object _cookLock = new object();

        public void PrepareFood(Food food)
        {
            lock (_cookLock)
            {
                //Task.Delay(food.PreparationTime * 250).ContinueWith(_ =>
                //{
                //    Utility.AddFood(food);
                //    if (Proficiency < _startingProficiency) Proficiency++;
                //});
                this.Proficiency--;
                Thread.Sleep(food.PreparationTime * 250);
                if (Proficiency < _startingProficiency) Proficiency++;
                Utility.AddFood(food);
            }
        }
        public void DoWork()
        {
            lock (Utility.Lock)
            {
                if (Utility.FoodsToPrepare.IsEmpty) return;
                var potentialFood = Utility.FoodsToPrepare.First();
                if (potentialFood.Complexity > Rank || Proficiency < 1) return;
                Utility.FoodsToPrepare.TryDequeue(out Food food);
                new Thread(() =>
                {
                    this.PrepareFood(food);
                }).Start();
            }
        }
    }
}
