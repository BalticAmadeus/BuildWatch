namespace DataSource.TFS2015.DataClasses
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public int Revision { get; set; }
    }
}
