using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoContest.Services.Services
{
    public class ContestService : IContestService
    {
        private readonly PhotoContestContext dbContext;
        public ContestService(PhotoContestContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ContestDTO Create(NewContestDTO dto)
        {
            var newContest = new Contest();

            newContest.Name = dto.Name;

            var category = this.dbContext.Categories.FirstOrDefault(c => c.Id == dto.CategoryId)
                ?? throw new ArgumentNullException();
            newContest.CategoryId = dto.CategoryId;

            var status = this.dbContext.Statuses.FirstOrDefault(s => s.Id == dto.StatusId)
                ?? throw new ArgumentNullException();
            newContest.StatusId = dto.StatusId;

            newContest.CreatedOn = DateTime.UtcNow;
            this.dbContext.Contests.Add(newContest);
            this.dbContext.SaveChanges();
            var createdContest = FindContest(newContest.Id);

            return new ContestDTO(createdContest);
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContestDTO> GetAll()
        {
            return this.dbContext
                      .Contests
                      .Include(c => c.Category)
                      .Include(a => a.Status)
                      .Where(c => c.IsDeleted == false)
                      .Select(c => new ContestDTO(c));
        }

        public ContestDTO Update(Guid id, Contest contest)
        {
            throw new NotImplementedException();
        }

        private Contest FindContest(Guid id)
        {
            return this.dbContext
                .Contests
                .Where(c => c.IsDeleted == false)
                .FirstOrDefault(c => c.Id == id)
                ?? throw new ArgumentException();
        }
    }
}
