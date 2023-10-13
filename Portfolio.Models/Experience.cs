using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Experience
    {
        public DateTime ProgrammingStartDate { get; set; }
        public int Years { get; private set; }
        public int Months { get; private set; }
        public int Days { get; private set; }
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public void CalculateTotalExperience()
        {
            if (ProgrammingStartDate == DateTime.MinValue)
            {
                // Set a default start date if not provided
                ProgrammingStartDate = new DateTime(2019, 8, 8);
            }

            DateTime currentDate = DateTime.Now;
            TimeSpan totalExperience = currentDate - ProgrammingStartDate;

            Years = totalExperience.Days / 365;
            totalExperience = totalExperience.Subtract(new TimeSpan(Years * 365, 0, 0, 0));

            Months = totalExperience.Days / 30;
            totalExperience = totalExperience.Subtract(new TimeSpan(Months * 30, 0, 0, 0));

            Days = totalExperience.Days;
            totalExperience = totalExperience.Subtract(new TimeSpan(Days, 0, 0, 0));

            Hours = totalExperience.Hours;
            Minutes = totalExperience.Minutes;
            Seconds = totalExperience.Seconds;
        }
    }
}