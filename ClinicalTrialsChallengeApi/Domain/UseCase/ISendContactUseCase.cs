using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using OneOf;
using OneOf.Types;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    public interface ISendContactUseCase
    {
        Task<OneOf<Success, NotFound>> SendContact(ContactRequest request);
    }
}
