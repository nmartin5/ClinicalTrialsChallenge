namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class Recipient
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Recipient(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
