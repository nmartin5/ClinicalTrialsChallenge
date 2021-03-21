using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IFullStudyRepository
    {
        Task<FullStudyDto> GetFullStudy(string nctIdentifier);
        Task<IEnumerable<FullStudyDto>> GetPaginatedFullStudies(int Skip, int Take, IEnumerable<string> keywords,
            string location = null, string status = null, string gender = null, DateTime? dateOfBirth = null);
    }
}
