
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Attribute
{
    public interface IProtectActionAttribute
    {
        
            SecurityModule Module { get; set; }
            SecuritySubModule SubModule { get; set; }
            SecuritySystemAction SystemAction { get; set; }
        
    }
}
