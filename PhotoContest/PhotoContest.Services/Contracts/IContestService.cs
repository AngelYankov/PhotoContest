using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;

namespace PhotoContest.Services.Services
{
    public interface IContestService
    {
        ContestDTO Create(NewContestDTO dto);
        IEnumerable<ContestDTO> GetAll();
        ContestDTO Update(Guid id, Contest contest);
        bool Delete(Guid id);
    }
}