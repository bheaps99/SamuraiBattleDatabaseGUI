using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamuraiApp.Test.SamuraiServiceRef;
using Repository2;
using DomainClasses;

namespace SamuraiApp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            SamuraiServiceClient samuraiClient = new SamuraiServiceClient();

            SamuraiData samurai = samuraiClient.GetSamuraiById(5);
            Console.WriteLine("Samurai name is " + samurai.Name + " and age is " + samurai.Age);

            IEnumerable<SamuraiData> samuraiDatas = samuraiClient.GetAllSamurais();

            foreach(SamuraiData _samurai in samuraiDatas)
            {
                Console.WriteLine("Samurai name is " + _samurai.Name + " and age is " + _samurai.Age);
            }


            Console.ReadLine();
            */
            Repository repository = new Repository();
            Samurai samurai = repository.GetSamuraiById(2);
            Console.WriteLine(samurai.Name);
            Console.ReadLine(); 
        }
    }
}
