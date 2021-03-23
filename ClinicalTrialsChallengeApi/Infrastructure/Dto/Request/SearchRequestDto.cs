using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Request
{
    public record SearchRequestDto([Required] PaginationRequestDto PaginationRequest, IEnumerable<string> Keywords,
        string Location, IEnumerable<string> Statuses, string Gender, bool CentralContactRequired);
}