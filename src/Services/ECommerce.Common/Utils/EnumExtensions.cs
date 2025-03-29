using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Utils
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();

            }
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T? ToEnum<T>(this string value) where T : struct,Enum
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (Enum.TryParse<T>(value,true,out var result))
            {
                return result;

            }

            return null;
        }

        public static List<T> ToList<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static Dictionary<int, string> ToDictionary<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                                            .ToDictionary(k => Convert.ToInt32(k), v => v.ToString());
        }

        public static Dictionary<int, string> ToDictionaryWithDescription<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                                            .ToDictionary(k => Convert.ToInt32(k), v => v.GetDescription());
        }
    }
}
