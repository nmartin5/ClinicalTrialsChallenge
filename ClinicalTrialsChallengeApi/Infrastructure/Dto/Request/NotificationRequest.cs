using System.ComponentModel.DataAnnotations;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Request
{
    public record NotificationRequest([RegularExpression("(NCT)\\d{8}")][Required] string NctId, [Required]RecipientRequest RecipientRequest, [Required]NotificationType NotificationType);
}
