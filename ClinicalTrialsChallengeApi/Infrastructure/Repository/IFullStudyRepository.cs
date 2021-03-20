using ClinicalTrialsChallengeApi.Model;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IFullStudyRepository
    {
        Task<FullStudy> GetFullStudy(string nctIdentifier);
    }
}
