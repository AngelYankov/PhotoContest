using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class ContestService : IContestService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IMapper mapper;
        public ContestService(PhotoContestContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ContestDTO> CreateAsync(NewContestDTO dto)
        {
            //newContest.Name = dto.Name;
            //newContest.CategoryId = dto.CategoryId;
            //newContest.StatusId = dto.StatusId;
            if (dto.Name == null) throw new ArgumentException();
            
            var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId)
                ?? throw new ArgumentNullException();

            var status = await this.dbContext.Statuses.FirstOrDefaultAsync(s => s.Id == dto.StatusId)
                ?? throw new ArgumentNullException();

            if (dto.Phase1 == null || dto.Phase1 < DateTime.UtcNow) throw new ArgumentException();
            if (dto.Phase2 == null || dto.Phase2 <= dto.Phase1 || dto.Phase2 > dto.Phase1.AddDays(31)) ;
            if (dto.Finished == null || dto.Finished <= dto.Phase2.AddHours(1) || dto.Finished > dto.Phase2.AddHours(24));

            var newContest = this.mapper.Map<Contest>(dto);

            newContest.CreatedOn = DateTime.UtcNow;
            await this.dbContext.Contests.AddAsync(newContest);
            await this.dbContext.SaveChangesAsync();

            return new ContestDTO(newContest);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var contest = await FindContestAsync(id);
            contest.DeletedOn = DateTime.UtcNow;
            contest.IsDeleted = true;
            await this.dbContext.SaveChangesAsync();
            return contest.IsDeleted;
        }

        public async Task<IEnumerable<ContestDTO>> GetAllAsync()
        {
            return await this.dbContext
                             .Contests
                             .Include(c => c.Category)
                             .Include(a => a.Status)
                             .Where(c => c.IsDeleted == false)
                             .Select(c => new ContestDTO(c))
                             .ToListAsync();
        }

        public async Task<ContestDTO> UpdateAsync(Guid id, Contest dto)
        {
            var contest = await FindContestAsync(id);
            contest.Name = dto.Name ?? contest.Name;

            
        }

        private async Task<Contest> FindContestAsync(Guid id)
        {
            return await this.dbContext
                             .Contests
                             .Where(c => c.IsDeleted == false)
                             .FirstOrDefaultAsync(c => c.Id == id)
                             ?? throw new ArgumentException();
        }
    }
}
