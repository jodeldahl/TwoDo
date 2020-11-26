using System;


namespace TwoDo.Domain
{
    class TwoDoTask
    {
        public int Id { get; protected set; }

        public string TaskName { get; protected set; }

        public DateTime DueDate { get; protected set; }

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
