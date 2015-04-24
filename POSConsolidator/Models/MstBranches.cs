using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace POSConsolidator.Models
{
    public class MstBranches
    {
        public int Id { get; set; }             //Fields in the D
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public int Companyid { get; set; }

    }

    public class posconsolidatorConnectionString : DbContext       //must have same name as Database
    {
        public DbSet<MstBranches> MstBranch { get; set; }
    }

}