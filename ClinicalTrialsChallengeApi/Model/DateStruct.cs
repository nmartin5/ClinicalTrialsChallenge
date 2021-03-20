using System;

namespace ClinicalTrialsChallengeApi.Model
{
    public class DateStruct : IComparable<DateStruct>, IEquatable<DateStruct>
    {
        public DateTime Date { get; set; }
        public DateType DateType { get; set; }

        public DateStruct(string dateString, DateType dateType = DateType.Actual)
        {
            if (!DateTime.TryParse(dateString, out var result))
            {
                throw new ArgumentException($"{nameof(dateString)} is not in a valid date format!");
            }

            Date = result;
            DateType = dateType;
        }

        public int CompareTo(DateStruct other)
        {
            return DateTime.Compare(Date, other.Date);
        }

        public bool Equals(DateStruct other)
        {
            return DateTime.Equals(Date, other.Date) && Enum.Equals(DateType, other.DateType);
        }
    }
}

