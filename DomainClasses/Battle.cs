using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClasses
{
    public class Battle
    {
        public int BattleId { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Samurai> Samurais { get; set; }
    }
}
