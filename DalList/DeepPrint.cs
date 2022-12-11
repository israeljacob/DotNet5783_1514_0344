using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public static class DeepPrintUtilities
{
    public static void DOPrintProperties<S>(this S print)
    {
        foreach (PropertyInfo propTo in print?.GetType().GetProperties()!)
        {
            if (propTo != null)
            {
                    Console.WriteLine($"{propTo.Name}: {propTo.GetValue(print, null)}");
            }
        }
        Console.WriteLine();
    }
}
