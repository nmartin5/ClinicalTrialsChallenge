using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Exceptions;
using ClinicalTrialsChallengeApi.Infrastructure;
using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class SendContactUseCase : ISendContactUseCase
    {
        private readonly ISendEmailService _sendEmailService;
        private readonly IVCardSerializer _vCardSerializer;
        private readonly IFullStudyRepository _fullStudyRepository;
        private readonly ILogger _logger;

        public SendContactUseCase(ISendEmailService notificationService, IFullStudyRepository fullStudyRepository, IVCardSerializer vCardSerializer, ILogger<SendContactUseCase> logger)
        {
            _sendEmailService = notificationService;
            _fullStudyRepository = fullStudyRepository;
            _vCardSerializer = vCardSerializer;
            _logger = logger;
        }
        public async Task<OneOf<Success, NotFound>> SendContact(ContactRequest request)
        {
            var fullStudy = await _fullStudyRepository.GetFullStudyAsync(request.NctId);

            if (fullStudy is null || !(HasCentralContact(fullStudy)))
                return new NotFound();

            var subject = "Clinical Trial Contact";
            var content = @$"
Your contact request has arrived. See the attachment for your requested contact information.

Have an awesome rest of the day!

";
            var email = new Email(subject, content);
            
            var recipient = new Recipient(request.NotificationRequest.RecipientName, request.NotificationRequest.RecipientAddress);
            email.AddRecipient(recipient);

            foreach (var centralContact in fullStudy.ProtocolSection.ContactsLocationsModule.CentralContactList.CentralContact)
            {
                var contact = new Contact(centralContact.CentralContactName, centralContact.CentralContactPhone, centralContact.CentralContactEMail);
                var serializedContact = _vCardSerializer.SerializeVCard(contact);
                var base64VCard = Convert.ToBase64String(serializedContact);
                var attachment = new Attachment($"{contact.Name}.vcf", base64VCard);

                email.AddAttachment(attachment);
            }

            try
            {
                await _sendEmailService.SendNotificationAsync(email);
                return new Success();
            }
            catch (NotificationFailedException ex)
            {
                _logger.LogError(ex, $"{nameof(SendContact)} failed to send notification.");
                throw;
            }
        }

        private bool HasCentralContact(FullStudyDto fullStudy)
        {
            return fullStudy.ProtocolSection?.ContactsLocationsModule?.CentralContactList?.CentralContact.Any() ?? false;
        }
    }
}
