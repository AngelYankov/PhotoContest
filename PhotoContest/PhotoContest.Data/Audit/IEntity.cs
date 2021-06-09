using System;

namespace PhotoContest.Data.Audit
{
    public interface IEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
        DateTime DeletedOn { get; set; }
        bool IsDeleted { get; set; }
    }
}
