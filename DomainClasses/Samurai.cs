using System.Collections.Generic;

namespace DomainClasses
{
    public class Samurai
    {
        public int SamuraiId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }
        public ICollection<Battle> Battles { get; set; }
    }
}
