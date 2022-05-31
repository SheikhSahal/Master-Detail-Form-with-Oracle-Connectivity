using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Master_detail.Models
{
    public class Dept
    {
        public int Deptid { get; set; }
        public string Dname { get; set; }
        public string Location { get; set; }

        public virtual ICollection<EMP> EMP { get; set; }
    }
}