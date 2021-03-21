using System.ComponentModel.DataAnnotations;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Request
{
    public record PaginationRequestDto([Range(0, int.MaxValue)] int Skip, [Range(1, 100)] int Take);
}
