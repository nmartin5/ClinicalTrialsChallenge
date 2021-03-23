using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using OneOf;
using OneOf.Types;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface ISendNotificationUseCase
    {
        Task<OneOf<Success, NotFound>> SendNotification(NotificationRequest request);
    }
}
