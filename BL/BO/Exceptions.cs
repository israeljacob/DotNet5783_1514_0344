using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BO;




//[Serializable]
//public class EmptyException : Exception
//{
//    public EmptyException(string? label, string id)
//       : base($"There is no {label} in the List.\n")
//    {
//    }

//    public EmptyException(string? label, int id, Exception? innerException)
//        : base($"There is no {label} in the List.\n", innerException)
//    {
//    }
//}

////public class EmptyException : Exception
//{
//    string label;
//    public string getLabel() { return label; }
//    public EmptyException(string label)
//    {
//        this.label = label;
//    }
//    public override string ToString()
//    {
//        return $"[Error] There is no {label} in the List.\n";
//    }

//}
//public class IdNotExistException : Exception
//{
//    int id;
//    string label;
//    public int getId() { return id; }
//    public string getLabel() { return label; }
//    public IdNotExistException(string label, int id)
//    {
//        this.id = id;
//        this.label = label;
//    }
//    public override string ToString()
//    {
//        return $"[Error] There is no {label} with id number {id}.\n";
//    }

//}
[Serializable]
public class EmptyException : Exception
{
    public EmptyException(string? label, int id)
       : base($"{label?? ""} with id number {id} not exist.\n")
    {
    }

    public EmptyException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} not exist.\n", innerException)
    {
    }
}
[Serializable]
public class IdNotExistException : Exception
{
    public IdNotExistException(string? label, int id)
       : base($"{label} with id number {id} not exist.\n")
    {
    }

    public IdNotExistException(string? label, int id, Exception? innerException)
        : base($" {label} with id number {id} not exist.\n", innerException)
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
        : base($"{label} with id number {id} not  exist.\n", innerException)
    {
    }
}

//public class DisplayException : Exception
//{
//    string ex;
//    public DisplayException(string ex)
//    {
//        this.ex = ex;
//    }
//    public override string ToString()
//    {
//        return $"{ex.ToString()}\n";
//    }
//}

//public class InCorrectIntException : Exception
//{

//    int id;
//    string label;
//    public InCorrectIntException(string label, int id)
//    {
//        this.id = id;
//        this.label = label;
//    }
//    public int getId() { return id; }
//    public string getLabel() { return label; }
//    public override string ToString()
//    {
//        return $"[Error] {label} :  {id} is incorrect input.\n";
//    }

//}


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
//public class InCorrectStringException : Exception
//{

//    string n;
//    string label;
//    public InCorrectStringException(string label, string n)
//    {
//        this.n = n;
//        this.label = label;
//    }
//    public string getId() { return n; }
//    public string getLabel() { return label; }
//    public override string ToString()
//    {
//        return $"[Error] {label} :  {n} is incorrect input.\n";
//    }

//}

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

//public class InCorrectDoubleException : Exception
//{

//    double n;
//    string label;
//    public InCorrectDoubleException(string label, double n)
//    {
//        this.n = n;
//        this.label = label;
//    }
//    public double getId() { return n; }
//    public string getLabel() { return label; }
//    public override string ToString()
//    {
//        return $"[Error] {label} :  {n} is incorrect input.\n";
//    }

//}
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

//public class InCorrectDetailsException : Exception
//{
//    public InCorrectDetailsException() : base() { }
//    public override string ToString() => "The details are incorrect.";
//}

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

//public class MissingDataException : Exception
//{
//    string label;
//    public string getLabel() { return label; }
//    public MissingDataException(string label)
//    {
//        this.label = label;
//    }
//    public override string ToString()
//    {
//        return $"[Error] There is no {label} in the List.\n";
//    }

//}

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

//public class ItemExistsInOrderException : Exception
//{
//    int id;
//    string label;
//    public int getId() { return id; }
//    public string getLabel() { return label; }
//    public ItemExistsInOrderException(string label, int id)
//    {
//        this.id = id;
//        this.label = label;
//    }
//    public override string ToString()
//    {
//        return $"[Error] There is no {label} with id number {id}.\n";
//    }

//}



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
//public class DatesException : Exception
//{
//    DateTime d1;
//    DateTime d2;
//    string label;
//    public DateTime getD1() { return d1; }
//    public DateTime getD2() { return d2; }
//    public string getLabel() { return label; }
//    public DatesException(string label, DateTime d1, DateTime d2)
//    {
//        this.d1 = d1;
//        this.d2 = d2;
//        this.label = label;
//    }
//    public override string ToString()
//    {
//        return $"[Error] {label} Can not compare between {d1} to {d2}.\n";
//    }

//}


