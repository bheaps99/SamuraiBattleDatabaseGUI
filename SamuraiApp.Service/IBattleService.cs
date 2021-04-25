using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Service
{
    [ServiceContract]
    public interface IBattleService
    {
        [OperationContract]
        IEnumerable<BattleData> GetAllBattles();

        [OperationContract]
        BattleData GetBattleById(int id);

        [OperationContract]
        void UpdateBattle(int id, string name, DateTime dateStarted, string town, string country);

        [OperationContract]
        IEnumerable<SamuraiData> GetSamuraisInBattle(int battleId);

        [OperationContract]
        void AddSamuraiToBattle(int samuraiId, int battleId);
    }

    [DataContract]
    public class BattleData
    {
        [DataMember]
        public int BattleId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }
        
        /*
        [DataMember]
        public IEnumerable<SamuraiData> Samurais { get; set; }
        */
        


       
    }
}
