using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSConsolidator.Models
{
    public class TrnSales
    {
        public int Id { get; set; }
        public string SalesDate { get; set; }
        public string SalesNumber { get; set; }
        public string ORNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal VATSales { get; set; }
        public decimal NonVATSales { get; set; }
        public decimal VATAmount { get; set; }
        public string Customer { get; set; }
        public string Discount { get; set; }
        public string SeniorCitizenId { get; set; }
        public string SeniorCitizenName { get; set; }
        public int SeniorCitizenAge { get; set; }
        public string Remarks { get; set; }
        public string Terminal { get; set; }
        public bool IsCancelled { get; set; }
    }
}