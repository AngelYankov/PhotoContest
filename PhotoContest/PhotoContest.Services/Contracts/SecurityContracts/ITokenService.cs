using PhotoContest.Services.Models.SecurityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts.SecurityContracts
{
    public interface ITokenService
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
    }
}
