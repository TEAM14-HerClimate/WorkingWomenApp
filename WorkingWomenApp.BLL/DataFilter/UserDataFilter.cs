using WorkingWomenApp.BLL.QueryFilters;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.BLL.DataFilter
{
    public static class UserDataFilter
    {
        public static IEnumerable<ApplicationUser> FilterUsers(IEnumerable<ApplicationUser> users, UserQueryFilter userQueryFilter)
        {
            if (!string.IsNullOrEmpty(userQueryFilter.Name) && users != null)
                users = FilterByName(users, userQueryFilter.Name);

            if (!string.IsNullOrEmpty(userQueryFilter.Surnames) && users != null)
                users = FilterBySurnames(users, userQueryFilter.Surnames);

            return users;
        }
        private static IEnumerable<ApplicationUser> FilterByName(IEnumerable<ApplicationUser> users, string name) =>
            users.Where(x => x.FirstName.ToLower().Contains(name.ToLower()));

        private static IEnumerable<ApplicationUser> FilterBySurnames(IEnumerable<ApplicationUser> users, string surnames) =>
           users.Where(x => x.LastName.ToLower().Trim().Contains(surnames.ToLower().Trim()));

        private static IEnumerable<ApplicationUser> FilterByDate(IEnumerable<ApplicationUser> urers, string? username) =>
            urers.Where(x => x.UserName.ToLower().Trim().Contains(username.ToLower().Trim()));
    }
}
