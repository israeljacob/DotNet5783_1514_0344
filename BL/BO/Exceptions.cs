using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{


    public class Empty : Exception
    {
        string label;
        public string getLabel() { return label; }
        public Empty(string label)
        {
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] There is no {label} in the List.\n";
        }

    }
    public class IdNotExist : Exception
    {
        int id;
        string label;
        public int getId() { return id; }
        public string getLabel() { return label; }
        public IdNotExist(string label, int id)
        {
            this.id = id;
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] There is no {label} with id number {id}.\n";
        }

    }
    public class IdAlreadyExist : Exception
    {

        int id;
        string label;
        public IdAlreadyExist(string label, int id)
        {
            this.id = id;
            this.label = label;
        }
        public int getId() { return id; }
        public string getLabel() { return label; }
        public override string ToString()
        {
            return $"[Error] {label} with id number {id} already exist.\n";
        }

    }






    public class DisplayException : Exception
    {
        string ex;
        public DisplayException(string ex)
        {
            this.ex = ex;
        }
        public override string ToString()
        {
            return $"{ex.ToString()}\n";
        }
    }


    //public class DoesNotExistsException : Exception
    //{




    //    private string _message;

    //    public DoesNotExistsException(string message) : base(message) { _message = message; }
    //    public override string ToString() => _message + " does not exists";
    //}
    //public class ExistException : Exception
    //{
    //    private string _message;

    //    public ExistException(string message) : base(message) { _message = message; }
    //    public override string ToString() => _message + " ID allresdy exists";
    //}



    public class InCorrectIntException : Exception
    {

        int id;
        string label;
        public InCorrectIntException(string label, int id)
        {
            this.id = id;
            this.label = label;
        }
        public int getId() { return id; }
        public string getLabel() { return label; }
        public override string ToString()
        {
            return $"[Error] {label} :  {id} is incorrect input.\n";
        }

    }

    public class InCorrectStringException : Exception
    {

        string n;
        string label;
        public InCorrectStringException(string label, String n)
        {
            this.n = n;
            this.label = label;
        }
        public string getId() { return n; }
        public string getLabel() { return label; }
        public override string ToString()
        {
            return $"[Error] {label} :  {n} is incorrect input.\n";
        }

    }

    public class InCorrectDoubleException : Exception
    {

        double n;
        string label;
        public InCorrectDoubleException(string label, double n)
        {
            this.n = n;
            this.label = label;
        }
        public double getId() { return n; }
        public string getLabel() { return label; }
        public override string ToString()
        {
            return $"[Error] {label} :  {n} is incorrect input.\n";
        }

    }


    public class InCorrectDetailsException : Exception
    {
        public InCorrectDetailsException() : base() { }
        public override string ToString() => "The details are incorrect.";
    }


    public class MissingDataException : Exception
    {
        string label;
        public string getLabel() { return label; }
        public MissingDataException(string label)
        {
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] There is no {label} in the List.\n";
        }

    }

    //public class MissingAttributeException : Exception
    //{
    //    private string _message;
    //    public MissingAttributeException(string message) : base(message) { _message = message; }
    //    public override string ToString() => _message + " is missing";
    //}
    //public class DetailsAreInCorrctException : Exception
    //{
    //    private string _message;
    //    public DetailsAreInCorrctException(string message) : base(message) { _message = message; }
    //    public override string ToString() => _message + " is incorrect";
    //}


    public class ItemExistsInOrderException : Exception
    {
        int id;
        string label;
        public int getId() { return id; }
        public string getLabel() { return label; }
        public ItemExistsInOrderException(string label, int id)
        {
            this.id = id;
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] There is no {label} with id number {id}.\n";
        }

    }
    public class ItemExistsInOrder : Exception
    {
        private string _message;
        public ItemExistsInOrder(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " exists in an order";
    }

}
