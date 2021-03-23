using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Domain.Model.Request;
using ClinicalTrialsChallengeApi.Domain.Model.Response;
using ClinicalTrialsChallengeApi.Domain.UseCase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ISendNotificationUseCase _sendNotificationUseCase;
        private readonly IGetEmailUseCase _getEmailUseCase;
        private readonly ISearchEmailUseCase _searchEmailUseCase;

        public EmailController(ISendNotificationUseCase sendContactUseCase, IGetEmailUseCase getEmailUseCase, ISearchEmailUseCase searchEmailUseCase)
        {
            _sendNotificationUseCase = sendContactUseCase;
            _getEmailUseCase = getEmailUseCase;
            _searchEmailUseCase = searchEmailUseCase;
        }

        [HttpPost]
        public async Task<ActionResult> Send([Required]NotificationRequest request)
        {
            var sendContactResult = await _sendNotificationUseCase.SendNotification(request);
            return sendContactResult.Match<ActionResult>(
                success => NoContent(),
                notfound => NotFound($"Study not found with NCT ID: {request.NctId}")
            );
        }

        [HttpGet]
        public async Task<ActionResult<Email>> Get([Required]Guid emailId)
        {
            var emailResult = await _getEmailUseCase.GetEmailAsync(emailId);

            return emailResult.Match<ActionResult<Email>>(
                email => email,
                notFound => NotFound($"Email not found with Id:{emailId}")
            );
        }

        [HttpGet]
        [Route("search")]
        public Task<PaginatedEmails> Search([FromQuery] PaginationRequestDto paginationRequest)
        {
            return _searchEmailUseCase.SearchEmailsAsync(paginationRequest);
        }
    }
}
