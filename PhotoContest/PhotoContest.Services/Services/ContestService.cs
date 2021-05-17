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
        public async Task<IEnumerable<ContestDTO>> GetByUser(string username, string filter)
        {
            var allContests = await this.dbContext
                                        .Contests
                                        .Include(c => c.Category)
                                        .Include(c => c.Status)
                                        .Include(c => c.UserContests)
                                            .ThenInclude(uc => uc.User)
                                        .SelectMany(c => c.UserContests)
                                        .Where(u => u.User.UserName == username)
                                        .Select(u => new ContestDTO(u.Contest))
                                        .ToListAsync();
            var filteredContests = new List<ContestDTO>();
            if (filter.Equals("open", StringComparison.OrdinalIgnoreCase))
            {
                return allContests.Where(c => c.Status != "Finished");
            }
            else if (filter.Equals("closed", StringComparison.OrdinalIgnoreCase))
            {
                return allContests.Where(c => c.Status == "Finished");
            }
            else if (filter == null)
            {
                return allContests;
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public async Task<IEnumerable<ContestDTO>> GetByPhaseAsync(string phaseName, string sortBy, string order)
        {
            var allContests = await this.dbContext
                                        .Contests
                                        .Include(c => c.Category)
                                        .Include(c => c.Status)
                                        .Where(c => c.IsDeleted == false)
                                        .ToListAsync();
            var filteredContests = new List<ContestDTO>();

            if (phaseName.Equals("phase1", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var contest in allContests)
                {
                    if (contest.Status.Name == "Phase1")
                    {
                        var contestDTO = new ContestDTO(contest);
                        filteredContests.Add(contestDTO);
                    }
                }
            }
            if (phaseName.Equals("phase2", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var contest in allContests)
                {
                    if (contest.Status.Name == "Phase2")
                    {
                        var contestDTO = new ContestDTO(contest);
                        filteredContests.Add(contestDTO);
                    }
                }
            }
            if (phaseName.Equals("finished", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var contest in allContests)
                {
                    if (contest.Status.Name == "Finished")
                    {
                        var contestDTO = new ContestDTO(contest);
                        filteredContests.Add(contestDTO);
                    }
                }
            }
            if (filteredContests.Count == 0 && sortBy == null) throw new ArgumentException();
            if (filteredContests.Count != 0)
            {
                Sorting(filteredContests, sortBy, order);
            }
            if (filteredContests.Count == 0 && sortBy != null)
            {
                Sorting(filteredContests, sortBy, order);
            }
            return filteredContests;
        }
        private void Sorting(List<ContestDTO> filteredContests, string sortBy, string order)
        {
            if (sortBy == "name")
            {
                switch (order)
                {
                    case null:
                        filteredContests = filteredContests.OrderBy(c => c.Name).ToList();
                        break;
                    case "asc":
                        filteredContests = filteredContests.OrderBy(c => c.Name).ToList();
                        break;
                    case "desc":
                        filteredContests = filteredContests.OrderByDescending(c => c.Name).ToList();
                        break;
                }
            }
            if (sortBy == "category")
            {
                switch (order)
                {
                    case null:
                        filteredContests = filteredContests.OrderBy(c => c.Category).ToList();
                        break;
                    case "asc":
                        filteredContests = filteredContests.OrderBy(c => c.Category).ToList();
                        break;
                    case "desc":
                        filteredContests = filteredContests.OrderByDescending(c => c.Category).ToList();
                        break;
                }
            }
            if (sortBy == "newest")
            {
                filteredContests = filteredContests.OrderBy(c => c.Phase1).ToList();
            }
            if (sortBy == "oldest")
            {
                filteredContests = filteredContests.OrderByDescending(c => c.Finished).ToList();
            }
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
