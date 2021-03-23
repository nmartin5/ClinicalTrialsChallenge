using System.ComponentModel.DataAnnotations;

namespace ClinicalTrialsChallengeApi.Domain.Model.Request
{
    public record NotificationRequest([RegularExpression("(NCT)\\d{8}")][Required] string NctId, [Required]RecipientRequest RecipientRequest, [Required]NotificationType NotificationType);
}
