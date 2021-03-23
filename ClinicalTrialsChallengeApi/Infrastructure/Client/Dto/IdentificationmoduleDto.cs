namespace ClinicalTrialsChallengeApi.Infrastructure.Client.Dto
{
    public class IdentificationmoduleDto
    {
        public string NCTId { get; set; }
        public OrgstudyidinfoDto OrgStudyIdInfo { get; set; }
        public SecondaryidinfolistDto SecondaryIdInfoList { get; set; }
        public OrganizationDto Organization { get; set; }
        public string BriefTitle { get; set; }
        public string OfficialTitle { get; set; }
        public string Acronym { get; set; }
    }
}
