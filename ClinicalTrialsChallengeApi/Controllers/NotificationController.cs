using ClinicalTrialsChallengeApi.Domain.UseCase;
using ClinicalTrialsChallengeApi.Infrastructure.Dto.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ISendContactUseCase _sendContactUseCase;
        public NotificationController(ISendContactUseCase sendContactUseCase)
        {
            _sendContactUseCase = sendContactUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> SendContact(ContactRequest request)
        {
            var sendContactResult = await _sendContactUseCase.SendContact(request);
            return sendContactResult.Match<ActionResult>(
                success => NoContent(),
                notfound => NotFound("")
            );
        }
    }
}
