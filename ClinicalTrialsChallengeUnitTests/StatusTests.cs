using ClinicalTrialsChallengeApi.Domain.Model.Core;
using NUnit.Framework;
using System.Linq;

namespace ClinicalTrialsChallengeUnitTests
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
        public void OverallStatus_GetAll_ReturnsExpectedCount()
        {
            var results = Enumeration.GetAll<Status>();

            Assert.AreEqual(14, results.Count());
        }

        [Test]
        public void OverallStatus_GetAll_ReturnsExpectedCountWithDescriptions()
        {
            var results = Enumeration.GetAll<Status>();

            Assert.AreEqual(8, results.Count(r => !string.IsNullOrWhiteSpace(r.Description)));
        }
    }
}