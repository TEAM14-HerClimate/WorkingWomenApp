namespace WorkingWomenApp.BLL.QueryFilters
{
    public class UserQueryFilter: BaseQueryFilter 
    {
        public string Name { get; set; }
        public string Surnames { get; set; }
        public DateTime? Bithdate { get; set; }
    }
}
