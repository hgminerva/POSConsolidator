using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace POSConsolidator.Models
{
    public class Branches
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public int CompanyId { get; set; }
    }

    public class posconsolidatorConnectionString : DbContext
    {
        public DbSet<Branches> MstBranches { get; set; }
    }

}