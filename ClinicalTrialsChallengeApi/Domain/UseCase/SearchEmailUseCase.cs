using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Domain.Model.Response;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class SearchEmailUseCase : ISearchEmailUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchEmailUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedEmails> SearchEmailsAsync(PaginationRequestDto paginationRequest)
        {   
            var totalEmails = _unitOfWork.EmailRepository.GetCountAsync();
            var emails = await _unitOfWork.EmailRepository.GetAsync(paginationRequest.Skip, paginationRequest.Take);

            var dtos = emails.Select(e => new EmailDto(e.Id, e.Sent, e.Subject, Enum.Parse<NotificationType>(e.GetType().Name), e.Recipient.Address, e.Attachments.Count()));

            return new PaginatedEmails(dtos, new Pagination(paginationRequest.Skip, paginationRequest.Take, await totalEmails));
        }
    }
}
