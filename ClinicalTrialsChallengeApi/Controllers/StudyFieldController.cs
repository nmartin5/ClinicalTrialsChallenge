using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyFieldController : ControllerBase
    {
        private readonly IStudyFieldRepository studyFieldRepository;        

        public StudyFieldController(IStudyFieldRepository studyFieldRepository)
        {
            this.studyFieldRepository = studyFieldRepository;
        }

        [HttpGet]
        public Task<IEnumerable<string>> GetAsync()
        {
            return studyFieldRepository.GetStudyFieldsAsync();
        }
    }
}
