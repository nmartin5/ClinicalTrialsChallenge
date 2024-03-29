﻿using System;

namespace ClinicalTrialsChallengeApi.Domain.Model.Notification
{
    public class Attachment
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Base64EncodedContent { get; private set; }

        public Attachment(string name, string base64EncodedContent)
        {
            Id = Guid.NewGuid();
            Name = name;
            Base64EncodedContent = base64EncodedContent;
        }
    }
}
