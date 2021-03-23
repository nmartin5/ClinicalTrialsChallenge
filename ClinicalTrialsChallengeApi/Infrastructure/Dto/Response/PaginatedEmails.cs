using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Response
{
    public record PaginatedEmails(IEnumerable<EmailDto> Emails, Pagination Pagination);
}
