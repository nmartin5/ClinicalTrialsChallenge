using ClinicalTrialsChallengeApi.Domain.Model.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ClinicalTrialsChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        [Route("list")]
        public IEnumerable<Status> GetStatuses()
        {
            return Enumeration.GetAll<Status>();
        }
    }
}
