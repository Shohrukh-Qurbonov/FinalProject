using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Context.Entities
{
    public class Home
    {
        public int Id { get; set; }
        public int Rooms { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public int CreditTerm { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BuildDate { get; set; }
        public double? YearlyPercent { get; set; }

        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        
        public virtual City City { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<HomeImage> HomeImages { get; set; }
    }
}
