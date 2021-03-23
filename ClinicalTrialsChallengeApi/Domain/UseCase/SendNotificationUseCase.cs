using ClinicalTrialsChallengeApi.Domain.Factory;
using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Exceptions;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class SendNotificationUseCase : ISendNotificationUseCase
    {
        private readonly IEmailFactory _emailFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public SendNotificationUseCase(IEmailFactory emailFactory, IUnitOfWork unitOfWork, ILogger<SendNotificationUseCase> logger)
        {
            _emailFactory = emailFactory;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<OneOf<Success, NotFound>> SendNotificationAsync(NotificationRequest request)
        {
            Email email;
            try
            {
                email = await _emailFactory.BuildAsync(request);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return new NotFound();
            }
            try
            {
                await email.SendAsync();

                _unitOfWork.EmailRepository.Add(email);
                _unitOfWork.Save();
                return new Success();
            }
            catch (NotificationFailedException ex)
            {
                _logger.LogError(ex, $"{nameof(SendNotificationAsync)} failed to send notification.");
                throw;
            }
        }
    }
}
