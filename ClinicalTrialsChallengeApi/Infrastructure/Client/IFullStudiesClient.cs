using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Client
{
    public interface IFullStudiesClient
    {
        Task<IEnumerable<FullStudyDto>> GetFullStudiesAsync(string expression, int minRank, int maxRank);
    }
}
