using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Domain.Model.Response;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface ISearchEmailUseCase
    {
        Task<PaginatedEmails> SearchEmailsAsync(PaginationRequestDto paginationRequest);
    }
}
