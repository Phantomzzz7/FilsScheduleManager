using System;
using System.Collections.Generic;
using System.Text;

namespace FSM.Models
{
    public class ScheduleItem
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        public string ActivityType { get; set; } = string.Empty;// e.g., Lecture, Lab

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
