using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BO;


[Serializable]
public class CatchetDOException : Exception
{
    public CatchetDOException( Exception innerException)
        : base($"{innerException}\n")
    {
    }
}



[Serializable]
public class missingItemsException : Exception
{
    public missingItemsException(int label, int detail)
        : base($"Product {label} In Stock  is less then {detail}\n")
    {
    }
}
[Serializable]
public class InCorrectDetailsException : Exception
{
    public InCorrectDetailsException(string label, object detail)
        : base($"{label} {detail} is incorrect\n")
    {
    }
}


[Serializable]
public class MissingDataException : Exception
{
    public MissingDataException(string? label)
       : base($"{label}is missing.\n")
    {
    }

    
}

[Serializable]
public class ItemExistsInOrderException : Exception
{
    public ItemExistsInOrderException(string? label)
       : base($"{label} exsists in a final order\n")
    {
    }
}
[Serializable]
public class DatesException : Exception
{
    public DatesException(String label)
       : base($" {label} is allready updated\n")
    {
    }
}


