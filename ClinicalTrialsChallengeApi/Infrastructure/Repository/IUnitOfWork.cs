namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        IEmailRepository EmailRepository { get; }
        IFullStudyRepository FullStudyRepository { get; }
        void Save();
    }
}
