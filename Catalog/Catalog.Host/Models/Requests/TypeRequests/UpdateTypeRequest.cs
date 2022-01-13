namespace Catalog.Host.Models.Requests.TypeRequests
{
    public class UpdateTypeRequest
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;
    }
}