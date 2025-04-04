namespace LoginFrontEnd.Models
{
    public class FastClient
    {
        public int id { get; set; }
        public int member_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public long exp_date { get; set; }
        public int admin_enabled { get; set; }
        public int enabled { get; set; }
        public string admin_notes { get; set; }
        public string reseller_notes { get; set; }
        public string bouquet { get; set; }
        public int max_connections { get; set; }
        public int is_trial { get; set; }
        public long created_at { get; set; }
    }
}
