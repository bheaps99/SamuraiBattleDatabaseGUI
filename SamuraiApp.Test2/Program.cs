using DomainClasses;
using Repository2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            DBRepository repository = new DBRepository();
            var watch = new System.Diagnostics.Stopwatch();
            /*
            watch.Start();
            Console.WriteLine("Time starts now");
            IEnumerable<Battle> battles = repository.GetAllBattle();
            //IEnumerable<Samurai> samurais = repository.GetAllSamurai();
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            Console.WriteLine($"Execution Time for getAll: {ts.Seconds} seconds");
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Printing starts now");
            watch.Reset();
            watch.Start();
            PrintBattle(battles);
            //PrintSamurai(samurais);
            watch.Stop();
            ts = watch.Elapsed;
            Console.WriteLine($"Execution Time: {ts.Seconds} seconds");
            Console.ReadLine();
            */
                
            Battle battle = repository.GetBattleById(2);
            Console.WriteLine(battle.Name);
            Console.ReadLine();


        }

        private static void PrintBattle(IEnumerable<Battle> battles)
        {
            foreach (Battle _battle in battles)
            {
                Console.WriteLine("Samurai name is " + _battle.Name + " and age is " + _battle.City);
                foreach (Samurai samurai in _battle.Samurais)
                {
                    Console.WriteLine("Samurai " + samurai.Name + "is in battle " + _battle.Name);
                }
            }
            //Console.ReadLine();
        }

        private static void PrintSamurai(IEnumerable<Samurai> samurais)
        {
            foreach (Samurai _samurai in samurais)
            {
                Console.WriteLine("Samurai name is " + _samurai.Name + " and age is " + _samurai.Town);
                foreach (Battle battle in _samurai.Battles)
                {
                    Console.WriteLine("Battle  " + battle.Name + "has " + _samurai.Name);
                }
            }
            //Console.ReadLine();
        }
    }
}
