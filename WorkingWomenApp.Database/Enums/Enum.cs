using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWomenApp.Database.enums
{
    public enum ArticleCategory
    {
        Health,
        Climate,
        GeneralKnowledge
    }
    public enum Profession
    {
        Health,
        Climate,
        GeneralKnowledge
    }
    public enum ChildcareCenter
    {
        School,
        Climate,
        GeneralKnowledge
    }

    public enum SecurityModule
    {Account=0,
        Settings = 1,
        Climate = 2,
        Health=3,
        Profile=4

    }
    public enum SecuritySubModule
    {
        [Description("System Users")]
        Account = 1,
        [Description("System Users")]
        SystemUsers = 2,
        [Description("Security Roles")]
        SecurityRoles = 3,
        [Description("Security Roles")]
        UserProfile = 4, 
        [Description("Security Roles")]
        ClimateAdmin = 5, 
        [Description("Security Roles")]
        HealthAdmin = 6,

    }
    public enum SecuritySystemAction
    {
        //generic actions under 1000
        [Description("Create and Edit")]
        CreateAndEdit = 1,
        Delete = 2,
        [Description("View List")]
        ViewList = 3,
        [Description("View")]
        ViewItem = 4,
    }
}
