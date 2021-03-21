using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Exceptions
{
    public class NotificationFailedException : Exception
    {
        public NotificationFailedException()
        {
        }

        public NotificationFailedException(string message) : base(message)
        {
        }

        public NotificationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotificationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
