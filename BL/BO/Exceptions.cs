using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BO;
[Serializable]
public class EmptyException : Exception
{
    public EmptyException(string? label, int id)
       : base($"{label?? ""} with id number {id} does not exist.\n")
    {
    }

    public EmptyException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} does not exist.\n", innerException)
    {
    }
}
[Serializable]
public class IdNotExistException : Exception
{
    public IdNotExistException(string? label, int id)
       : base($"{label} with id number {id} does not exist.\n")
    {
    }

    public IdNotExistException(string? label, int id, Exception? innerException)
        : base($" {label} with id number {id} does not exist.\n", innerException)
    {
    }
}



[Serializable]
public class InCorrectIntException : Exception
{
    public InCorrectIntException(string? label, int id)
       : base($"{label} with id number {id} is incorrect input.\n")
    {
    }

    public InCorrectIntException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} is incorrect input.\n", innerException)
    {
    }
}

[Serializable]
public class IdAlreadyExistException : Exception
{
    public IdAlreadyExistException(string? label, int id)
       : base($"{label} with id number {id} alraedy exist.\n")
    {
    }

    public IdAlreadyExistException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} does not  exist.\n", innerException)
    {
    }
}

[Serializable]
public class InCorrectStringException : Exception
{
    public InCorrectStringException(string? label, string id)
       : base($"{label} with id number {id} is incorrect input.\n")
    {
    }

    public InCorrectStringException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} is incorrect input.\n", innerException)
    {
    }
}

[Serializable]
public class InCorrectDoubleException : Exception
{
    public InCorrectDoubleException(string? label, double id)
       : base($"{label} with id number {id} is incorrect input.\n")
    {
    }

    public InCorrectDoubleException(string? label, double id, Exception? innerException)
        : base($"{label} with id number {id} is incorrect input.\n", innerException)
    {
    }
}

[Serializable]
public class InCorrectDetailsException : Exception
{
    public InCorrectDetailsException(string? label, int id)
       : base($" There is no {label} in the List.\n")
    {
    }

    public InCorrectDetailsException(string? label, int id, Exception? innerException)
        : base($" There is no {label} in the List.\n", innerException)
    {
    }
}


[Serializable]
public class MissingDataException : Exception
{
    public MissingDataException(string? label, string id)
       : base($" There is no {label} in the List.\n")
    {
    }

    public MissingDataException(string? label, int id, Exception? innerException)
        : base($"There is no {label} in the List.\n", innerException)
    {
    }
}

[Serializable]
public class ItemExistsInOrderException : Exception
{
    public ItemExistsInOrderException(string? label)
       : base($" There is no {label} with id number .\n")
    {
    }

    public ItemExistsInOrderException(string? label, int id, Exception? innerException)
        : base($"There is no {label} with id number {id}.\n", innerException)
    {
    }
}
[Serializable]
public class DatesException : Exception
{
    public DatesException(String label,DateTime? d1, DateTime d2)
       : base($" {label} Can not compare between {d1} to {d2}.\n")
    {
    }

    public DatesException(String label, DateTime? d1, DateTime d2, Exception? innerException)
        : base($" {label} Can not compare between {d1} to {d2}.\n", innerException)
    {
    }
}


