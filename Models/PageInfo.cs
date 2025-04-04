namespace LoginFrontEnd.Models
{
    public class PageInfo
    {
        // crie as seguintes proprieades
        // "PageInfo": {
        // "pageSize": 0,
        // "totalRows": 0,
        // "isFirstPage": true,
        // "isLastPage": true,
        // "page": 0
        // }
        public int pageSize { get; set; }
        public int totalRows { get; set; }
        public bool isFirstPage { get; set; }
        public bool isLastPage { get; set; }
        public int page { get; set; }
    }
}