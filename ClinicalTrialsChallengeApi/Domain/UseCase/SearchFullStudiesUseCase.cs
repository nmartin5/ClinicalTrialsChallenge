using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Response;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class SearchFullStudiesUseCase : ISearchFullStudiesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchFullStudiesUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PaginatedFullStudies> GetPaginatedFullStudiesAsync(SearchRequestDto searchRequest)
        {
            var results = await _unitOfWork.FullStudyRepository.GetPaginatedFullStudiesAsync(searchRequest.PaginationRequest.Skip, searchRequest.PaginationRequest.Take,
                    searchRequest.Keywords, searchRequest.Location, searchRequest.Statuses, searchRequest.Gender, searchRequest.CentralContactRequired);

            if (results is null)
                return new PaginatedFullStudies(new List<FullStudyViewDto>(), new Pagination(searchRequest.PaginationRequest.Skip, searchRequest.PaginationRequest.Take, 0));

            var studies = results.FullStudies.Select(r => new FullStudyViewDto(r.ProtocolSection.IdentificationModule.NCTId,
                r.ProtocolSection.IdentificationModule.BriefTitle, r.ProtocolSection.IdentificationModule.Organization.OrgFullName,
                r.ProtocolSection.StatusModule.OverallStatus, r.ProtocolSection.DescriptionModule.BriefSummary,
                r.ProtocolSection.ContactsLocationsModule?.LocationList?.Location.FirstOrDefault(),
                r.ProtocolSection.ContactsLocationsModule?.CentralContactList?.CentralContact?.Select(c => new Contact(c.CentralContactName, c.CentralContactPhone, c.CentralContactEMail))));

            return new PaginatedFullStudies(studies, new Pagination(results.Pagination.Skip, results.Pagination.Take, results.Pagination.TotalItems));
        }
    }
}
