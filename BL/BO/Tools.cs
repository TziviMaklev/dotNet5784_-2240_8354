using System;
using System.Collections;
using System.Reflection;
using System.Text;
namespace BO;

static internal class Tools
{
    public static string ToStringProperty<T>(this T obj)
    {
        StringBuilder sb = new StringBuilder();
        Type objType = obj.GetType();
        PropertyInfo[] properties = objType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object? propertyValue = property.GetValue(obj);

            if (propertyValue == null || (propertyValue is IEnumerable<object> collection && !collection.Any()))
                continue;

            sb.Append($"{property.Name}: ");

            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                IEnumerable<object> collection1 = (IEnumerable<object>)propertyValue;
                sb.Append("[ ");

                foreach (var item in collection1)
                {
                    sb.Append(item.ToString() + ", ");
                }

                if (sb.Length > 2)
                {
                    sb.Length -= 2;
                }

                sb.Append(" ]");
            }
            else
            {
                sb.Append(propertyValue.ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    internal static class Config
    {
        // Define the running variable
        private static int MilestoneId = 0;

        // Function that uses the running variable
        public static void NextMilestoneId()
        {
            // Increment the running variable
            MilestoneId++;
        }
    }
}
