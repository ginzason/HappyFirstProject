
namespace Happy.Models
{
    public class UserMenu
    {
        public int menu_idx { get; set; }
        public int parent_idx { get; set; }
        public string menu_name { get; set; }
        public string menu_url { get; set; }
        public string page_url { get; set; }
        public int sort_order { get; set; }
    }

    public class AuthMenu
    {
        public int menu_idx { get; set; }
        public int parent_idx { get; set; }
        public int au_idx { get; set; }
        public string menu_name { get; set; }
        public string menu_url { get; set; }
        public string page_url { get; set; }
        public int sort_order { get; set; }
    }
}