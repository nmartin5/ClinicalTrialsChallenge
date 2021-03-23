using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using System.Text;

namespace ClinicalTrialsChallengeApi.Infrastructure
{
    public class VCard4Serializer : IVCardSerializer
    {
        public byte[] SerializeVCard(Contact contact)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCARD");
            sb.AppendLine("VERSION:4.0");

            sb.AppendLine($"FN:{contact.Name}");
            sb.AppendLine($"TEL;TYPE=work,voice;VALUE=uri:tel:{contact.Phone}");
            sb.AppendLine($"EMAIL:{contact.Email}");

            sb.AppendLine("END:VCARD");

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
