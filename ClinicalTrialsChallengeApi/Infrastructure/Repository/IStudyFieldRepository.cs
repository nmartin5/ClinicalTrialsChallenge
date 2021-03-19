using ClinicalTrialsChallengeApi.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IStudyFieldRepository
    {
        Task<IEnumerable<string>> GetStudyFieldsAsync();
    }
}
