using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class Attachment
    {
        public string Name { get; private set; }
        public string Base64EncodedContent { get; private set; }

        public Attachment(string name, string base64EncodedContents)
        {
            // TODO file name regex
            Name = name;
            Base64EncodedContent = base64EncodedContents;
        }
    }
}
