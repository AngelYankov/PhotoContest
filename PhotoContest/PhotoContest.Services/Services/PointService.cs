using PhotoContest.Data;
using PhotoContest.Data.Models;
using PhotoContest.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Services
{
    public class PointService : IPointService
    {
        private readonly PhotoContestContext dbContext;

        public PointService(PhotoContestContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAsync(int value)
        {
            var point = new Point();
            point.Value = value;
            point.CreatedOn = DateTime.UtcNow;
            await this.dbContext.AddAsync(point);
            await this.dbContext.SaveChangesAsync();
            return point.Value;
        }
    }
}
