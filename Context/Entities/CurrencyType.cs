using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Context.Entities
{
    public class CurrencyType
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public double? YearlyPercent { get; set; }

        public int HomeId { get; set; }

        public virtual Home Home { get; set; }
    }
}
