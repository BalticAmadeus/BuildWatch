namespace DataSource.TFS2015.DataClasses
{
    public class LastChangedBy
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string UniqueName { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsContainer { get; set; }
    }
}