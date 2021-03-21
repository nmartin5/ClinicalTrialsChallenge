namespace ClinicalTrialsChallengeApi.Model.Core
{
    public class Status : Enumeration
    {
        public static readonly Status ActiveNotRecruiting = new("Active, not recruiting", 
            "Study is continuing, meaning participants are receiving an intervention or being examined, but new participants are not currently being recruited or enrolled");
        public static readonly Status ApprovedForMarketing = new("Approved for marketing");
        public static readonly Status Available = new("Available");
        public static readonly Status Completed = new("Completed",
            "The study has concluded normally; participants are no longer receiving an intervention or being examined (that is, last participant’s last visit has occurred)");
        public static readonly Status EnrollingByInvitation = new("Enrolling by invitation",
            "Participants are being (or will be) selected from a predetermined population");
        public static readonly  Status NoLongerAvailable = new("No longer available");
        public static readonly Status NotYetRecruiting = new("Not yet recruiting",
            "Participants are not yet being recruited");
        public static readonly Status Recruiting = new("Recruiting",
            "Participants are currently being recruited, whether or not any participants have yet been enrolled");
        public static readonly Status Suspended = new("Suspended",
            "Study halted prematurely but potentially will resume");
        public static readonly Status TemporarilyNotAvailable = new("Temporarily not available");
        public static readonly Status Terminated = new("Terminated",
            "Study halted prematurely and will not resume; participants are no longer being examined or receiving intervention");
        public static readonly Status UnknownStatus = new("Unknown status");
        public static readonly Status Withdrawn = new("Withdrawn",
            "Study halted prematurely, prior to enrollment of first participant");
        public static readonly Status Withheld = new("Withheld");

        private Status(string value, string description = null) : base(value, description) { }
    }
}
