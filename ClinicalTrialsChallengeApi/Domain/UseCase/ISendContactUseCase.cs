using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using OneOf;
using OneOf.Types;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface ISendContactUseCase
    {
        Task<OneOf<Success, NotFound>> SendContact(ContactRequest request);
    }
}
