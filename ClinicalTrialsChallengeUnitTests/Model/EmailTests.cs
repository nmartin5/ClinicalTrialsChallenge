using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace ClinicalTrialsChallengeUnitTests
{
    [TestFixture]
    public class EmailTests
    {
        private ISendEmailService sendEmailService;
        private readonly Recipient validRecipient = new("John Doe", "johndoe@buffalo.edu");
        private Attachment validAttachment;
        private const string testBase64 = "aGVsbG8gZGFya25lc3MgbXkgb2xkIGZyaWVuZA==";

        [SetUp]
        public void SetUp()
        {
            sendEmailService = Substitute.For<ISendEmailService>();
            validAttachment = new Attachment("attachment.txt", testBase64);
        }

        [Test]
        public void NullContent_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new StudyRequestEmail(validAttachment, null, validRecipient, sendEmailService));
        }

        [Test]
        public void EmptyContent_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new StudyRequestEmail(validAttachment, "", validRecipient, sendEmailService));
        }

        [Test]
        public void NullRecipient_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new StudyRequestEmail(validAttachment, "test content", null, sendEmailService));
        }

        [Test]
        public void NullAttachment_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new StudyRequestEmail(null, "test content", validRecipient, sendEmailService));
        }

        [Test]
        public void AddAttachment_AddsSuccessfully()
        {
            var email = new StudyRequestEmail(validAttachment, "test content", validRecipient, sendEmailService);

            email.AddAttachment(validAttachment);

            Assert.AreEqual(2, email.Attachments.Count());
        }
    }
}