using ClinicalTrialsChallengeApi.Infrastructure.Client;
using ClinicalTrialsChallengeApi.Infrastructure.Persistence;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmailRepository EmailRepository { get; }
        public IFullStudyRepository FullStudyRepository { get; }

        private readonly ClinicalTrialsDbContext _context;

        public UnitOfWork(ClinicalTrialsDbContext context, IFullStudiesClient fullStudiesClient)
        {
            _context = context;
            EmailRepository = new EmailRepository(context);
            FullStudyRepository = new FullStudyRepository(fullStudiesClient);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
