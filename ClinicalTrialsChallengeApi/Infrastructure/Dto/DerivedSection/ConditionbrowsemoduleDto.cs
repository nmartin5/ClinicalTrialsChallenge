namespace ClinicalTrialsChallengeApi.Infrastructure.Dto
{
    public class ConditionbrowsemoduleDto
    {
        public ConditionmeshlistDto ConditionMeshList { get; set; }
        public ConditionancestorlistDto ConditionAncestorList { get; set; }
        public ConditionbrowseleaflistDto ConditionBrowseLeafList { get; set; }
        public ConditionbrowsebranchlistDto ConditionBrowseBranchList { get; set; }
    }
}
