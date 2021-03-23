using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using ClinicalTrialsChallengeApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ClinicalTrialsDbContext _context;
        public EmailRepository(ClinicalTrialsDbContext context)
        {
            _context = context;
        }

        public void Add(Email email)
        {
            _context.Add(email);
        }

        public ValueTask<Email> GetAsync(Guid id)
        {
            return _context.Emails.FindAsync(id);
        }

        public async Task<IEnumerable<Email>> GetAsync(int skip, int take)
        {
            return await _context.Emails
                .Include(e => e.Recipient)
                .Include(e => e.Attachments)
                .OrderByDescending(e => e.Sent)
                .Skip(skip).Take(take)
                .ToListAsync();
        }

        public Task<int> GetCountAsync()
        {
            return _context.Emails.CountAsync();
        }
    }
}
