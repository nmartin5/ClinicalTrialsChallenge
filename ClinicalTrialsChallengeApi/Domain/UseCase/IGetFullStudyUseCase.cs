using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using OneOf;
using OneOf.Types;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface IGetFullStudyUseCase
    {
        Task<OneOf<FullStudyDto, NotFound>> GetFullStudyAsync(string nctIdentifier);
    }
}
