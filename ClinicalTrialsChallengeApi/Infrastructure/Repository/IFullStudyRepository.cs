using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IFullStudyRepository
    {
        Task<FullStudyDto> GetFullStudy(string nctIdentifier);
        Task<PaginatedFullStudyDto> GetPaginatedFullStudies(int skip, int take, IEnumerable<string> keywords,
            string location = null, IEnumerable<string> filterStatuses = null, string gender = null);
    }
}
