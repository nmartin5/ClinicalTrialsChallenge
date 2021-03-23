using ClinicalTrialsChallengeApi.Infrastructure.Dto;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using OneOf;
using OneOf.Types;
using System;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.UseCase
{
    public class GetFullStudyUseCase : IGetFullStudyUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFullStudyUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<FullStudyDto, NotFound>> GetFullStudyAsync(string nctIdentifier)
        {
            if (string.IsNullOrWhiteSpace(nctIdentifier))
                throw new ArgumentException($"{nameof(nctIdentifier)} is required!");

            var study = await _unitOfWork.FullStudyRepository.GetFullStudyAsync(nctIdentifier);

            if (study is null)
                return new NotFound();

            return study;
        }
    }
}
