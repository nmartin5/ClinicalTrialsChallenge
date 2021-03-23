using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure;
using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Factory
{
    public class EmailFactory : IEmailFactory
    {
        private readonly IVCardSerializer _vCardSerializer;
        private readonly ISendEmailService _sendEmailService;
        private readonly IUnitOfWork _unitOfWork;

        public EmailFactory(IVCardSerializer vCardSerializer, ISendEmailService sendEmailService, IUnitOfWork unitOfWork)
        {
            _vCardSerializer = vCardSerializer;
            _sendEmailService = sendEmailService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Email> BuildAsync(NotificationRequest notificationRequest)
        {
            var study = await _unitOfWork.FullStudyRepository.GetFullStudyAsync(notificationRequest.NctId);
            if (study is null)
                throw new ArgumentException($"Study with NCT Identifier: {notificationRequest.NctId} not found!");

            var recipient =  new Recipient(notificationRequest.RecipientRequest.RecipientName, notificationRequest.RecipientRequest.RecipientAddress);
            return notificationRequest.NotificationType switch
            {
                NotificationType.ContactRequestEmail => BuildContactRequestEmail(recipient, study),
                NotificationType.StudyRequestEmail => BuildStudyRequestEmail(recipient, study),
                _ => throw new NotSupportedException($"{nameof(notificationRequest.NotificationType)} of type: {notificationRequest.NotificationType} is not supported!"),
            };
        }

        private ContactRequestEmail BuildContactRequestEmail(Recipient recipient, FullStudyDto study)
        {
            var contacts = study.ProtocolSection?.ContactsLocationsModule?.CentralContactList?.CentralContact?
                .Select(c => new Contact(c.CentralContactName, c.CentralContactPhone, c.CentralContactEMail));

            if (!contacts.Any())
                throw new ArgumentException($"No central contact found for request! NCT ID: {study.ProtocolSection.IdentificationModule.NCTId}");

            return new ContactRequestEmail(contacts, _vCardSerializer, GetContent("contact"), recipient, _sendEmailService);
        }

        private StudyRequestEmail BuildStudyRequestEmail(Recipient recipient, FullStudyDto study)
        {
            var json = JsonSerializer.Serialize(study);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            var attachment = new Attachment($"study-{study.ProtocolSection.IdentificationModule.NCTId}.json", base64);

            return new StudyRequestEmail(attachment, GetContent("clinical study data"), recipient, _sendEmailService);
        }

        private static string GetContent(string contentName)
        {
            return @$"
Your {contentName} request has arrived. See the attachment for your requested contact information.

Have an awesome rest of the day!

";
        }
    }
}
