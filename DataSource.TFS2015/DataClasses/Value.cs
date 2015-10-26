namespace DataSource.TFS2015.DataClasses
{
    public class Value
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Definition Definition { get; set; }
        public string BuildNumber { get; set; }
        public Project2 Project { get; set; }
        public string Uri { get; set; }
        public string SourceVersion { get; set; }
        public string Status { get; set; }
        public Controller Controller { get; set; }
        public string QueueTime { get; set; }
        public string Priority { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string Reason { get; set; }
        public string Result { get; set; }
        public RequestedFor RequestedFor { get; set; }
        public RequestedBy RequestedBy { get; set; }
        public string LastChangedDate { get; set; }
        public LastChangedBy LastChangedBy { get; set; }
        public string Parameters { get; set; }
        public Logs Logs { get; set; }
        public Repository Repository { get; set; }
        public bool KeepForever { get; set; }
        public string SourceBranch { get; set; }
        public Queue Queue { get; set; }
        public OrchestrationPlan OrchestrationPlan { get; set; }
    }
}