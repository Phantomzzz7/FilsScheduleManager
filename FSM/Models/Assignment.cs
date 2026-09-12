using System;
using System.Collections.Generic;
using System.Text;

namespace FSM.Models
{
    public class Assignment
    {

        public int ID { get; set; }
        public int CourseID { get; set; }

        public string Name { get; set; }= string.Empty;

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }


    }
}
