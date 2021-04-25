using DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository2.BattleServiceRef;
using Repository2.SamuraiServiceRef;

namespace Repository2
{
    public class DBRepository : IRepository
    {
        BattleServiceClient battleClient = new BattleServiceClient();
        SamuraiServiceClient samuraiClient = new SamuraiServiceClient();
        public void AddSamuraiToBattle(int SamuraiId, int BattleId)
        {
            battleClient.AddSamuraiToBattle(SamuraiId, BattleId);
        }

        public IEnumerable<Battle> GetAllBattle()
        {
            ICollection<Battle> battles = new List<Battle>();
            IEnumerable<BattleServiceRef.BattleData> battleDatas = battleClient.GetAllBattles(); 
            
            foreach(BattleServiceRef.BattleData data in battleDatas)
            {
                Battle battle = new Battle();
                TranslateBattleFromWCFtoBattle(data, battle);
                battles.Add(battle);
            }

            return battles;
        }

        public IEnumerable<Samurai> GetAllSamurai()
        {
            ICollection<Samurai> samurais = new List<Samurai>();
            IEnumerable<SamuraiServiceRef.SamuraiData> samuraiDatas = samuraiClient.GetAllSamurais();

            foreach (SamuraiServiceRef.SamuraiData data in samuraiDatas)
            {
                Samurai samurai = new Samurai();
                TranslateSamuraiFromWCFtoSamurai(data, samurai);
                samurais.Add(samurai);
            }

            return samurais;
        }

        public Battle GetBattleById(int id)
        {
            Battle battle = new Battle();
            BattleServiceRef.BattleData battleData = battleClient.GetBattleById(id);
            TranslateBattleFromWCFtoBattle(battleData, battle);
            return battle;
        }

        public Samurai GetSamuraiById(int id)
        {
            Samurai samurai = new Samurai();
            SamuraiServiceRef.SamuraiData samuraiData = samuraiClient.GetSamuraiById(id);
            TranslateSamuraiFromWCFtoSamurai(samuraiData, samurai);
            return samurai;
        }

        public void UpdateBattle(int id, string Name, DateTime Date, string City, string Country)
        {
            if(Name != null)
            battleClient.UpdateBattle(id, Name, Date, City, Country);
        }

        public void UpdateSamurai(int id, string Name, int Age, string Town)
        {
            if(Name != null)
            samuraiClient.UpdateSamurai(id, Name, Age, Town);
        }

        private void TranslateBattleFromWCFtoBattle(BattleServiceRef.BattleData battleData, Battle battle)
        {
            battle.BattleId = battleData.BattleId;
            battle.Name = battleData.Name;
            battle.Date = battleData.Date;
            battle.City = battleData.City;
            battle.Country = battleData.Country;
            battle.Samurais = new List<Samurai>();


            ICollection<Samurai> samurais = new List<Samurai>();
            IEnumerable<BattleServiceRef.SamuraiData> samuraiDatas = battleClient.GetSamuraisInBattle(battleData.BattleId).ToList();

            foreach (BattleServiceRef.SamuraiData data in samuraiDatas)
            {
                Samurai _samurai = new Samurai();
                _samurai.SamuraiId = data.SamuraiId;
                _samurai.Name = data.Name;
                _samurai.Age = data.Age;
                _samurai.Town = data.Town;
                _samurai.Picture = data.Picture;
                battle.Samurais.Add(_samurai);
            }
            //samuraiClient.GetBattlesOfSamurai(samuraiData.SamuraiId);
        }

        private void TranslateSamuraiFromWCFtoSamurai(SamuraiServiceRef.SamuraiData samuraiData, Samurai samurai)
        {
            samurai.SamuraiId = samuraiData.SamuraiId;
            samurai.Name = samuraiData.Name;
            samurai.Age = samuraiData.Age;
            samurai.Town = samuraiData.Town;
            samurai.Picture = samuraiData.Picture;
            samurai.Battles = new List<Battle>();

            ICollection<Battle> battles = new List<Battle>();
            IEnumerable<SamuraiServiceRef.BattleData> battleDatas = samuraiClient.GetBattlesOfSamurai(samuraiData.SamuraiId).ToList();

            foreach (SamuraiServiceRef.BattleData data in battleDatas)
            {
                Battle _battle = new Battle();
                _battle.BattleId = data.BattleId;
                _battle.Name = data.Name;
                _battle.City = data.City;
                _battle.Country = data.Country;
                _battle.Date = data.Date;
                samurai.Battles.Add(_battle);
            }
            //samuraiClient.GetBattlesOfSamurai(samuraiData.SamuraiId);
        }
    }
}
