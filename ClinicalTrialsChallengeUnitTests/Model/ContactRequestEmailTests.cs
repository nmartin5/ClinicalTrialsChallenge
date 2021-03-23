using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicalTrialsChallengeUnitTests
{
    [TestFixture]
    public class ContactRequestEmailTests
    {
        private IVCardSerializer serializer;
        private ISendEmailService sendEmailService;
        private readonly Recipient validRecipient = new("John Doe", "johndoe@buffalo.edu");
        private const string alpha = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
        private const string testBase64 = "aGVsbG8gZGFya25lc3MgbXkgb2xkIGZyaWVuZA==";

        [SetUp]
        public void SetUp()
        {
            serializer = Substitute.For<IVCardSerializer>();
            sendEmailService = Substitute.For<ISendEmailService>();
        }

        [Test]
        public void NullContacts_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new ContactRequestEmail(null, serializer, "mock content", validRecipient, sendEmailService));
        }

        [Test]
        public void EmptyContacts_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new ContactRequestEmail(new List<Contact>(), serializer, "mock content", validRecipient, sendEmailService));
        }

        [Test]
        public void ValidCreate_AddsAllAttachments([Range(1, 10, 1)] int attachmentsToAdd)
        {
            serializer.SerializeVCard(BuildContact()).ReturnsForAnyArgs(Convert.FromBase64String(testBase64));
            var contacts = new List<Contact>();

            for (int i = 0; i < attachmentsToAdd; i++)
                contacts.Add(BuildContact());

            var email = new ContactRequestEmail(contacts, serializer, "test content", validRecipient, sendEmailService);

            Assert.AreEqual(attachmentsToAdd, email.Attachments.Count());
        }

        private Contact BuildContact()
        {
            var name = TestContext.CurrentContext.Random.GetString(15, alpha);
            return new Contact(name, "123-456-7890", validRecipient.Address);
        }
    }
}