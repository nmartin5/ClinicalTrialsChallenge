using ClinicalTrialsChallengeApi.Domain.Model.Request;
using System;

namespace ClinicalTrialsChallengeApi.Domain.Model.Response
{
    public record EmailDto(Guid Id, DateTime Sent, string  Subject, NotificationType NotificationType, string RecipientAddress, int AttachmentCount);
}
