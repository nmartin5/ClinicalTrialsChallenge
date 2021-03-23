using ClinicalTrialsChallengeApi.Domain.UseCase;
using ClinicalTrialsChallengeApi.Infrastructure.Client.Dto;
using ClinicalTrialsChallengeApi.Infrastructure.Repository;
using NSubstitute;
using NUnit.Framework;
using OneOf.Types;
using System;

namespace ClinicalTrialsChallengeUnitTests.UseCase
{
    [TestFixture]
    public class GetFullStudyUseCaseTests
    {
        private IUnitOfWork unitOfWork;
        private IFullStudyRepository fullStudyRepository;
        private GetFullStudyUseCase getFullStudyUseCase;

        [SetUp]
        public void SetUp()
        {
            unitOfWork = Substitute.For<IUnitOfWork>();
            fullStudyRepository = Substitute.For<IFullStudyRepository>();
            unitOfWork.FullStudyRepository.ReturnsForAnyArgs(fullStudyRepository);
            getFullStudyUseCase = new GetFullStudyUseCase(unitOfWork);
        }

        [Test]
        public void EmptyIdentifier_ThrowsArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(() => getFullStudyUseCase.GetFullStudyAsync(""));
        }

        [Test]
        public void NullStudy_ReturnsNotFound()
        {
            fullStudyRepository.GetFullStudyAsync("test").ReturnsForAnyArgs((FullStudyDto)null);

            var study = getFullStudyUseCase.GetFullStudyAsync("test").GetAwaiter().GetResult();
            Assert.IsInstanceOf<NotFound>(study.Value);
        }

        [Test]
        public void FoundStudy_ReturnsTheStudy()
        {
            var expectedStudy = new FullStudyDto();
            fullStudyRepository.GetFullStudyAsync("test").ReturnsForAnyArgs(expectedStudy);

            var study = getFullStudyUseCase.GetFullStudyAsync("test").GetAwaiter().GetResult();
            Assert.IsInstanceOf<FullStudyDto>(study.Value);
        }
    }
}