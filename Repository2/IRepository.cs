using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses;

namespace Repository2
{
    public interface IRepository
    {
        IEnumerable<Battle> GetAllBattle();
        Battle GetBattleById(int id);
        void UpdateBattle(int id, string Name, DateTime Date, string City, string Country);
        IEnumerable<Samurai> GetAllSamurai();
        Samurai GetSamuraiById(int id);
        void UpdateSamurai(int id, string Name, int Age, string Town);
        void AddSamuraiToBattle(int SamuraiId, int BattleId);
    }
}
