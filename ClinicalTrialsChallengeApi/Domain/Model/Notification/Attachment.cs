using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class Attachment
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Base64EncodedContent { get; private set; }

        public Attachment(string name, string base64EncodedContent)
        {
            // TODO file name regex
            Id = Guid.NewGuid();
            Name = name;
            Base64EncodedContent = base64EncodedContent;
        }
    }
}
