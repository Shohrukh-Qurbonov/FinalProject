using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Context.Entities
{
    public class Request
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FIO { get; set; }
        public string PurchaseType { get; set; }
        public DateTime RequestDate { get; set; }
        public int HomeId { get; set; }
        public string Aim { get; set; }
        public int? CreditTerm { get; set; }
        public double? CreditSumm { get; set; }
        public double? Prepayment { get; set; }
        public int CurrencyTypeId { get; set; }
    }
}
