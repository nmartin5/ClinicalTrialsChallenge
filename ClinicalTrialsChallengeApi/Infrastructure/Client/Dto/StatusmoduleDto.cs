namespace ClinicalTrialsChallengeApi.Infrastructure.Client.Dto
{
    public class StatusmoduleDto
    {
        public string StatusVerifiedDate { get; set; }
        public string OverallStatus { get; set; }
        public ExpandedaccessinfoDto ExpandedAccessInfo { get; set; }
        public StartdatestructDto StartDateStruct { get; set; }
        public PrimarycompletiondatestructDto PrimaryCompletionDateStruct { get; set; }
        public CompletiondatestructDto CompletionDateStruct { get; set; }
        public string StudyFirstSubmitDate { get; set; }
        public string StudyFirstSubmitQCDate { get; set; }
        public StudyfirstpostdatestructDto StudyFirstPostDateStruct { get; set; }
        public string LastUpdateSubmitDate { get; set; }
        public LastupdatepostdatestructDto LastUpdatePostDateStruct { get; set; }
    }
}
