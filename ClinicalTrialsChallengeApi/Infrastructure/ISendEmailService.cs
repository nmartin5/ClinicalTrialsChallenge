using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure
{
    public interface ISendEmailService
    {
        Task SendNotificationAsync(Email notification);
    }
}
