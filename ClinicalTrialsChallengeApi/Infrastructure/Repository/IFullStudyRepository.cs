﻿using ClinicalTrialsChallengeApi.Infrastructure.Client.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IFullStudyRepository
    {
        Task<FullStudyDto> GetFullStudyAsync(string nctIdentifier);
        Task<PaginatedFullStudyDto> GetPaginatedFullStudiesAsync(int skip, int take, IEnumerable<string> keywords,
            string location = null, IEnumerable<string> filterStatuses = null, string gender = null, bool centralContactRequired = false);
    }
}
