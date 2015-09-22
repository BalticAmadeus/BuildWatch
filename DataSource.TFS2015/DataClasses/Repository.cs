namespace DataSource.TFS2015.DataClasses
{
    public class Repository
    {
        public string Type { get; set; }
        public object Clean { get; set; }
        public bool CheckoutSubmodules { get; set; }
        public string Id { get; set; }
    }
}