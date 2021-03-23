using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using OneOf;
using OneOf.Types;
using System;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public interface IGetEmailUseCase
    {
        Task<OneOf<Email, NotFound>> GetEmailAsync(Guid emailId);
    }
}
