using ClinicalTrialsChallengeApi.Model.Core;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Response
{
    public record FullStudyViewDto(string NCTId, string Title, string OrganizationName, Status Status, string BriefSummary, LocationDto location);
}
