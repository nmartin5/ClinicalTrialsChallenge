using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Domain.Model.Response;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface ISearchFullStudiesUseCase
    {
        Task<PaginatedFullStudies> GetPaginatedFullStudiesAsync(SearchRequestDto searchRequest);
    }
}
