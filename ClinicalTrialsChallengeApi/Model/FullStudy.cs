using ClinicalTrialsChallengeApi.Model.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Model
{
    public class FullStudy
    {
        public ProtocolSection ProtocolSection { get; set; }
        public DerivedSection DerivedSection { get; set; }
    }

    public class ProtocolSection
    {
        public IdentificationModule IdentificationModule { get; set; }
        public StatusModule StatusModule { get; set; }
        public SponsorCollaboratorsModule SponsorCollaboratorsModule { get; set; }
        public bool OversightHasDMC { get; set; }
        public DescriptionModule DescriptionModule { get; set; }
        public ConditionsModule ConditionsModule { get; set; }
        public DesignModule DesignModule { get; set; }
        public ArmsInterventionsModule ArmsInterventionsModule { get; set; }
        public OutcomesModule OutcomesModule { get; set; }
        public EligibilityModule EligibilityModule { get; set; }
        public ContactsLocationsModule ContactsLocationsModule { get; set; }
        public IEnumerable<Reference> References { get; set; }
    }

    public class IdentificationModule
    {
        public string NCTId { get; set; }
        public string OrgStudyId { get; set; }
        public Organization Organization { get; set; }
        public string BriefTitle { get; set; }
        public string OfficialTitle { get; set; }
        public string Acronym { get; set; }
    }

    public class Organization
    {
        public string FullName { get; set; }
        public string Class { get; set; }
    }

    public class StatusModule
    {
        public DateStruct VerifiedDate { get; set; }
        public Status OverallStatus { get; set; }
        public bool HasExpandedAccess { get; set; }
        public DateStruct StartDate { get; set; }
        public DateStruct PrimaryCompletionDateStruct { get; set; }
        public DateStruct CompletionDateStruct { get; set; }
        public DateStruct StudyFirstSubmitDate { get; set; }
        public DateStruct StudyFirstSubmitQCDate { get; set; }
        public DateStruct StudyFirstPostDateStruct { get; set; }
        public DateStruct LastUpdateSubmitDate { get; set; }
        public DateStruct LastUpdatePostDateStruct { get; set; }
    }

    public class SponsorCollaboratorsModule
    {
        public ResponsibleParty ResponsibleParty { get; set; }
        public LeadSponsor LeadSponsor { get; set; }
    }

    public class ResponsibleParty
    {
        public string OldNameTitle { get; set; }
        public string OldOrganization { get; set; }
    }

    public class LeadSponsor
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }

    public class DescriptionModule
    {
        public string BriefSummary { get; set; }
        public string DetailedDescription { get; set; }
    }

    public class ConditionsModule
    {
        public IEnumerable<string> Conditions { get; set; }
        public IEnumerable<string> Keywords { get; set; }
    }

    public class DesignModule
    {
        public string StudyType { get; set; }
        public IEnumerable<string> Phases { get; set; }
        public DesignInfo DesignInfo { get; set; }
        public EnrollmentInfo EnrollmentInfo { get; set; }
    }

    public class DesignInfo
    {
        public string DesignAllocation { get; set; }
        public string DesignInterventionModel { get; set; }
        public string DesignPrimaryPurpose { get; set; }
        public string DesignMasking { get; set; }
    }

    public class EnrollmentInfo
    {
        public string EnrollmentCount { get; set; }
        public string EnrollmentType { get; set; }
    }

    public class ArmsInterventionsModule
    {
        public IEnumerable<ArmGroup> ArmGroups { get; set; }
        public IEnumerable<Intervention> Interventions { get; set; }
    }

    public class ArmGroup
    {
        public string ArmGroupLabel { get; set; }
        public string ArmGroupType { get; set; }
        public string ArmGroupDescription { get; set; }
        public IEnumerable<string> ArmGroupInterventionNames { get; set; }
    }

    public class Intervention
    {
        public string InterventionType { get; set; }
        public string InterventionName { get; set; }
        public string InterventionDescription { get; set; }
        public IEnumerable<string> InterventionArmGroupLabels { get; set; }
    }

    public class OutcomesModule
    {
        public IEnumerable<Outcome> PrimaryOutcomes { get; set; }
        public IEnumerable<Outcome> SecondaryOutcomes { get; set; }
    }

    public class Outcome
    {
        public string Measure { get; }
        public string Description { get; }
        public string TimeFrame { get; }

        public Outcome(string measure, string description, string timeFrame)
        {
            Measure = measure;
            Description = description;
            TimeFrame = timeFrame;
        }
    }

    public class EligibilityModule
    {
        public string EligibilityCriteria { get; set; }
        public string HealthyVolunteers { get; set; }
        public string Gender { get; set; }
        public string MinimumAge { get; set; }
        public IEnumerable<string> StdAges { get; set; }
    }

    public class ContactsLocationsModule
    {
        public IEnumerable<OverallOfficial> OverallOfficials { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }

    public class OverallOfficial
    {
        public string Name { get; set; }
        public string Affiliation { get; set; }
        public string Role { get; set; }
    }

    public class Location
    {
        public string Facility { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }

    public class Reference
    {
        public string ReferencePMID { get; set; }
        public string ReferenceType { get; set; }
        public string ReferenceCitation { get; set; }
    }

    public class DerivedSection
    {
        public string VersionHolder { get; set; }
        public ConditionBrowseModule ConditionBrowseModule { get; set; }
    }

    public class ConditionBrowseModule
    {
        public IEnumerable<Condition> ConditionMeshes { get; set; }
        public IEnumerable<Condition> ConditionAncestors { get; set; }
        public IEnumerable<ConditionBrowseLeaf> ConditionBrowseLeaves { get; set; }
        public IEnumerable<ConditionBrowseBranch> ConditionBrowseBranches { get; set; }
    }

    public class Condition
    {
        public string Id { get; private set; }
        public string Term { get; private set; }
    }

    public class ConditionBrowseLeaf
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AsFound { get; set; }
        public string Relevance { get; set; }
    }

    public class ConditionBrowseBranch
    {
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}

