using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.Database.Core;
using WorkingWomenApp.Database.enums;

namespace WorkingWomenApp.Database.Models.Users
{
    public class SecurityRole : IdentityRole<Guid>
    {
        //public Guid Id { get; set; }
       
        public virtual List<RolePermission> Permissions { get; set; } = new List<RolePermission>();

        public SecurityRole() : base() { }
        public SecurityRole(string roleName) : base(roleName) { }
    }

    public class RolePermission : Entity
    {
        #region Fields

        [ForeignKey("Permission")]
        public Guid PermissionId { get; set; }
        [ForeignKey("Role"), Required]
        public Guid RoleId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual PermissionType Permission { get; set; }
        public virtual SecurityRole Role { get; set; }

        #endregion
    }
    public class PermissionType : Entity
    {
        #region Fields

        public SecurityModule Module { get; set; }
        public SecuritySubModule SubModule { get; set; }
        public SecuritySystemAction SystemAction { get; set; }

        #endregion

        #region Navigation Properties

        public virtual List<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        #endregion
    }

    public class UserRoleMapping : Entity
    {
        #region Fields

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("SecurityRole")]
        public Guid SecurityRoleId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ApplicationUser User { get; set; }
        public virtual SecurityRole SecurityRole { get; set; }

        #endregion
    }
}
