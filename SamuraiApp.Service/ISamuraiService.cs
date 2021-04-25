using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SamuraiApp.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISamuraiService
    {
        [OperationContract]
        IEnumerable<SamuraiData> GetAllSamurais();

        [OperationContract]
        SamuraiData GetSamuraiById(int id);

        [OperationContract]
        void UpdateSamurai(int id, string name, int age, string town);
        
        [OperationContract]
        ICollection<BattleData> GetBattlesOfSamurai(int samuraiId);
        
        

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "SamuraiApp.Service.ContractType".
    [DataContract]
    public class SamuraiData
    {
        [DataMember]
        public int SamuraiId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string Picture { get; set; }

        [DataMember]
        public string Town { get; set; }
        
        /*
        [DataMember]
        ICollection<BattleData> BattlesOfSamurai { get; set; }
        */
    }
}
