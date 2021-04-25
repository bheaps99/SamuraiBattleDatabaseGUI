using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace Repository2
{
    public class CSVRepository : IRepository
    {
        public IEnumerable<Battle> GetAllBattle()
        {
            List<Battle> battles = File.ReadAllLines("BattleData.csv")
                .Skip(1)
                .Select(v => getBattleFromCSV(v))
                .ToList();

            return battles;
        }

        private static Battle getBattleFromCSV(string line)
        {
            Battle battle = new Battle();
            string[] values = line.Split(',');
            battle.BattleId = Convert.ToInt32(values[0]);
            battle.Date = Convert.ToDateTime(values[1]);
            battle.Name = values[2];
            battle.City = values[3];
            battle.Country = values[4];
            return battle;
        }

        public void UpdateBattle(int id, string Name, DateTime Date, string City, string Country)
        {

        }

        public IEnumerable<Samurai> GetAllSamurai()
        {
            List<Samurai> samurais = File.ReadAllLines("SamuraiData.csv")
                .Skip(1)
                .Select(v => getSamuraiFromCSV(v))
                .ToList();

            return samurais;
        }

        public static Samurai getSamuraiFromCSV(string line)
        {
            Samurai samurai = new Samurai();
            string[] values = line.Split(',');
            samurai.SamuraiId = Convert.ToInt32(values[0]);
            samurai.Picture = values[1];
            samurai.Name = values[2];
            samurai.Age = Convert.ToInt32(values[3]);
            samurai.Town = values[4];
            return samurai;
        }

        public Battle GetBattleById(int id) { return null; }
        public Samurai GetSamuraiById(int id) { return null; }

        public void UpdateSamurai(int id, string Name, int Age, string Town)
        {

        }

        public void AddSamuraiToBattle(int SamuraiId, int BattleId)
        {

        }
    }
}
