using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Response
{
    public record FullStudyViewDto(string NCTId, string Title, string OrganizationName, string Status, string BriefSummary, LocationDto Location, IEnumerable<Contact> CentralContacts);
}
