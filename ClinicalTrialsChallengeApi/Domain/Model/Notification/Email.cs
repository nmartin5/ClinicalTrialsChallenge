using System;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class Email
    {
        public IEnumerable<Recipient> Recipients => recipients;
        private readonly List<Recipient> recipients = new List<Recipient>();
        public IEnumerable<Attachment> Attachments => attachments;
        private readonly List<Attachment> attachments = new List<Attachment>();

        public string Subject { get; private set; }
        public string Content { get; private set; }

        public Email(string subject, string content)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException($"{nameof(subject)} is required!");

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException($"{nameof(content)} is required!");

            Subject = subject;
            Content = content;
        }

        public virtual void AddRecipient(Recipient recipient)
        {
            recipients.Add(recipient);
        }

        public virtual void AddAttachment(Attachment attachment)
        {
            attachments.Add(attachment);
        }
    }
}
