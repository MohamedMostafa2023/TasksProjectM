namespace TasksProjectM.Server.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public int? TaskGroupId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int? TaskStatusId { get; set; }

        //public TaskGroup TaskGroup { get; set; }
        //public TaskStatus TaskStatus { get; set; }
    }
}
