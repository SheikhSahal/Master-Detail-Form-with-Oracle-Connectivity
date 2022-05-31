using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Master_detail.Models
{
    public class MergeModelDetails
    {
        public List<EMP> EMPlist { get; set; }
        public EMP EMP { get; set; }
        public EMP EMPNO { get; set; }
    }
}