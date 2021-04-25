using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SamuraiApp.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SamuraiService : ISamuraiService
    {
        public IEnumerable<SamuraiData> GetAllSamurais()
        {
            using (var ctx = new SamuraiAppEntities())
            {
                var samurais = from s in ctx.SamuraiEntities
                               select new SamuraiData
                               {
                                   SamuraiId = s.SamuraiId,
                                   Name = s.Name,
                                   Age = s.Age,
                                   Town = s.Town,
                                   Picture = s.Picture,
                               };
                return samurais.ToList();
            }
        }

        public SamuraiData GetSamuraiById(int id)
        {
            SamuraiData samurai = null;

            using (var context = new SamuraiAppEntities())
            {
                var foundSamurai = GetSamuraiEntity(context, id);
                if (foundSamurai != null)
                    samurai = GetSamuraiFromSamuraiEntity(foundSamurai);
            }
            return samurai;
        }

       
        public void UpdateSamurai(int id, string name, int age, string town)
        {
            using (var context = new SamuraiAppEntities())
            {
                var samurai = context.SamuraiEntities.First(s => s.SamuraiId == id);
                if (samurai == null)
                    return;
                samurai.Name = name;
                samurai.Age = age;
                samurai.Town = town;
                context.SaveChanges();
            }
        }

        private SamuraiData GetSamuraiFromSamuraiEntity(SamuraiEntity samuraiEntity)
        {
            var samurai = new SamuraiData()
            {
                SamuraiId = samuraiEntity.SamuraiId,
                Name = samuraiEntity.Name,
                Age = samuraiEntity.Age,
                Town = samuraiEntity.Town,
                Picture = samuraiEntity.Picture,
                //BattlesOfSamurai = GetAllBattlesOfSamurai(samuraiEntity.SamuraiId),
            };
            return samurai;
        }

        private SamuraiEntity GetSamuraiEntity(SamuraiAppEntities context, int id)
        {
            SamuraiEntity foundSamurai = null;
            foundSamurai = (from s in context.SamuraiEntities
                            where s.SamuraiId == id
                            select s).FirstOrDefault();
            return foundSamurai;
        }

        
        public ICollection<BattleData> GetBattlesOfSamurai(int samuraiId)
        {

            using (var ctx = new SamuraiAppEntities())
            {
                var battleSamurais = ctx.BattleSamuraiEntities.Where(bs => bs.SamuraiId == samuraiId);
                ICollection<BattleData> battles = new List<BattleData>();

                foreach(BattleSamuraiEntity battleSamurai in battleSamurais)
                {
                    BattleEntity battle = (from b in ctx.BattleEntities
                                          where b.BattleId == battleSamurai.BattleId
                                          select b).FirstOrDefault();

                    BattleData battleData = new BattleData()
                    {
                        BattleId = battle.BattleId,
                        Name = battle.Name,
                        City = battle.City,
                        Country = battle.Country,
                        Date = battle.Date
                    };

                    battles.Add(battleData);
                }

                return battles;
            }

        }
    }
}
