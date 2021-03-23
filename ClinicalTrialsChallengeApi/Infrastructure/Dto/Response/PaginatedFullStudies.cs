using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Response
{
    public record PaginatedFullStudies(IEnumerable<FullStudyViewDto> FullStudies, Pagination Pagination);
}
