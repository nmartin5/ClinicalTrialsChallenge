using ClinicalTrialsChallengeApi.Domain.Model.Notification;

namespace ClinicalTrialsChallengeApi.Infrastructure
{
    public interface IVCardSerializer
    {
        byte[] SerializeVCard(Contact contact); 
    }
}
