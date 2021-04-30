using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class EditHomeViewModel
    {
        public int Id { get; set; }
        public int Rooms { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public DateTime BuildDate { get; set; }
        public IFormFileCollection Images { get; set; }
        public Dictionary<int,string> PrevImages { get; set; }

        public int CategoryId { get; set; }
        public int CityId { get; set; }

        public Dictionary<int, string> Cities { get; set; }
        public Dictionary<int, string> Categories { get; set; }
    }
}
