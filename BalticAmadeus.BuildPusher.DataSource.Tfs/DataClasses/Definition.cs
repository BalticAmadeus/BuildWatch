namespace BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses
{
    public class definition
    {
        public string type { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int? revision { get; set; }
        public project project { get; set; }
    }
}