using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.ExceptionMessages;
using PhotoContest.Services.Models;
using PhotoContest.Services.Models.Create;
using PhotoContest.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class ContestService : IContestService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IHttpContextAccessor contextAccessor;

        public ContestService(PhotoContestContext dbContext, IHttpContextAccessor contextAccessor)
        {
            this.dbContext = dbContext;
            this.contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Create a contest.
        /// </summary>
        /// <param name="dto">Details of the contest to be created.</param>
        /// <returns>Returns the created contest or an appropriate error message.</returns>
        public async Task<ContestDTO> CreateAsync(NewContestDTO dto)
        {
            var newContest = new Contest();

            newContest.Name = dto.Name ?? throw new ArgumentException(Exceptions.RequiredContestName);

            var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == dto.CategoryName.ToLower())
                ?? throw new ArgumentException(Exceptions.InvalidCategory);
            newContest.CategoryId = category.Id;

            var status = await this.dbContext.Statuses.FirstOrDefaultAsync(s => s.Name == "Phase 1");

            newContest.StatusId = status.Id;

            newContest.Open = dto.Open;

            ValidatePhase1(DateTime.ParseExact(dto.Phase1, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture));
            newContest.Phase1 = DateTime.ParseExact(dto.Phase1, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture);
            ValidatePhase2(DateTime.ParseExact(dto.Phase2, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture), DateTime.ParseExact(dto.Phase1, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture));
            newContest.Phase2 = DateTime.ParseExact(dto.Phase2, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture);
            ValidateFinished(DateTime.ParseExact(dto.Finished, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture), DateTime.ParseExact(dto.Phase2, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture));
            newContest.Finished = DateTime.ParseExact(dto.Finished, "dd-MM-yyyy HH:mm tt", CultureInfo.InvariantCulture);

            newContest.CreatedOn = DateTime.UtcNow;
            await this.dbContext.Contests.AddAsync(newContest);
            await this.dbContext.SaveChangesAsync();

            return new ContestDTO(newContest);
        }

        /// <summary>
        /// Delere a contest by certain ID.
        /// </summary>
        /// <param name="id">ID of the contest to delete.</param>
        /// <returns>Returns a boolean value if the contest is deleted.</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var contest = await FindContestAsync(id);
            contest.DeletedOn = DateTime.UtcNow;
            contest.IsDeleted = true;
            await this.dbContext.SaveChangesAsync();
            return contest.IsDeleted;
        }

        /// <summary>
        /// Get all contests.
        /// </summary>
        /// <returns>Returns all contests.</returns>
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

        /// <summary>
        /// Get all open contests.
        /// </summary>
        /// <returns>Returns all open contests.</returns>
        public async Task<IEnumerable<ContestDTO>> GetAllOpenAsync()
        {
            return await this.dbContext
                             .Contests
                             .Include(c => c.Category)
                             .Include(a => a.Status)
                             .Where(c => c.IsDeleted == false && (c.Status.Name == "Phase1"))
                             .Select(c => new ContestDTO(c))
                             .ToListAsync();
        }

        /// <summary>
        /// Enroll user to contest
        /// </summary>
        /// <param name="username">Username of the user to enroll.</param>
        /// <param name="contestName">Name of the contest to enroll in.</param>
        /// <returns>Return true if successful or an appropriate error message.</returns>
        public async Task<bool> Enroll(string contestName)
        {
            var contest = await this.dbContext.Contests.FirstOrDefaultAsync(c => c.Name.ToLower() == contestName.ToLower())
                ?? throw new ArgumentException(Exceptions.InvalidContestName);

            var username = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.dbContext.Users.FirstAsync(u => u.Email == username);

            if (await this.dbContext.UserContests.AnyAsync(uc => uc.UserId == user.Id && uc.ContestId == contest.Id))
            {
                throw new ArgumentException(Exceptions.EnrolledUser);
            }

            var userContest = new UserContest();
            userContest.ContestId = contest.Id;
            userContest.UserId = user.Id;
            await this.dbContext.UserContests.AddAsync(userContest);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Update a contest by a certain ID and data.
        /// </summary>
        /// <param name="id">ID of the contest to update.</param>
        /// <param name="dto">Details of the contest to be updated.</param>
        /// <returns>Returns the updated contest or an appropriate error message.</returns>
        public async Task<ContestDTO> UpdateAsync(Guid id, UpdateContestDTO dto)
        {
            var contest = await FindContestAsync(id);
            contest.Name = dto.Name ?? contest.Name;
            if (dto.CategoryId != Guid.Empty)
            {
                var category = await this.dbContext.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId)
                ?? throw new ArgumentException(Exceptions.InvalidCategory);
                contest.CategoryId = dto.CategoryId;
            }
            if (dto.StatusId != Guid.Empty)
            {
                var status = await this.dbContext.Statuses.FirstOrDefaultAsync(s => s.Id == dto.StatusId)
                                ?? throw new ArgumentException(Exceptions.InvalidStatus);
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
                ValidateFinished(dto.Finished, dto.Phase2);
                contest.Finished = dto.Finished;
            }
            if (dto.Open != false)
            {
                contest.Open = true;
            }

            contest.ModifiedOn = DateTime.UtcNow;
            await this.dbContext.SaveChangesAsync();
            return new ContestDTO(contest);
        }

        /// <summary>
        /// Filter and/or sort contests by username.
        /// </summary>
        /// <param name="username">Username for which we are filtering the contests.</param>
        /// <param name="filter">Value of the filter.</param>
        /// <returns>Returns the contests that correspond to the filter.</returns>
        public async Task<IEnumerable<ContestDTO>> GetByUserAsync(string filter)
        {
            var username = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.dbContext.Users.FirstAsync(u => u.Email == username);

            var allUserContests = await this.dbContext.UserContests
                                                .Include(uc => uc.User)
                                                .Include(uc => uc.Contest)
                                                .Where(uc => uc.UserId == user.Id).ToListAsync();
            var allUserContestsDTO = new List<ContestDTO>();
            foreach (var userContest in allUserContests)
            {
                var contest = await this.dbContext.Contests
                                  .Include(c => c.Category)
                                  .Include(c => c.Status)
                                  .FirstAsync(c => c.Id == userContest.ContestId);
                allUserContestsDTO.Add(new ContestDTO(contest));
            }

            if (filter == null)
            {
                return allUserContestsDTO;
            }
            else if (filter.Equals("open", StringComparison.OrdinalIgnoreCase))
            {
                return allUserContestsDTO.Where(c => c.Status != "Finished");
            }
            else if (filter.Equals("closed", StringComparison.OrdinalIgnoreCase))
            {
                return allUserContestsDTO.Where(c => c.Status == "Finished");
            }
            else
            {
                throw new ArgumentException(Exceptions.InvalidFilter);
            }
        }

        /// <summary>
        /// Filter and/or sort contests by phase.
        /// </summary>
        /// <param name="phaseName">Name of the phase for which we are filtering the contests.</param>
        /// <param name="sortBy">Value to sort by.</param>
        /// <param name="order">Value to order by.</param>
        /// <returns>Returns the contests that correspond to the filters.</returns>
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
            else if (phaseName.Equals("phase2", StringComparison.OrdinalIgnoreCase))
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
            else if (phaseName.Equals("finished", StringComparison.OrdinalIgnoreCase))
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
            else
            {
                throw new ArgumentException(Exceptions.InvalidPhase);
            }
            if (filteredContests.Count != 0 && sortBy != null)
            {
                Sorting(filteredContests, sortBy, order);
            }
            return filteredContests;
        }

        /// <summary>
        /// Sorting the filtered contests.
        /// </summary>
        /// <param name="filteredContests">Contests that that are already filtered.</param>
        /// <param name="sortBy">Value to sort by.</param>
        /// <param name="order">Value to order by.</param>
        private void Sorting(List<ContestDTO> filteredContests, string sortBy, string order)
        {
            if (sortBy == "name")
            {
                switch (order)
                {
                    case null:
                    case "asc":
                        filteredContests = filteredContests.OrderBy(c => c.Name).ToList();
                        break;
                    case "desc":
                        filteredContests = filteredContests.OrderByDescending(c => c.Name).ToList();
                        break;
                }
            }
            else if (sortBy == "category")
            {
                switch (order)
                {
                    case null:
                    case "asc":
                        filteredContests = filteredContests.OrderBy(c => c.Category).ToList();
                        break;
                    case "desc":
                        filteredContests = filteredContests.OrderByDescending(c => c.Category).ToList();
                        break;
                }
            }
            else if (sortBy == "newest")
            {
                filteredContests = filteredContests.OrderBy(c => c.Phase1).ToList();
            }
            else if (sortBy == "oldest")
            {
                filteredContests = filteredContests.OrderByDescending(c => c.Finished).ToList();
            }
            else
            {
                throw new ArgumentException(Exceptions.InvalidSorting);
            }
        }

        /// <summary>
        /// Validating the DateTime of Phase1
        /// </summary>
        /// <param name="date">Starting date of Phase1</param>
        private void ValidatePhase1(DateTime date)
        {
            if (date == DateTime.MinValue || date < DateTime.UtcNow)
                throw new ArgumentException(Exceptions.InvalidDateTimePhase1);
        }

        /// <summary>
        /// Validating DateTime of Phase2
        /// </summary>
        /// <param name="date1">Starting date of Phase2</param>
        /// <param name="date2">DateTime value of Phase1</param>
        private void ValidatePhase2(DateTime date1, DateTime date2)
        {
            if (date1 == DateTime.MinValue || date1 <= date2 || date1 > date2.AddDays(31))
                throw new ArgumentException(Exceptions.InvalidDateTimePhase2);
        }

        /// <summary>
        /// Validating DateTime for Finished
        /// </summary>
        /// <param name="date1">Starting DateTime of Finished</param>
        /// <param name="date2">DateTime value of Phase2</param>
        private void ValidateFinished(DateTime date1, DateTime date2)
        {
            if (date1 == DateTime.MinValue || date1 <= date2.AddHours(1) || date1 > date2.AddHours(24))
                throw new ArgumentException(Exceptions.InvalidDateTimeFinished);
        }


        /// <summary>
        /// Find a contest with certain ID.
        /// </summary>
        /// <param name="id">ID of the contest to get.</param>
        /// <returns>Returns a contest with certain ID or an appropriate error message.</returns>
        private async Task<Contest> FindContestAsync(Guid id)
        {
            return await this.dbContext
                             .Contests
                             .Where(c => c.IsDeleted == false)
                             .FirstOrDefaultAsync(c => c.Id == id)
                             ?? throw new ArgumentException(Exceptions.InvalidContestID);
        }
    }
}
