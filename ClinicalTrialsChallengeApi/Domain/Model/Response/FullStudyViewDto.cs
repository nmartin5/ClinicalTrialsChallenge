using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure.Client.Dto;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Domain.Model.Response
{
    public record FullStudyViewDto(string NCTId, string Title, string OrganizationName, string Status, string BriefSummary, LocationDto Location, IEnumerable<Contact> CentralContacts);
}
