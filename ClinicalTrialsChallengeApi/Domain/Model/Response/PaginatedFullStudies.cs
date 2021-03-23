using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Domain.Model.Response
{
    public record PaginatedFullStudies(IEnumerable<FullStudyViewDto> FullStudies, Pagination Pagination);
}
