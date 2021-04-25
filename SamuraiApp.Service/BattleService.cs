using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Service
{
    public class BattleService : IBattleService
    {
        public IEnumerable<BattleData> GetAllBattles()
        {
            using (var ctx = new SamuraiAppEntities())
            {
                var battles = from b in ctx.BattleEntities
                               select new BattleData
                               {
                                   BattleId = b.BattleId,
                                   Name = b.Name,
                                   City = b.City,
                                   Date = b.Date,
                                   Country = b.Country,
                                   //Samurais = GetSamuraisInBattle(b.BattleId),
                               };                               
                return battles.ToList();
            }
        }

        public BattleData GetBattleById(int id)
        {
            BattleData battle = null;

            using (var context = new SamuraiAppEntities())
            {
                var foundBattle = GetBattleEntity(context, id);
                if (foundBattle != null)
                    battle = GetBattleFromBattleEntity(foundBattle);
            }
            return battle;
        }

        private BattleData GetBattleFromBattleEntity(BattleEntity battleEntity)
        {
            var samurai = new BattleData()
            {
                BattleId = battleEntity.BattleId,
                Name = battleEntity.Name,
                Date = battleEntity.Date,
                City = battleEntity.City,
                Country = battleEntity.Country,
                //Battles = samuraiEntity.Battles
            };
            return samurai;
        }

        private BattleEntity GetBattleEntity(SamuraiAppEntities context, int id)
        {
            BattleEntity foundBattle = null;
            foundBattle = (from s in context.BattleEntities
                            where s.BattleId == id
                            select s).FirstOrDefault();
            return foundBattle;
        }

        public void UpdateBattle(int id, string name, DateTime dateStarted, string city, string country)
        {
            using(var context = new SamuraiAppEntities())
            {
                var battle = context.BattleEntities.First(b => b.BattleId == id);
                if (battle == null)
                    return;
                battle.Name = name;
                battle.Date = dateStarted;
                battle.City = city;
                battle.Country = country;
                context.SaveChanges();
            }
        }

        public IEnumerable<SamuraiData> GetSamuraisInBattle(int battleId)
        {

            using (var ctx = new SamuraiAppEntities())
            {
                var battleSamurais = ctx.BattleSamuraiEntities.Where(bs => bs.BattleId== battleId);
                ICollection<SamuraiData> samurais = new List<SamuraiData>();

                foreach (BattleSamuraiEntity battleSamurai in battleSamurais)
                {
                    SamuraiEntity samurai = (from s in ctx.SamuraiEntities
                                           where s.SamuraiId == battleSamurai.SamuraiId
                                           select s).FirstOrDefault();

                    SamuraiData samuraiData = new SamuraiData()
                    {
                        SamuraiId = samurai.SamuraiId,
                        Name = samurai.Name,
                        Age = samurai.Age,
                        Picture = samurai.Picture,
                        Town = samurai.Town,
                    };

                    samurais.Add(samuraiData);
                }

                return samurais;
            }

        }

        public void AddSamuraiToBattle(int samuraiId, int battleId)
        {
            using (var context = new SamuraiAppEntities())
            {
                var samuraiInBattle = new BattleSamuraiEntity
                {
                    SamuraiId = samuraiId,
                    BattleId = battleId,
                };
                context.BattleSamuraiEntities.Add(samuraiInBattle);
                context.SaveChanges();
            }
        }
    }
}
