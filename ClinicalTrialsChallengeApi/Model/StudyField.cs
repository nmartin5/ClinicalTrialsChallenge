using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicalTrialsChallengeApi.Model
{
    public class StudyField
    {
        public string Name { get; private set; }

        public StudyField(string name)
        {
            Name = name;
        }
    }
}
