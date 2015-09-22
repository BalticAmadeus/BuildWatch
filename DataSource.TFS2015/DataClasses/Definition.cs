namespace DataSource.TFS2015.DataClasses
{
    public class Definition
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? Revision { get; set; }
        public Project Project { get; set; }
    }
}