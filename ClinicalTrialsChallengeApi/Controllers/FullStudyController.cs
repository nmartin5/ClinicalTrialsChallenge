using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Domain.Model.Response;
using ClinicalTrialsChallengeApi.Domain.UseCase;
using ClinicalTrialsChallengeApi.Infrastructure.Client.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FullStudyController : ControllerBase
    {
        private readonly IGetFullStudyUseCase _getFullStudyUseCase;
        private readonly ISearchFullStudiesUseCase _searchFullStudiesUseCase;
        public FullStudyController(IGetFullStudyUseCase getFullStudyUseCase, ISearchFullStudiesUseCase searchFullStudiesUseCase)
        {
            _getFullStudyUseCase = getFullStudyUseCase;
            _searchFullStudiesUseCase = searchFullStudiesUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<FullStudyDto>> GetAsync([RegularExpression("(NCT)\\d{8}")][Required] string nctIdentifier)
        {
            var study = await _getFullStudyUseCase.GetFullStudyAsync(nctIdentifier);

            return study.Match<ActionResult<FullStudyDto>>(
                study => study,
                notFound => NotFound($"Study not found for NCT Identifier: {nctIdentifier}"));
        }

        [HttpGet]
        [Route("search")]
        public Task<PaginatedFullStudies> SearchAsync([FromQuery] SearchRequestDto searchRequest)
        {
            return _searchFullStudiesUseCase.GetPaginatedFullStudiesAsync(searchRequest);
        }
    }
}
