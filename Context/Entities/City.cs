using System.Collections.Generic;

namespace FinalProject.Context.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Home> Homes { get; set; }
    }
}