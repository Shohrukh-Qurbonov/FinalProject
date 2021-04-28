using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Context.Entities
{
    public class HomeImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        
        public int HomeId { get; set; }

        public virtual Home Home { get; set; }
    }
}
