namespace BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses
{
    public class requestedBy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string uniqueName { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public bool isContainer { get; set; }
    }
}