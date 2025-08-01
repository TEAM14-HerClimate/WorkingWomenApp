using System.Reflection;
using WorkingWomenApp.Attribute;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Helpers
{
    public class PermissionHelper
    {
        public static List<PermissionType> GetPermissionTypes()
        {
            var allControllers = GetAllControllers();

            var baseAttrs = (from a in allControllers
                             from b in a.GetMethods()
                             where b.GetCustomAttributes().Any(r => r is IProtectActionAttribute)
                             from c in b.GetCustomAttributes().Where(r => r is IProtectActionAttribute)
                             select (IProtectActionAttribute)c);

            var protectedActionAttributes = baseAttrs.Select(r => new
            {
                Module = r.Module,
                SubModule = r.SubModule,
                SystemAction = r.SystemAction
            }).Distinct()
                .Select(r => new PermissionType
                {
                    Module = r.Module,
                    SubModule = r.SubModule,
                    SystemAction = r.SystemAction
                }).ToList();

            return protectedActionAttributes;
        }

        public static IEnumerable<Type> GetAllControllers()
        {
            var allControllers = from a in Assembly.GetExecutingAssembly().GetTypes().Where(r => r.FullName.StartsWith("PromiseCRM.Controllers")
                    || (r.FullName.StartsWith("PromiseCRM") && r.FullName.Contains("Controllers")))
                select a;

            return allControllers;
        }
    }
}
