using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure
{
    public interface ISendEmailService
    {
        Task SendNotificationAsync(Email notification);
    }
}
