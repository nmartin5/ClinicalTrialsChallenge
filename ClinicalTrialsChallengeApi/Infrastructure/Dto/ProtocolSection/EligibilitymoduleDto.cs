namespace ClinicalTrialsChallengeApi.Infrastructure.Dto
{
    public class EligibilitymoduleDto
    {
        public string EligibilityCriteria { get; set; }
        public string HealthyVolunteers { get; set; }
        public string Gender { get; set; }
        public StdagelistDto StdAgeList { get; set; }
        public string StudyPopulation { get; set; }
        public string SamplingMethod { get; set; }
    }
}
