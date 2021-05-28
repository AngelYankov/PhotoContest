using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
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
using System.Threading;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class ContestService : IContestService
    {
        private readonly PhotoContestContext dbContext;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;

        public ContestService(PhotoContestContext dbContext, IHttpContextAccessor contextAccessor, IUserService userService, ICategoryService categoryService)
        {
            this.dbContext = dbContext;
            this.contextAccessor = contextAccessor;
            this.userService = userService;
            this.categoryService = categoryService;
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

            var category = await this.categoryService.FindCategoryByNameAsync(dto.CategoryName);
            newContest.CategoryId = category.Id;

            var status = await this.dbContext.Statuses.FirstOrDefaultAsync(s => s.Name == "Phase 1");

            newContest.StatusId = status.Id;

            newContest.IsOpen = dto.IsOpen;

            ValidatePhase1(DateTime.ParseExact(dto.Phase1, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                           DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture));
            newContest.Phase1 = DateTime.ParseExact(dto.Phase1, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture);

            ValidatePhase2(DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                           DateTime.ParseExact(dto.Phase1, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                           DateTime.ParseExact(dto.Finished, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture));
            newContest.Phase2 = DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture);

            ValidateFinished(DateTime.ParseExact(dto.Finished, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                             DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture));
            newContest.Finished = DateTime.ParseExact(dto.Finished, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture);

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
        public async Task<bool> DeleteAsync(string contestName)
        {
            var contest = await FindContestByNameAsync(contestName);
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
        public async Task<IEnumerable<ContestDTO>> GetAllOpenAsync(string phase)
        {
            var allOpenContests = await this.dbContext
                                            .Contests
                                            .Include(c => c.Category)
                                            .Include(a => a.Status)
                                            .Where(c => c.IsDeleted == false && c.IsOpen == true)
                                            .Select(c => new ContestDTO(c))
                                            .ToListAsync();
            if (phase.Equals("Phase 1", StringComparison.OrdinalIgnoreCase))
            {
                return allOpenContests.Where(c => c.Status == "Phase 1");
            }
            else if (phase.Equals("Phase 2", StringComparison.OrdinalIgnoreCase))
            {
                return allOpenContests.Where(c => c.Status == "Phase 2");
            }
            else if (phase.Equals("Finished", StringComparison.OrdinalIgnoreCase))
            {
                return allOpenContests.Where(c => c.Status == "Finished");
            }
            else if (phase.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                return allOpenContests;
            }
            else
            {
                throw new ArgumentException(Exceptions.InvalidPhase);
            }
        }

        /// <summary>
        /// Enroll user to contest.
        /// </summary>
        /// <param name="username">Username of the user to enroll.</param>
        /// <param name="contestName">Name of the contest to enroll in.</param>
        /// <returns>Return true if successful or an appropriate error message.</returns>
        public async Task<bool> EnrollAsync(string contestName)
        {
            var contest = await FindContestByNameAsync(contestName);
            if (contest.IsOpen == false)
                throw new ArgumentException(Exceptions.NotAllowedEnrollment);

            var username = this.contextAccessor.HttpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var user = await this.userService.GetUserByUsernameAsync(username);

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
        /// Invite user in contest.
        /// </summary>
        /// <param name="contestName">Name of the contest to invite to.</param>
        /// <param name="username">Username of the user to invite.</param>
        /// <returns>Return true if successful or an appropriate error message.</returns>
        public async Task<bool> InviteAsync(string contestName, string username)
        {
            var contest = await FindContestByNameAsync(contestName);
            var user = await this.userService.GetUserByUsernameAsync(username);

            if (user.Rank.Name == "Organizer")
            {
                throw new ArgumentException(Exceptions.InvalidParticipant);
            }

            if (await this.dbContext.Juries.AnyAsync(j => j.UserId == user.Id && j.ContestId == contest.Id))
            {
                throw new ArgumentException(Exceptions.ExistingJury);
            }

            if (await this.dbContext.UserContests.AnyAsync(uc => uc.UserId == user.Id && uc.ContestId == contest.Id))
            {
                throw new ArgumentException(Exceptions.EnrolledUser);
            }

            if (contest.IsOpen)
            {
                throw new ArgumentException(Exceptions.NotAllowedInvitation);
            }

            var userContest = new UserContest()
            {
                ContestId = contest.Id,
                UserId = user.Id,
            };
            await this.dbContext.UserContests.AddAsync(userContest);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Choose jury from users.
        /// </summary>
        /// <param name="contestName">Contest to choose jury for.</param>
        /// <param name="username">Username of the user chosen.</param>
        /// <returns>Return true if successful or an appropriate error message.</returns>
        public async Task<bool> ChooseJuryAsync(string contestName, string username)
        {
            var contest = await FindContestByNameAsync(contestName);
            var user = await this.userService.GetUserByUsernameAsync(username);
            if (user.Rank.Name != "Master" && user.Rank.Name != "Wise and Benevolent Photo Dictator")
            {
                throw new ArgumentException(Exceptions.InvalidJuryUser);
            }
            if (await this.dbContext.Juries.AnyAsync(uc => uc.UserId == user.Id && uc.ContestId == contest.Id))
            {
                throw new ArgumentException(Exceptions.ExistingJury);
            }

            var juryMember = new JuryMember();
            juryMember.ContestId = contest.Id;
            juryMember.UserId = user.Id;
            await this.dbContext.Juries.AddAsync(juryMember);
            await this.dbContext.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Update a contest by a certain ID and data.
        /// </summary>
        /// <param name="id">ID of the contest to update.</param>
        /// <param name="dto">Details of the contest to be updated.</param>
        /// <returns>Returns the updated contest or an appropriate error message.</returns>
        public async Task<ContestDTO> UpdateAsync(string contestName, UpdateContestDTO dto)
        {
            var contest = await FindContestByNameAsync(contestName);

            if (dto.CategoryName != null)
            {
                var category = await this.categoryService.FindCategoryByNameAsync(dto.CategoryName);
                contest.CategoryId = category.Id;
            }
            if (dto.StatusName != null)
            {
                var status = await this.dbContext.Statuses.FirstOrDefaultAsync(s => s.Name == dto.StatusName)
                                ?? throw new ArgumentException(Exceptions.InvalidStatus);
                contest.StatusId = status.Id;
            }
            if (dto.Phase1 != null)
            {
                ValidatePhase1(DateTime.ParseExact(dto.Phase1, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                               DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture));
                contest.Phase1 = DateTime.ParseExact(dto.Phase1, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture);
            }
            if (dto.Phase2 != null)
            {
                ValidatePhase2(DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                               DateTime.ParseExact(dto.Phase1, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                               DateTime.ParseExact(dto.Finished, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture));
                contest.Phase2 = DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture);
            }
            if (dto.Finished != null)
            {
                ValidateFinished(DateTime.ParseExact(dto.Finished, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture),
                                 DateTime.ParseExact(dto.Phase2, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture));
                contest.Finished = DateTime.ParseExact(dto.Finished, "dd.MM.yy HH:mm", CultureInfo.InvariantCulture);
            }
            contest.Name = dto.Name ?? contest.Name;
            if (dto.IsOpen != false)
            {
                contest.IsOpen = true;
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
            var user = await this.userService.GetUserByUsernameAsync(username);

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
                                  .FirstAsync(c => c.Id == userContest.ContestId && c.IsDeleted == false);
                allUserContestsDTO.Add(new ContestDTO(contest));
            }

            if (filter == null)
            {
                return allUserContestsDTO;
            }
            else if (filter.Equals("current", StringComparison.OrdinalIgnoreCase))
            {
                return allUserContestsDTO.Where(c => c.Status != "Finished");
            }
            else if (filter.Equals("past", StringComparison.OrdinalIgnoreCase))
            {
                return allUserContestsDTO.Where(c => c.Status == "Finished");
            }
            else
            {
                throw new ArgumentException(Exceptions.InvalidFilter);
            }
        }
        public async Task<List<Contest>> GetAllFinishedContestsAsync()
        {
            return await this.dbContext.Contests
                                       .Include(c => c.Status)
                                       .Where(c => c.Status.Name == "Finished" && c.IsDeleted == false)
                                       .ToListAsync();
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

            if (phaseName.Equals("phase 1", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var contest in allContests)
                {
                    if (contest.Status.Name == "Phase 1")
                    {
                        var contestDTO = new ContestDTO(contest);
                        filteredContests.Add(contestDTO);
                    }
                }
            }
            else if (phaseName.Equals("phase 2", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var contest in allContests)
                {
                    if (contest.Status.Name == "Phase 2")
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
                filteredContests = Sorting(filteredContests, sortBy, order);
            }
            return filteredContests;
        }

        /// <summary>
        /// Changing the status of a contest in the background.
        /// </summary>
        /// <returns></returns>
        public async Task ChangeStatus(CancellationToken cancellationToken)
        {
            var contests = await this.dbContext.Contests.ToListAsync();

            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var contest in contests)
                {
                    if (DateTime.UtcNow < contest.Phase2)
                    {
                        contest.StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186");
                    }
                    if (DateTime.UtcNow >= contest.Phase2 && DateTime.UtcNow < contest.Finished)
                    {
                        contest.StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc");
                    }
                    if (DateTime.UtcNow >= contest.Finished)
                    {
                        contest.StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6");
                    }
                    await this.dbContext.SaveChangesAsync();
                }
                await Task.Delay(1000 * 5);
            }
        }

        /// <summary>
        /// Sorting the filtered contests.
        /// </summary>
        /// <param name="filteredContests">Contests that that are already filtered.</param>
        /// <param name="sortBy">Value to sort by.</param>
        /// <param name="order">Value to order by.</param>
        private List<ContestDTO> Sorting(List<ContestDTO> filteredContests, string sortBy, string order)
        {
            if (sortBy == "name")
            {
                switch (order)
                {
                    case null:
                    case "asc":
                        return filteredContests = filteredContests.OrderBy(c => c.Name).ToList();
                    case "desc":
                        return filteredContests = filteredContests.OrderByDescending(c => c.Name).ToList();
                    default: throw new ArgumentException("Invalid order type.");
                }
            }
            else if (sortBy == "category")
            {
                switch (order)
                {
                    case null:
                    case "asc":
                        return filteredContests = filteredContests.OrderBy(c => c.Category).ToList();
                    case "desc":
                        return filteredContests = filteredContests.OrderByDescending(c => c.Category).ToList();
                    default: throw new ArgumentException("Invalid order type.");

                }
            }
            else if (sortBy == "newest")
            {
                return filteredContests = filteredContests.OrderBy(c => c.Phase1).ToList();
            }
            else if (sortBy == "oldest")
            {
                return filteredContests = filteredContests.OrderByDescending(c => c.Finished).ToList();
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
        private void ValidatePhase1(DateTime date1, DateTime date2)
        {
            if (date1 == DateTime.MinValue || date1 < DateTime.UtcNow || date1 > date2)
                throw new ArgumentException(Exceptions.InvalidDateTimePhase1);
        }

        /// <summary>
        /// Validating DateTime of Phase2
        /// </summary>
        /// <param name="date1">Starting date of Phase2</param>
        /// <param name="date2">DateTime value of Phase1</param>
        private void ValidatePhase2(DateTime date1, DateTime date2, DateTime date3)
        {
            if (date1 == DateTime.MinValue || date1 <= date2 || date1 > date2.AddDays(31) || date1 > date3)
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
        public async Task<Contest> FindContestAsync(Guid id)
        {
            return await this.dbContext
                             .Contests
                             .Include(c => c.Category)
                             .Include(c => c.Status)
                             .Where(c => c.IsDeleted == false)
                             .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false)
                             ?? throw new ArgumentException(Exceptions.InvalidContestID);
        }

        /// <summary>
        /// Find a contest with certain name.
        /// </summary>
        /// <param name="id">Name of the contest to get.</param>
        /// <returns>Returns a contest with certain ID or an appropriate error message.</returns>
        public async Task<Contest> FindContestByNameAsync(string contestName)
        {
            return await this.dbContext
                             .Contests
                             .Include(c => c.Category)
                             .Include(c => c.Status)
                             .Where(c => c.IsDeleted == false)
                             .FirstOrDefaultAsync(c => c.Name.ToLower() == contestName.ToLower() && c.IsDeleted == false)
                             ?? throw new ArgumentException(Exceptions.InvalidContestName);
        }

        /// <summary>
        /// Changing the status of a contest in the background.
        /// </summary>
        /// <returns></returns>
        public async Task ChangeStatus()
        {
            var contests = await this.dbContext.Contests.ToListAsync();

            foreach (var contest in contests)
            {
                if (DateTime.UtcNow < contest.Phase2)
                {
                    contest.StatusId = Guid.Parse("9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186");
                }
                if (DateTime.UtcNow >= contest.Phase2 && DateTime.UtcNow < contest.Finished)
                {
                    contest.StatusId = Guid.Parse("27c7d81e-eb1c-469b-8919-a532322273cc");
                }
                if (DateTime.UtcNow >= contest.Finished)
                {
                    contest.StatusId = Guid.Parse("cf6bf4fb-655e-47cc-8dac-4a39cbff74b6");
                }
                await this.dbContext.SaveChangesAsync();
            }
        }
    }
}
