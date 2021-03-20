using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using ClinicalTrialsChallengeApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public class FullStudyRepository : IFullStudyRepository
    {
        private Dictionary<string, FullStudy> _fullStudyCache = new Dictionary<string, FullStudy>();
        private readonly HttpClient _httpClient;
        public FullStudyRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<FullStudy> GetFullStudy(string nctIdentifier)
        {
            if (string.IsNullOrWhiteSpace(nctIdentifier))
                throw new ArgumentException($"{nameof(nctIdentifier)} is required!");

            // TODO make this unit testable
            if (!Regex.IsMatch(nctIdentifier, "(NCT)\\d{8}"))
                throw new ArgumentException($"{nameof(nctIdentifier)} does not match the expected format! Expected format: NCT00000000");

            if (_fullStudyCache.TryGetValue(nctIdentifier, out var result))
                return result;

            // get the study via the api
            var response = await _httpClient.GetAsync($"https://clinicaltrials.gov/api/query/full_studies?expr={nctIdentifier}&min_rnk=1&max_rnk=1&fmt=json");
            response.EnsureSuccessStatusCode();

            if (response.Content is object)
            {
                var fullStudiesResponse = await response.Content.ReadFromJsonAsync<FullStudiesResponse>();

                if (fullStudiesResponse.NStudiesReturned == 0)
                    return null;

                var studyDto = fullStudiesResponse.FullStudies.Single().Study;
                FullStudy fullStudy = new FullStudy(); // TODO build per required fields
                _fullStudyCache.Add(nctIdentifier, fullStudy);
                return fullStudy;
            }
            else
            {
                throw new HttpRequestException("HTTP response was invalid and cannot be deserialized");
            }
        }

        private class FullStudiesResponse
        {
            public int NStudiesReturned { get; set; }
            public IEnumerable<RankedFullStudyDto> FullStudies { get; set; }
        }

        private class RankedFullStudyDto
        {
            public StudyDto Study { get; set; }
        }

    }
}
