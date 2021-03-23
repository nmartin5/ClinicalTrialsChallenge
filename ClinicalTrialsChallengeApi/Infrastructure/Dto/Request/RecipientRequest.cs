using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClinicalTrialsChallengeApi.Infrastructure.Dto.Request
{
    public record RecipientRequest([EmailAddress]string RecipientAddress, string RecipientName);
}
