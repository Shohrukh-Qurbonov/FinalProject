using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class HomeDetailsViewModel
    {
        public int Id { get; set; }
        public int Rooms { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public int CreditTerm { get; set; }
        public DateTime BuildDate { get; set; }
        public double? YearlyPercent { get; set; }

        public string CategoryName { get; set; }
        public string CityName { get; set; }
    }
}
