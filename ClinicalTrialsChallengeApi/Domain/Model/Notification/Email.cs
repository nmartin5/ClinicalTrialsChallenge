using ClinicalTrialsChallengeApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public abstract class Email
    {
        public Recipient Recipient { get; set; }        
        public IEnumerable<Attachment> Attachments => attachments;
        private readonly List<Attachment> attachments = new List<Attachment>();

        private readonly ISendEmailService _sendEmailService;
        public Guid Id { get; private set; }
        public string Subject { get; private set; }
        public string Content { get; private set; }

        public DateTime Sent { get; private set; }

        public Email(string subject, string content, Recipient recipient, ISendEmailService sendEmailService)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException($"{nameof(subject)} is required!");

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException($"{nameof(content)} is required!");

            if (recipient is null)
                throw new ArgumentException($"{nameof(recipient)} is required!");

            if (sendEmailService is null)
                throw new ArgumentException($"{nameof(sendEmailService)} is required!");

            Id = Guid.NewGuid();
            Subject = subject;
            Content = content;
            Recipient = recipient;

            _sendEmailService = sendEmailService;
        }

        protected Email(string subject, string content)
        {
            Id = Guid.NewGuid();
            Subject = subject;
            Content = content;
        }

        public void AddAttachment(Attachment attachment)
        {
            attachments.Add(attachment);
        }

        public virtual Task SendAsync()
        {
            Sent = DateTime.UtcNow;
            return _sendEmailService.SendNotificationAsync(this);
        }
    }
}
