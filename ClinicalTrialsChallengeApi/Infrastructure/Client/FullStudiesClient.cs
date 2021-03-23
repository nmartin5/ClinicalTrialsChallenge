using ClinicalTrialsChallengeApi.Domain.Model.Response;
using ClinicalTrialsChallengeApi.Infrastructure.Client.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Client
{
    public class FullStudiesClient : IFullStudiesClient
    {
        private readonly HttpClient _httpClient;
        public FullStudiesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PaginatedFullStudyDto> GetFullStudiesAsync(string expression, int skip, int take)
        {
            var minRank = skip + 1;
            var maxRank = (minRank + take) - 1;


            var response = await _httpClient.GetAsync($"https://clinicaltrials.gov/api/query/full_studies?expr={expression}&min_rnk={minRank}&max_rnk={maxRank}&fmt=json");

            if (response.StatusCode.Equals(HttpStatusCode.ServiceUnavailable))
            {
                return new PaginatedFullStudyDto(new List<FullStudyDto>(), new Pagination(0, 0, 0));
            }

            response.EnsureSuccessStatusCode();

            if (response.Content is object)
            {
                var httpResult = await response.Content.ReadFromJsonAsync<Response>();

                if (httpResult is null || httpResult.FullStudiesResponse.NStudiesReturned == 0)
                    return null;

                var studies = httpResult.FullStudiesResponse.FullStudies.OrderBy(r => r.Rank).Select(s => s.Study);
                var total = httpResult.FullStudiesResponse.NStudiesFound;

                return new PaginatedFullStudyDto(studies, new Pagination(skip, take, total));
            }
            else
            {
                throw new HttpRequestException("HTTP response was invalid and cannot be deserialized");
            }
        }

        private class Response
        {
            public FullStudiesResponse FullStudiesResponse { get; set; }
        }
        private class FullStudiesResponse
        {
            public int NStudiesReturned { get; set; }
            public int NStudiesFound { get; set; }
            public IEnumerable<RankedFullStudyDto> FullStudies { get; set; }
        }

        private class RankedFullStudyDto
        {
            public int Rank { get; set; }
            public FullStudyDto Study { get; set; }
        }
    }
}
