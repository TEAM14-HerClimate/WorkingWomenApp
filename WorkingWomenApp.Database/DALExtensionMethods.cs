using System.Collections;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using WorkingWomenApp.Database.Models.Users;

namespace WorkingWomenApp.Database
{
    public static class DALExtensionMethods
    {
        public static bool IsSuperUser(this string UserName)
        {
            return UserName?.LocaleCompare(Constants.Constants.HerClimateAdministrator, true) ?? false;
        }

     

        public static bool IsSuperRole(this SecurityRole model)
        {
            return model?.Name.LocaleCompare(Constants.Constants.SuperRoleName, true) ?? false;
        }
        public static bool IsUiaAdminRole(this SecurityRole model)
        {
            return model?.Name.LocaleCompare(Constants.Constants.HerClimateAdministrator, true) ?? false;
        }

        #region Logging

        private static string[] _IgnoreFieldNames = new[] { "PasswordHash", "SecurityStamp", "LockoutEnd", "LockoutEnabled", "AccessFailedCount"
        ,"ConcurrencyStamp","PhoneNumberConfirmed","TwoFactorEnabled","NormalizedUserName","EmailConfirmed"};

        private static Type[] _GenericLogTypes = new[] {typeof(decimal),typeof(int),typeof(DateTime),typeof(bool),typeof(string)
        ,typeof(decimal?),typeof(int?),typeof(DateTime?),typeof(bool?)};
        public static string GenerateLogXML(this object Model, string PreferredName = null)
        {
            if (Model == null) return null;

            var ModelName = PreferredName ?? Model.GetType().Name;

            var modelType = Model.GetType();

            var xml = $"<{ModelName}>";

            if (modelType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(modelType))
            {
                foreach (var value in Model as IEnumerable)
                {
                    xml += value.GenerateLogXML();
                }
            }
            else
            {
                foreach (var property in Model.GetType().GetProperties())
                {
                    if (_IgnoreFieldNames.Contains(property.Name)) continue;

                    var propertyType = property.PropertyType;
                    var propertyValue = property.GetValue(Model);
                    bool renderProperty = false;

                    var propertyXML = $"<{property.Name}>";
                    if (_GenericLogTypes.Contains(propertyType))//generic type
                    {
                        renderProperty = true;
                        propertyXML += propertyValue?.ToString() ?? "";
                    }
                    else if (propertyType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(propertyType))//List
                    {
                        renderProperty = true;
                        if (propertyValue == null)//null
                        {
                            propertyXML += "";
                        }
                        else//has elements
                        {
                            foreach (var value in propertyValue as IEnumerable)
                            {
                                propertyXML += value.GenerateLogXML();
                            }
                        }
                    }
                    else if (propertyType.IsEnum || (Nullable.GetUnderlyingType(propertyType)?.IsEnum ?? false))//enum
                    {
                        renderProperty = true;
                        propertyXML += propertyValue == null ? "" :
                            Enum.GetName(propertyType.IsEnum ? propertyType : Nullable.GetUnderlyingType(propertyType), propertyValue);
                    }

                    propertyXML += $"</{property.Name}>";

                    if (renderProperty) xml += propertyXML;
                }
            }

            xml += $"</{ModelName}>";

            return xml;
        }

        #endregion

        public static string GetEnumName(this Enum Value)
        {
            if (Value !=null)
            {
                return GetDescriptionAttributeValue(Value) ?? Value.ToLocaleString().LocaleReplace("__", "-").LocaleReplace("_", " ");
            }
            else
            {
                return "";
            }
            
        }

        private static string GetDescriptionAttributeValue<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return null;
        }

        public static long? GetIntValue(this Enum Value)
        {
            return Value == null ? (int?)null : Convert.ToInt32(Value, FormatProviderToUse);
        }

        #region Strings

        public static System.Globalization.CultureInfo CultureToUse => System.Globalization.CultureInfo.InvariantCulture;
        public static IFormatProvider FormatProviderToUse => CultureToUse;

        public static bool LocaleCompare(this string value, string valueToCompare, bool ignoreCase)
        {
            return string.Compare(value, valueToCompare, ignoreCase, CultureToUse) == 0;
        }

        public static string ToLocaleString(this object value)
        {
            return value?.ToString();
        }

        public static string LocaleReplace(this string value, string oldValue, string newValue, bool ignoreCase = false)
        {
            return value?.Replace(oldValue, newValue, ignoreCase, CultureToUse);
        }

        #endregion

        public static byte[] GetFileBytes(this IFormFile model)
        {
            using (var reader = new BinaryReader(model.OpenReadStream()))
            {
                return reader.ReadBytes((int)model.Length);
            }
        }
    }
}
