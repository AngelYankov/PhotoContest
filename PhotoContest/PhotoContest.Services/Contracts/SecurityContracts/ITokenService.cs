using PhotoContest.Services.Models.SecurityModels;
using System.Threading.Tasks;

namespace PhotoContest.Services.Contracts.SecurityContracts
{
    public interface ITokenService
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
    }
}
