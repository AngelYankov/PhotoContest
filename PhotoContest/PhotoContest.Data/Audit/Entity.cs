using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Data.Audit
{
    public abstract class Entity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
