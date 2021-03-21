using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Client
{
    public interface IFullStudiesClient
    {
        Task<PaginatedFullStudyDto> GetFullStudiesAsync(string expression, int skip, int take);
    }
}
