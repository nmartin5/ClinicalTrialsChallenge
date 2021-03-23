using ClinicalTrialsChallengeApi.Infrastructure.Client;
using ClinicalTrialsChallengeApi.Infrastructure.Client.Dto;
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
        private readonly Dictionary<string, FullStudyDto> _fullStudyCache = new();
        private readonly IFullStudiesClient _fullStudiesClient;
        public FullStudyRepository(IFullStudiesClient fullStudiesClient)
        {
            _fullStudiesClient = fullStudiesClient;
        }

        public async Task<FullStudyDto> GetFullStudyAsync(string nctIdentifier)
        {
            if (string.IsNullOrWhiteSpace(nctIdentifier))
                throw new ArgumentException($"{nameof(nctIdentifier)} is required!");

            if (!Regex.IsMatch(nctIdentifier, "(NCT)\\d{8}"))
                throw new ArgumentException($"{nameof(nctIdentifier)} does not match the expected format! Expected format: NCT00000000");

            if (_fullStudyCache.TryGetValue(nctIdentifier, out var result))
                return result;

            var expression = $"EXPANSION[None]AREA[NCTId]\"{nctIdentifier}\"";

            var foundStudies = await _fullStudiesClient.GetFullStudiesAsync(expression, 0, 1);

            if (foundStudies is null)
                return null;

            var foundStudy = foundStudies.FullStudies.SingleOrDefault(s => s.ProtocolSection.IdentificationModule.NCTId.Equals(nctIdentifier));
            if (foundStudy != null)
                _fullStudyCache.Add(nctIdentifier, foundStudy);

            return foundStudy;
        }

        public async Task<PaginatedFullStudyDto> GetPaginatedFullStudiesAsync(int skip, int take, IEnumerable<string> keywords,
            string location = null, IEnumerable<string> filterStatuses = null, string gender = null, bool centralContactRequired = false)
        {
            if (skip < 0)
                throw new ArgumentException($"{nameof(skip)} cannot be negative!");

            if (take < 0)
                throw new ArgumentException($"{nameof(take)} cannot be negative!");

            if (!keywords.Any(k => !string.IsNullOrWhiteSpace(k)))
                throw new ArgumentException($"{nameof(keywords)} cannot be empty!");            

            var keywordInclusiveClauseSb = new StringBuilder("(");
            keywordInclusiveClauseSb.Append(string.Join(" OR ", keywords));
            keywordInclusiveClauseSb.Append(')');

            if (!string.IsNullOrWhiteSpace(location))
                keywordInclusiveClauseSb.Append($" AND (SEARCH[Location] ({location}))");

            if (!string.IsNullOrWhiteSpace(gender) && (gender.Equals("Male", StringComparison.OrdinalIgnoreCase) || gender.Equals("Female",  StringComparison.OrdinalIgnoreCase)))
                keywordInclusiveClauseSb.Append($" AND (EXPANSION[None]AREA[Gender]\"{gender}\")");

            if (filterStatuses != null && filterStatuses.Any(s => !string.IsNullOrWhiteSpace(s)))
            {
                var nonEmptyFilterStatuses = filterStatuses.Where(s => !string.IsNullOrWhiteSpace(s));

                keywordInclusiveClauseSb.Append(" AND (EXPANSION[None]AREA[OverallStatus]\"");
                keywordInclusiveClauseSb.Append(string.Join("\" OR EXPANSION[None]AREA[OverallStatus]\"", nonEmptyFilterStatuses));
                keywordInclusiveClauseSb.Append("\")");
            }

            if (centralContactRequired)
            {
                keywordInclusiveClauseSb.Append(" AND (NOT AREA[CentralContactEMail]MISSING)");
            }

            var foundStudies = await _fullStudiesClient.GetFullStudiesAsync(keywordInclusiveClauseSb.ToString(), skip, take);

            return foundStudies;
        }

    }
}
