using ClinicalTrialsChallengeApi.Configuration;
using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf.Types;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
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
            var from = new EmailAddress(_emailOptions.ApiKey, "Nick Martin");
            var subject = "Sending with SendGrid is Fun";
            var tos = notification.Recipients.Select(r => new EmailAddress(r.Address, r.Name)).ToList();
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
            
            foreach (var attachment in notification.Attachments)
            {
                msg.AddAttachment(attachment.Name, attachment.Base64EncodedContent);
            }
            
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                throw new NotificationFailedException($"Notification was not sent successfully. SendGrid api response: {response.StatusCode}.");
            }
        }
    }
}
