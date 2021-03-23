using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Factory
{
    public interface IEmailFactory
    {
        Task<Email> BuildAsync(NotificationRequest notificationRequest);
    }
}
