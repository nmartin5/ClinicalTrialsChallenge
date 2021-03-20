using ClinicalTrialsChallengeApi.Model;
using NUnit.Framework;
using System;

namespace ClinicalTrialsChallengeUnitTests
{
    [TestFixture]
    public class DateStructTests
    {
        [TestCase("December 2009")]
        [TestCase("December 19, 2009")]
        public void DateStructCompare_Preceeds_ReturnsNegativeValue(string dateString)
        {
            var dateStruct = new DateStruct(dateString, DateType.Actual);
            var compareToDate = dateStruct.Date.AddMonths(1);

            var compareTo = new DateStruct(compareToDate.ToString(), DateType.Actual);

            Assert.Negative(dateStruct.CompareTo(compareTo));
        }

        [TestCase("December 2009")]
        [TestCase("December 19, 2009")]
        public void DateStructCompare_Follows_ReturnsPositiveValue(string dateString)
        {
            var dateStruct = new DateStruct(dateString, DateType.Actual);
            var compareToDate = dateStruct.Date.AddMonths(-1);

            var compareTo = new DateStruct(compareToDate.ToString(), DateType.Actual);

            Assert.Positive(dateStruct.CompareTo(compareTo));
        }

        [TestCase("December 2009")]
        [TestCase("December 19, 2009")]
        public void DateStructCompare_Matches_ReturnsZero(string dateString)
        {
            var dateStruct = new DateStruct(dateString, DateType.Actual);

            var compareTo = new DateStruct(dateStruct.Date.ToString(), DateType.Actual);

            Assert.Zero(dateStruct.CompareTo(compareTo));
        }

        [TestCase("")]
        [TestCase("December")]
        [TestCase("D ecember 2010")]
        [TestCase("December 32, 2009")]
        public void DateStructCreate_InvalidDateString_ThrowsArgumentException(string dateString)
        {
            Assert.Throws<ArgumentException>(() => new DateStruct(dateString, DateType.Actual));
        }
    }
}