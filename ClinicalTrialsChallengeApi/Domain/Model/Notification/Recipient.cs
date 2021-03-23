using System;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class Recipient
    {
        public string Name { get; set; }
        public string Address { get; private set; }

        public Recipient(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException($"{nameof(address)} is required!");

            Name = name;
            Address = address;
        }
    }
}
