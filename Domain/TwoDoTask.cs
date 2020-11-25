using System;
using System.Collections.Generic;
using System.Text;

namespace TwoDo.Domain
{
    class TwoDoTask
    {
        public int Id { get; internal set; }

        public string TaskName { get; internal set; }

        public DateTime DueDate { get; internal set; }

        public TwoDoTask(string taskName, DateTime dueDate)
        {
            TaskName = taskName;

            DueDate = dueDate;
        }

        public TwoDoTask(int id, string taskName, DateTime dueDate)
        {
            Id = id;
            
            TaskName = taskName;

            DueDate = dueDate;
        }



    }
}
