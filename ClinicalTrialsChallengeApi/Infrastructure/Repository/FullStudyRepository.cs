using ClinicalTrialsChallengeApi.Infrastructure.Client;
using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public class FullStudyRepository : IFullStudyRepository
    {
        // TODO TTL?
        private readonly Dictionary<string, FullStudyDto> _fullStudyCache = new();
        private readonly IFullStudiesClient _fullStudiesClient; 
        public FullStudyRepository(IFullStudiesClient fullStudiesClient)
        {
            _fullStudiesClient = fullStudiesClient;
        }
        public async Task<FullStudyDto> GetFullStudy(string nctIdentifier)
        {
            if (string.IsNullOrWhiteSpace(nctIdentifier))
                throw new ArgumentException($"{nameof(nctIdentifier)} is required!");

            // TODO make this unit testable
            if (!Regex.IsMatch(nctIdentifier, "(NCT)\\d{8}"))
                throw new ArgumentException($"{nameof(nctIdentifier)} does not match the expected format! Expected format: NCT00000000");

            if (_fullStudyCache.TryGetValue(nctIdentifier, out var result))
                return result;

            var foundStudies = await _fullStudiesClient.GetFullStudiesAsync(nctIdentifier, 1, 1);

            if (foundStudies is null)
                return null;

            var foundStudy = foundStudies.SingleOrDefault();
            if (foundStudy != null)
                _fullStudyCache.Add(nctIdentifier, foundStudy);

            return foundStudy;
        }

        public async Task<IEnumerable<FullStudyDto>> GetPaginatedFullStudies(int skip, int take, IEnumerable<string> keywords,
            string location = null, string status = null, string gender = null, DateTime? dateOfBirth = null)
        {
            if (skip < 0)
                throw new ArgumentException($"{nameof(skip)} cannot be negative!");

            if (take < 0)
                throw new ArgumentException($"{nameof(take)} cannot be negative!");

            if (!keywords.Any())
                throw new ArgumentException($"{nameof(keywords)} cannot be empty!");

            var minRank = skip + 1;
            var maxRank = (minRank + take) - 1;

            var keywordInclusiveClauseSb = new StringBuilder(string.Join(" OR ", keywords));

            if (!string.IsNullOrWhiteSpace(location))
            {
                keywordInclusiveClauseSb.Insert(0, '(');
                keywordInclusiveClauseSb.Append(')');
                keywordInclusiveClauseSb.Append($" AND (SEARCH[Location] ({location}))");
            }

            var foundStudies = await _fullStudiesClient.GetFullStudiesAsync(keywordInclusiveClauseSb.ToString(), minRank, maxRank);

            return foundStudies;
        }       

    }
}
