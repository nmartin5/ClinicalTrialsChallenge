using ClinicalTrialsChallengeApi.Infrastructure;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class StudyRequestEmail : Email
    {
        public StudyRequestEmail(Attachment studyRequestAttachment, string content, Recipient recipient, ISendEmailService sendEmailService)
            : base("Clinical Study Data Request", content, recipient, sendEmailService)
        {
            AddAttachment(studyRequestAttachment);
        }

        private StudyRequestEmail(string subject, string content) : base(subject, content) { }
    }
}
