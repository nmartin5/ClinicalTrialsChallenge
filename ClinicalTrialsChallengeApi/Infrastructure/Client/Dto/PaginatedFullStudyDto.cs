using ClinicalTrialsChallengeApi.Domain.Model.Response;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Infrastructure.Client.Dto
{
    public record PaginatedFullStudyDto(IEnumerable<FullStudyDto> FullStudies, Pagination Pagination);
}
