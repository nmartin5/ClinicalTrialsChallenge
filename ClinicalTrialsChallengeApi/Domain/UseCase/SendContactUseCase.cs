using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using OneOf;
using OneOf.Types;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class SendContactUseCase : ISendContactUseCase
    {
        private readonly ISendEmailService _sendEmailService;
        private readonly IFullStudyRepository _fullStudyRepository;

        public SendContactUseCase(ISendEmailService notificationService, IFullStudyRepository fullStudyRepository)
        {
            _sendEmailService = notificationService;
            _fullStudyRepository = fullStudyRepository;
        }
        public async Task<OneOf<Success, NotFound>> SendContact(ContactRequest request)
        {
            var studyField = _fullStudyRepository.GetFullStudy(request.NctId);

            if (studyField is null)
                return new NotFound();

            var email = new Email("subject", "content");
            var recipient = new Recipient(request.NotificationRequest.RecipientName, request.NotificationRequest.RecipientAddress);
            email.AddRecipient(recipient);

            await _sendEmailService.SendNotificationAsync(email);

            return new Success();
        }
    }
}
