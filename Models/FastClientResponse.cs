namespace LoginFrontEnd.Models
{
    public class FastClientResponse
    {
        public int statusCode { get; set; }
        public bool result { get; set; }
        public List<FastClient>? data { get; set; }
        public int totalClients { get; set; }
        public int totalPages { get; set; }
    }
}
