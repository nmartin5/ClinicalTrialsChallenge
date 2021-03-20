using ClinicalTrialsChallengeApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    // TODO NotificationController

    [ApiController]
    [Route("[controller]")]
    public class FullStudyController : ControllerBase
    {
        public FullStudyController()
        {

        }

        [HttpGet]
        public Task<FullStudy> GetAsync(string nctIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}
