using ClinicalTrialsChallengeApi.Configuration;
using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Exceptions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure
{
    public class EmailService : ISendEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptions<EmailOptions> sendGridOptions)
        {
            _emailOptions = sendGridOptions.Value;
        }
        public async Task SendNotificationAsync(Email notification)
        {
            var client = new SendGridClient(_emailOptions.ApiKey);
            var msg = BuildMessage(notification);

            foreach (var attachment in notification.Attachments)
            {
                msg.AddAttachment(attachment.Name.Replace(" ", ""), attachment.Base64EncodedContent);
            }

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
                throw new NotificationFailedException($"Notification was not sent successfully. SendGrid api response: {response.StatusCode}.");
        }

        private SendGridMessage BuildMessage(Email notification)
        {
            var from = new EmailAddress(_emailOptions.AuthorEmail, _emailOptions.AuthorName);

            if (notification.Recipient is null)
                throw new ArgumentException($"{nameof(notification.Recipient)} cannot be null!");

            var to = new EmailAddress(notification.Recipient.Address, notification.Recipient.Name);
            return MailHelper.CreateSingleEmail(from, to, notification.Subject, notification.Content, "");
        }
    }
}
