namespace ClinicalTrialsChallengeApi.Infrastructure.Dto
{
    public class DesignmoduleDto
    {
        public string StudyType { get; set; }
        public string PatientRegistry { get; set; }
        public string TargetDuration { get; set; }
        public DesigninfoDto DesignInfo { get; set; }
        public EnrollmentinfoDto EnrollmentInfo { get; set; }
    }
}
