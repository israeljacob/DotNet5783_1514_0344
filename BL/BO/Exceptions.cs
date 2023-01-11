using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BO;


/// Catch DO Exception
[Serializable]
public class CatchetDOException : Exception
{
    public CatchetDOException( Exception innerException)
        : base($"{innerException}\n")
    {
    }
}


///missing Items Exception
[Serializable]
public class missingItemsException : Exception
{
    public missingItemsException(int label, int detail)
        : base($"Product {label} In Stock  is less then {detail}\n")
    {
    }
}
///InCorrec tDetails Exception
[Serializable]
public class InCorrectDetailsException : Exception
{
    public InCorrectDetailsException(string label, object detail)
        : base($"{label} {detail} is incorrect\n")
    {
    }
}

///Missing Data Exception
[Serializable]
public class MissingDataException : Exception
{
    public MissingDataException(string? label)
       : base($"{label}is missing.\n")
    {
    }

    
}
///Item Exists InOrder Exception
[Serializable]
public class ItemExistsInOrderException : Exception
{
    public ItemExistsInOrderException(string? label)
       : base($"{label} exsists in a final order\n")
    {
    }
}
///Dates Exceptions
[Serializable]
public class DatesException : Exception
{
    public DatesException(String label)
       : base($" {label} is allready updated\n")
    {
    }
}

[Serializable]
public class DateException : Exception
{
    public DateException()
       : base("The chronological order of the dates makes no sense\n" )
    {
    }
}

[Serializable]
public class InCorrectIDException : Exception
{
    public InCorrectIDException(int ID)
        :base($"{ID} is smaller than 300,000")
    {
    }
}

