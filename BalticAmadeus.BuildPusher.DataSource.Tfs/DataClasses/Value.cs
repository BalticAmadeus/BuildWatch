namespace BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses
{
    public class value
    {
        public int id { get; set; }
        public string url { get; set; }
        public definition definition { get; set; }
        public string buildNumber { get; set; }
        public project2 project { get; set; }
        public string uri { get; set; }
        public string sourceVersion { get; set; }
        public string status { get; set; }
        public controller controller { get; set; }
        public string queueTime { get; set; }
        public string priority { get; set; }
        public string startTime { get; set; }
        public string finishTime { get; set; }
        public string reason { get; set; }
        public string result { get; set; }
        public requestedFor requestedFor { get; set; }
        public requestedBy requestedBy { get; set; }
	    public string lastChangedDate { get; set; }
        public lastChangedBy lastChangedBy { get; set; }
        public string parameters { get; set; }
        public logs logs { get; set; }
        public repository repository { get; set; }
        public bool keepForever { get; set; }
        public string sourceBranch { get; set; }
        public queue queue { get; set; }
        public orchestrationPlan orchestrationPlan { get; set; }
    }
}