using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Domain.Model.Core
{
    public abstract class Enumeration
    {
        public string Value { get; }
        public string Description { get; } = null;

        protected Enumeration(string value, string description = null)
        {
            Value = value;
            Description = description;
        }

        public static IEnumerable<T> GetAll<T>() where T: Enumeration
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType.Equals(typeof(T)))
                .Select(s => (T)s.GetValue(null)).ToList();
        }
        
    }
}
