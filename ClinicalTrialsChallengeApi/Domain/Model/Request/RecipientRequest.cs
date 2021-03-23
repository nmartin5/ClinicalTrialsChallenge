using System.ComponentModel.DataAnnotations;

namespace ClinicalTrialsChallengeApi.Domain.Model.Request
{
    public record RecipientRequest([EmailAddress]string RecipientAddress, string RecipientName);
}
