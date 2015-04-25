using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSConsolidator.Controllers
{
    public class BranchController : ApiController
    {
        Data.POSConsolidatorDBDataContext xy = new Data.POSConsolidatorDBDataContext();

        // GET api/Event                        
        [Authorize]
        public List<Models.Branches> Get()
        {
            var retryCounter = 0;
            List<Models.Branches> values;

            while (true)
            {
                try
                {
                    var Leaves = from x in xy.MstBranches
                                 orderby x.Id descending
                                 select new Models.Branches
                                 {
                                     Id = x.Id,
                                     BranchCode = x.BranchCode,
                                     Branch = x.Branch,
                                     CompanyId = x.CompanyId,
                                 };
                    if (Leaves.Count() > 0)
                    {
                        values = Leaves.ToList();
                    }
                    else
                    {
                        values = new List<Models.Branches>();
                    }
                    break;
                }
                catch
                {
                    if (retryCounter == 3)
                    {
                        values = new List<Models.Branches>();
                        break;
                    }

                    System.Threading.Thread.Sleep(1000);
                    retryCounter++;
                }
            }
            return values;
        }

        // POST api/AddEvent
        [Authorize]
        [Route("api/AddEvent")]
        public int Post(Models.Branches value)
        {
            try
            {

                Data.MstBranch NewBranch = new Data.MstBranch();

                NewBranch.BranchCode = value.BranchCode;
                NewBranch.Branch = value.Branch;
                NewBranch.CompanyId = value.CompanyId;

                xy.MstBranches.InsertOnSubmit(NewBranch);
                xy.SubmitChanges();

                return NewBranch.Id;
            }
            catch
            {
                return 0;
            }
        }

        // PUT /api/UpdateEvent/5
        [Authorize]
        [Route("api/UpdateEvent/{Id}")]
        public HttpResponseMessage Put(String Id, Models.Branches value)
        {
            Id = Id.Replace(",", "");
            int id = Convert.ToInt32(Id);

            try
            {
                var Leaves = from x in xy.MstBranches where x.Id == id select x;

                if (Leaves.Any())
                {
                    var UpdateBranch = Leaves.FirstOrDefault();

                    UpdateBranch.BranchCode = value.BranchCode;
                    UpdateBranch.Branch = value.Branch;
                    UpdateBranch.CompanyId = value.CompanyId;

                    xy.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (NullReferenceException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/DeleteEvent/5
        [Authorize]
        [Route("api/DeleteEvent/{Id}")]
        public HttpResponseMessage Delete(int Id)
        {
            Data.MstBranch DeleteEvent = xy.MstBranches.Where(x => x.Id == Id).First();

            if (DeleteEvent != null)
            {
                xy.MstBranches.DeleteOnSubmit(DeleteEvent);
                try
                {
                    xy.SubmitChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                catch
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

    }
}
