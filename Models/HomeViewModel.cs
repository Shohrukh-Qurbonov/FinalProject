using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class HomeViewModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int CreditTerm { get; set; }
        public string Address { get; set; }
        public string ImagePath{ get; set; }
        public string Description { get; set; }
    }
}
