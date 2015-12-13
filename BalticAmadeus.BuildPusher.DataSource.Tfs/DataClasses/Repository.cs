namespace BalticAmadeus.BuildPusher.DataSource.Tfs.DataClasses
{
    public class repository
    {
        public string type { get; set; }
        public object clean { get; set; }
        public bool checkoutSubmodules { get; set; }
        public string id { get; set; }
    }
}