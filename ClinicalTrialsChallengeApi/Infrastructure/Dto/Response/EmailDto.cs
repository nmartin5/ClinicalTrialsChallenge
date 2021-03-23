using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using System;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Response
{
    public record EmailDto(Guid Id, DateTime Sent, string  Subject, NotificationType NotificationType, string RecipientAddress, int AttachmentCount);
}
