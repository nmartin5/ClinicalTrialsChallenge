using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Response;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface ISearchFullStudiesUseCase
    {
        Task<PaginatedFullStudies> GetPaginatedFullStudiesAsync(SearchRequestDto searchRequest);
    }
}
