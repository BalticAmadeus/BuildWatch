namespace BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses
{
    public class project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
    }
}
