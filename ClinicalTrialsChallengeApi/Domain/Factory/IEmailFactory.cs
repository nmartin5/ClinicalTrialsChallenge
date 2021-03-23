using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Domain.Model.Request;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Factory
{
    public interface IEmailFactory
    {
        Task<Email> BuildAsync(NotificationRequest notificationRequest);
    }
}
