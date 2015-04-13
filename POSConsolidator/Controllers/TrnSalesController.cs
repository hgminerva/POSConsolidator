using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSConsolidator.Controllers
{
    public class TrnSalesController : ApiController
    {
        private Data.POSConsolidatorDBDataContext db = new Data.POSConsolidatorDBDataContext();

        // GET api/TrnSales
        public List<Models.TrnSales> Get()
        {
            var retryCounter = 0;
            List<Models.TrnSales> values;

            while (true)
            {
                try
                {
                    var TrnSales = from d in db.TrnSales
                                   select new Models.TrnSales
                                   {
                                        Id = d.Id,
                                        SalesDate = d.SalesDate.ToShortDateString(),
                                        SalesNumber = d.SalesNumber,
                                        ORNumber = d.ORNumber,
                                        Amount = d.Amount,
                                        VATSales = d.VATSales,
                                        NonVATSales = d.NonVATSales,
                                        VATAmount = d.VATAmount,
                                        Customer = d.Customer,
                                        Discount = d.Discount,
                                        Remarks = d.Remarks,
                                        Terminal = d.Terminal,
                                        IsCancelled = d.IsCancelled
                                   };

                    if (TrnSales.Count() > 0)
                    {
                        values = TrnSales.ToList();
                    }
                    else
                    {
                        values = new List<Models.TrnSales>();
                    }
                    break;
                }
                catch
                {
                    if (retryCounter == 3)
                    {
                        values = new List<Models.TrnSales>();
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                    retryCounter++;
                }
            }
            return values;
        }
    }
}