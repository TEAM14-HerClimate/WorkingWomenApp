namespace WorkingWomenApp.Database.Constants
{
    public static class Constants
    {
        /// <summary>
        /// Used in Emails
        /// </summary>
        public const string APPLICATION_NAME = "HerClimate";

        /// <summary>
        /// 50 MB
        /// </summary>
        public const long MaxUploadSize = 52428800;
        public static string MaxUploadSizeForUI => $"Max {(MaxUploadSize / 1048576)}MB";



        /// <summary>
        /// Create an administrator role
        /// </summary>
        public const string HerClimateAdministrator = "HerClimate Admin";
     
        public const string SuperUserName = "admin";
        public const string SuperRoleName = "administrator";
    }
}
