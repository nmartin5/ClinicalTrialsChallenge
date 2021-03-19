using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public class StudyFieldRepository : IStudyFieldRepository
    {
        private readonly HttpClient _client;
        private List<string> _studyFields = new List<string>();

        public StudyFieldRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<string>> GetStudyFieldsAsync()
        {
            var response = await _client.GetAsync("https://clinicaltrials.gov/api/info/study_fields_list?fmt=json");
            response.EnsureSuccessStatusCode();

            if (response.Content is object)
            {
                var studyFieldsListResponse = await response.Content.ReadFromJsonAsync<StudyFieldsListResponse>();

                _studyFields = studyFieldsListResponse.StudyFields.Fields.ToList();
                return _studyFields;
            }
            else
            {
                throw new HttpRequestException("HTTP response was invalid and cannot be deserialized");
            }
        }

        private class StudyFieldsListResponse
        {
            public StudyFields StudyFields { get; set; }
        }

        private class StudyFields
        {
            public IEnumerable<string> Fields { get; set; }
        }
    }
}
