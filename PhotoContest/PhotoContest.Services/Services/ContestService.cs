using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
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

            ValidatePhase1(dto.Phase1);
            ValidatePhase2(dto.Phase2, dto.Phase1);
            ValidatePhase3(dto.Finished, dto.Phase2);

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

        public async Task<ContestDTO> UpdateAsync(Guid id, UpdateContestDTO dto)
        {
            var contest = await FindContestAsync(id);
            contest.Name = dto.Name ?? contest.Name;
            if (dto.CategoryId != Guid.Empty)
            {
                var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId)
                ?? throw new ArgumentNullException();
                contest.CategoryId = dto.CategoryId;
            }
            if (dto.StatusId != Guid.Empty)
            {
                var status = await this.dbContext.Statuses.FirstOrDefaultAsync(s => s.Id == dto.StatusId)
                                ?? throw new ArgumentNullException();
                contest.StatusId = dto.StatusId;
            }
            if (dto.Phase1 != DateTime.MinValue)
            {
                ValidatePhase1(dto.Phase1);
                contest.Phase1 = dto.Phase1;
            }
            if (dto.Phase2 != DateTime.MinValue)
            {
                ValidatePhase2(dto.Phase2, dto.Phase1);
                contest.Phase2 = dto.Phase2;
            }
            if (dto.Finished != DateTime.MinValue)
            {
                ValidatePhase3(dto.Finished, dto.Phase2);
                contest.Finished = dto.Finished;
            }

            contest.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return new ContestDTO(contest);
        }

        private void ValidatePhase1(DateTime date)
        {
            if (date == DateTime.MinValue || date < DateTime.UtcNow) throw new ArgumentException();
        }

        private void ValidatePhase2(DateTime date1, DateTime date2)
        {
            if (date1 == DateTime.MinValue || date1 <= date2 || date1 > date2.AddDays(31)) throw new ArgumentException();
        }
        private void ValidatePhase3(DateTime date1, DateTime date2)
        {
            if (date1 == DateTime.MinValue || date1 <= date2.AddHours(1) || date1 > date2.AddHours(24)) throw new ArgumentException();
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
