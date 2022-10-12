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
        private static object _doWorkLock = new object();

        public void PrepareFood(Food food, ref CookingApparatus cookingApparatus)
        {
            lock (_cookLock)
            {
                Proficiency--;
                Thread.Sleep(food.PreparationTime * 100);
                if (Proficiency < _startingProficiency) Proficiency++;
                if(cookingApparatus != null)cookingApparatus.IsFull = false;
                Utility.AddFood(food);
            }
        }
        public void DoWork()
        {
            lock (_doWorkLock)
            {
                if (!Utility.Orders.Any()) return;
                if (Proficiency < 1) return;
                CookingApparatus cookingApparatus = null;
                var potentialLowFood = Utility.LowComplexityFoodsToPrepare.FirstOrDefault();
                var potentialHighFood = Utility.HighComplexityFoodsToPrepare.FirstOrDefault();
                if (potentialLowFood is null && potentialHighFood is null) return;
                if (Rank >= 2 && potentialHighFood != null && Rank >= potentialHighFood.Complexity)
                {
                    cookingApparatus = Simulation.CookingApparatuses.FirstOrDefault(x => !x.IsFull && x.Name == potentialHighFood.CookingApparatus);
                    if (potentialHighFood.CookingApparatus != null
                        && cookingApparatus is null) return;
                    Utility.HighComplexityFoodsToPrepare.TryDequeue(out Food food);
                    new Thread(() =>
                    {
                        if (cookingApparatus != null)
                        {
                            cookingApparatus.IsFull = true;
                        }
                        PrepareFood(food, ref cookingApparatus);
                    }).Start();
                    return;
                }
                if (potentialLowFood != null && Rank >= potentialLowFood.Complexity)
                {
                    cookingApparatus = Simulation.CookingApparatuses.FirstOrDefault(x => !x.IsFull && x.Name == potentialLowFood.CookingApparatus);
                    if (potentialLowFood.CookingApparatus != null
                        && cookingApparatus is null) return;
                    Utility.LowComplexityFoodsToPrepare.TryDequeue(out Food food);
                    new Thread(() =>
                    {
                        if (cookingApparatus != null)
                        {
                            cookingApparatus.IsFull = true;
                        }
                        PrepareFood(food, ref cookingApparatus);
                    }).Start();
                    return;
                }
            }
        }
    }
}
