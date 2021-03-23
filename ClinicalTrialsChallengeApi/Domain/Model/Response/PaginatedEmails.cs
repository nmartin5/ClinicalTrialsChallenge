using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Domain.Model.Response
{
    public record PaginatedEmails(IEnumerable<EmailDto> Emails, Pagination Pagination);
}
