using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Response;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FullStudyController : ControllerBase
    {
        private readonly IFullStudyRepository _fullStudyRepository;
        public FullStudyController(IFullStudyRepository fullStudyRepository)
        {
            _fullStudyRepository = fullStudyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<FullStudyDto>> GetAsync(string nctIdentifier)
        {
            if (string.IsNullOrWhiteSpace(nctIdentifier))
                return BadRequest($"{nameof(nctIdentifier)} cannot be null or whitespace!");

            FullStudyDto result;
            try
            {
                result = await _fullStudyRepository.GetFullStudy(nctIdentifier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }


            if (result is null)
                return NotFound($"Study not found for NCT Identifier: {nctIdentifier}");

            return result;
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<PaginatedFullStudies>> SearchAsync([FromQuery] SearchRequestDto searchRequest)
        {
            PaginatedFullStudyDto results;
            try
            {
                results = await _fullStudyRepository.GetPaginatedFullStudies(searchRequest.PaginationRequest.Skip, searchRequest.PaginationRequest.Take,
                    searchRequest.Keywords, searchRequest.Location, searchRequest.Statuses, searchRequest.Gender);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            if (results is null)
                return new PaginatedFullStudies(new List<FullStudyViewDto>(), new Pagination(searchRequest.PaginationRequest.Skip, searchRequest.PaginationRequest.Take, 0));
            var studies = results.FullStudies.Select(r => new FullStudyViewDto(r.ProtocolSection.IdentificationModule.NCTId,
                r.ProtocolSection.IdentificationModule.BriefTitle, r.ProtocolSection.IdentificationModule.Organization.OrgFullName,
                r.ProtocolSection.StatusModule.OverallStatus, r.ProtocolSection.DescriptionModule.BriefSummary,
                r.ProtocolSection.ContactsLocationsModule?.LocationList?.Location.FirstOrDefault()));
            return new PaginatedFullStudies(studies, new Pagination(results.Pagination.Skip, results.Pagination.Take, results.Pagination.TotalItems));
        }
    }
}
