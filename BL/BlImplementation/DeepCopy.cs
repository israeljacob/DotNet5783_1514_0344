using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    public static class DeepCopyUtilities
    {
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to?.GetType().GetProperties()!)
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name)!;
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is DO.Category)
                    propTo.SetValue(to, (BO.Category)value);
                else if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }

        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type)!; // new object of Type
            from.CopyPropertiesTo(to);
            return to!;

        }

        public static void PrintProperties<S>(this S print)
        {
            foreach (PropertyInfo propTo in print?.GetType().GetProperties()!)
            {
                if (propTo != null)
                {
                    if (propTo.GetValue(print, null) is List<BO.OrderItem?>)
                        foreach (BO.OrderItem item in (List<BO.OrderItem>)propTo.GetValue(print,null)!)
                        item.PrintProperties();
                    else
                        Console.WriteLine($"{propTo.Name}: {propTo.GetValue(print, null)}");
                        
                }
            }
            Console.WriteLine(@"

");
        }
    }
}
