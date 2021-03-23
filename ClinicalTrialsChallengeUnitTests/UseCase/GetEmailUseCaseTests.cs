using ClinicalTrialsChallengeApi.Domain.Factory;
using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Domain.UseCase;
using ClinicalTrialsChallengeApi.Exceptions;
using ClinicalTrialsChallengeApi.Infrastructure;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using OneOf.Types;
using System;

namespace ClinicalTrialsChallengeUnitTests.UseCase
{
    [TestFixture]
    public class GetEmailUseCaseTests
    {
        private IUnitOfWork unitOfWork;
        private IEmailRepository emailRepository;
        private GetEmailUseCase getEmailUseCase;
        private const string testBase64 = "aGVsbG8gZGFya25lc3MgbXkgb2xkIGZyaWVuZA==";

        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            emailRepository = Substitute.For<IEmailRepository>();
            unitOfWork.EmailRepository.ReturnsForAnyArgs(emailRepository);
            getEmailUseCase = new GetEmailUseCase(unitOfWork);
        }

        [Test]
        public void NullEmail_ReturnsNotFound()
        {
            emailRepository.GetAsync(Guid.NewGuid()).ReturnsForAnyArgs((Email)null);

            var email = getEmailUseCase.GetEmailAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            Assert.IsInstanceOf<NotFound>(email.Value);
        }

        [Test]
        public void FoundEmail_ReturnsTheEmail()
        {
            var expectedEmail = new StudyRequestEmail(new Attachment("name", testBase64), "test content", new Recipient("name", "address@email.com"), Substitute.For<ISendEmailService>());
            emailRepository.GetAsync(Guid.NewGuid()).ReturnsForAnyArgs(expectedEmail);

            var email = getEmailUseCase.GetEmailAsync(Guid.NewGuid()).GetAwaiter().GetResult();
            Assert.IsInstanceOf<Email>(email.Value);
        }
    }

    [TestFixture]
    public class SendNotificationUseCaseTests
    {
        private IUnitOfWork unitOfWork;
        private IEmailRepository emailRepository;
        private IEmailFactory emailFactory;        
        private SendNotificationUseCase sendNotificationUseCase;
        private readonly NotificationRequest notificationRequest = new("NCT12345678", new RecipientRequest("johndoe@example.com", "john doe"), NotificationType.StudyRequestEmail);
        private const string testBase64 = "aGVsbG8gZGFya25lc3MgbXkgb2xkIGZyaWVuZA==";

        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            emailRepository = Substitute.For<IEmailRepository>();
            unitOfWork.EmailRepository.ReturnsForAnyArgs(emailRepository);
            emailFactory = Substitute.For<IEmailFactory>();
            sendNotificationUseCase = new SendNotificationUseCase(emailFactory, unitOfWork, Substitute.For<ILogger<SendNotificationUseCase>>());
        }

        [Test]
        public void NotFoundEmail_ReturnsNotFound()
        {
            emailFactory.BuildAsync(null).ReturnsForAnyArgs<Email>(x => { throw new ArgumentException(); });

            var result = sendNotificationUseCase.SendNotificationAsync(notificationRequest).GetAwaiter().GetResult();
            Assert.IsInstanceOf<NotFound>(result.Value);
        }

        [Test]
        public void FoundEmail_NotificationFailed_ThrowsNotificationFailedException()
        {
            var emailSenderSubstitute = Substitute.For<ISendEmailService>();
            var expectedEmail = new StudyRequestEmail(new Attachment("attachment.txt", testBase64), "test content", new Recipient("john doe", "johndoe@example.com"), emailSenderSubstitute);
            emailFactory.BuildAsync(null).ReturnsForAnyArgs(expectedEmail);

            emailSenderSubstitute.SendNotificationAsync(expectedEmail).ReturnsForAnyArgs(x => throw new NotificationFailedException());

            Assert.ThrowsAsync<NotificationFailedException>(() => sendNotificationUseCase.SendNotificationAsync(notificationRequest));
        }

        [Test]
        public void FoundEmail_SentSuccessfully_SavesWithoutError()
        {
            var emailSenderSubstitute = Substitute.For<ISendEmailService>();
            var expectedEmail = new StudyRequestEmail(new Attachment("attachment.txt", testBase64), "test content", new Recipient("john doe", "johndoe@example.com"), emailSenderSubstitute);
            emailFactory.BuildAsync(null).ReturnsForAnyArgs(expectedEmail);

            var result = sendNotificationUseCase.SendNotificationAsync(notificationRequest).GetAwaiter().GetResult();
            Assert.IsInstanceOf<Success>(result.Value);
        }

    }
}