using ClinicalTrialsChallengeApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class ContactRequestEmail : Email
    {
        public ContactRequestEmail(IEnumerable<Contact> contacts, IVCardSerializer vCardSerializer,
            string content, Recipient recipient, ISendEmailService sendEmailService)
            : base("Clinical Study Contact", content, recipient, sendEmailService)
        {
            if (contacts is null || !contacts.Any())
                throw new ArgumentException($"{nameof(contacts)} is required!");

            foreach (var contact in contacts)
            {
                var vCard = vCardSerializer.SerializeVCard(contact);
                var base64 = Convert.ToBase64String(vCard);
                var attachmentName = contact.Name.Replace(" ", "");
                AddAttachment(new Attachment(attachmentName, base64));
            }
        }

        private ContactRequestEmail(string subject, string content) : base(subject, content) { }
    }
}
