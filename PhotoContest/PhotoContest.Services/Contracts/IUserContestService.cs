﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts
{
    public interface IUserContestService
    {
        Task CalculatePointsAsync();
    }
}