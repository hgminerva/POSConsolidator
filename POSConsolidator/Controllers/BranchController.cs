using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POSConsolidator.Models;

namespace POSConsolidator.Controllers
{
    public class EventController : ApiController //web api 2
    {
        private posconsolidatorConnectionString db = new posconsolidatorConnectionString();

        // GET                        
        [Authorize]
        public List<Models.MstBranches> Get()
        {
            var retryCounter = 0;
            List<Models.MstBranches> Sectors;

            while (true)
            {
                try
                {
                    var Leaves = from d in db.MstBranch
                                 orderby d.Id descending
                                 select new Models.MstBranches
                                 {
                                     Id = d.Id,
                                     BranchCode = d.BranchCode,
                                     Branch = d.Branch,
                                     Companyid = d.Companyid,
                                 };
                    if (Leaves.Count() > 0)
                    {
                        Sectors = Leaves.ToList();
                    }
                    else
                    {
                        Sectors = new List<Models.MstBranches>();
                    }
                    break;
                }
                catch
                {
                    if (retryCounter == 3)
                    {
                        Sectors = new List<Models.MstBranches>();
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                    retryCounter++;
                }
            }
            return Sectors;
        }

        // POST api/AddEvent
        [Authorize]
        [Route("api/AddEvent")]
        public int Post(Models.MstBranches Sectors)
        {
            try
            {

                Data.MstBranch NewBranch = new Data.MstBranch();

                SqlDateTime EventDate = new SqlDateTime(new DateTime(Convert.ToDateTime(value.EventDate).Year, +
                                                                     Convert.ToDateTime(value.EventDate).Month, +
                                                                     Convert.ToDateTime(value.EventDate).Day));


                NewEvent.EventDate = EventDate.Value;
                NewEvent.EventDescription = value.EventDescription;
                NewEvent.Particulars = value.Particulars;
                NewEvent.URL = value.URL;
                NewEvent.VideoURL = value.VideoURL;
                NewEvent.EventType = value.EventType;
                NewEvent.IsRestricted = value.IsRestricted;
                NewEvent.IsArchived = value.IsArchived;

                db.MstEvents.InsertOnSubmit(NewEvent);
                db.SubmitChanges();

                return NewEvent.Id;
            }
            catch
            {
                return 0;
            }
        }

    }
}
