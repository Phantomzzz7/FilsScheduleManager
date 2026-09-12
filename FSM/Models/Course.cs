using System;
using System.Collections.Generic;
using System.Text;

namespace FSM.Models
{
    public class Course
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Professor { get; set; } = string.Empty;

        public string Room { get; set; } = string.Empty;
    }
}
