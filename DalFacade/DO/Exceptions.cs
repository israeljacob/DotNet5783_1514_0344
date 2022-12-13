using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DO;

public class IdAlreadyExistException : Exception
{

    int id;
    string label;
    public IdAlreadyExistException(string label, int id)
    {
        this.id = id;
        this.label = label;
    }
    public override string ToString()
    {
        return $" {label} with id number {id} already exist.\n";
    }

}

public class EmptyException : Exception
{
    string label;
    public string getLabel() { return label; }
    public EmptyException(string label)
    {
        this.label = label;
    }
    public override string ToString()
    {
        return $"There is no {label}s in the List.\n";
    }

}

public class DoesNotExistException : Exception
{
    int? id;
    string label;
    public DoesNotExistException(string label, int? id = null)
    {
        this.id = id ;
        this.label = label;
    }
    public override string ToString()
    {
        if (id == null)
            return $"There is no {label} that fulfills the condition\n";
        return $" There is no {label} with id number {id}.\n";
    }

}






