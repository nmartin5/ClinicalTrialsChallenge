using ClinicalTrialsChallengeApi.Infrastructure.Dto.Response;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto
{
    public record PaginatedFullStudyDto(IEnumerable<FullStudyDto> FullStudies, Pagination Pagination);
}
