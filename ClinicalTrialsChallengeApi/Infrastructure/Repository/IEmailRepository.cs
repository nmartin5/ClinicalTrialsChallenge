using ClinicalTrialsChallengeApi.Domain.Model.Notification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Infrastructure.Repository
{
    public interface IEmailRepository
    {
        void Add(Email email);
        ValueTask<Email> GetAsync(Guid id);
        Task<IEnumerable<Email>> GetAsync(int skip, int take);
        Task<int> GetCountAsync();
    }
}
