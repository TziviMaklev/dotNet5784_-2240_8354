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
        Type objType = obj!.GetType();
        PropertyInfo[] properties = objType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            sb.Append($"{property.Name}: ");

            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
            {
                IEnumerable collection = (IEnumerable)property.GetValue(obj);
                sb.Append("[ ");
                foreach (var item in collection!)
                {
                    sb.Append(item.ToString() + ", ");
                }

                if (sb.Length > 2)
                {
                    sb.Length -= 2; // Remove the trailing comma and space
                }

                sb.Append(" ]");
            }
            else
            {
                sb.Append(property.GetValue(obj)?.ToString());
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
