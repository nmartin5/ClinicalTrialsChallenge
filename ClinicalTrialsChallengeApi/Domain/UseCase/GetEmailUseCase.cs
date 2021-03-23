using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using OneOf;
using OneOf.Types;
using System;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class GetEmailUseCase : IGetEmailUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmailUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<Email, NotFound>> GetEmailAsync(Guid emailId)
        {
            var email = await _unitOfWork.EmailRepository.GetAsync(emailId);

            if (email is null)
                return new NotFound();

            return email;
        }
    }
}
