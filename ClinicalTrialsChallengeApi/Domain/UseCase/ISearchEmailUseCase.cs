using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Response;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface ISearchEmailUseCase
    {
        Task<PaginatedEmails> SearchEmailsAsync(PaginationRequestDto paginationRequest);
    }
}
