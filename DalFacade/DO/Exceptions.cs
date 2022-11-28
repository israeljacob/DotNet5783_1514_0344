using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{


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

    public class InvalidAlreadyExist : Exception
    {
        public InvalidAlreadyExist() { }
        public InvalidAlreadyExist(string str, int id)
            : base(String.Format("Error ! {0} you search {1} not exist...", str, id)) { }
    }

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

    public class IdNotExistException : Exception
    {
        int id;
        string label;
        public int getId() { return id; }
        public string getLabel() { return label; }
        public IdNotExistException(string label, int id)
        {
            this.id = id;
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] There is no {label} with id number {id}.\n";
        }

    }

    public class DatesException : Exception
    {
        DateTime d1;
        DateTime d2;
        string label;
        public DateTime getD1() { return d1; }
        public DateTime getD2() { return d2; }
        public string getLabel() { return label; }
        public DatesException(string label, DateTime d1,DateTime d2)
        {
            this.d1 = d1;
            this.d2 = d2;
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] {label} Can not compare between {d1} to {d2}.\n";
        }

    }



    public class ExistException : Exception
    {
        private string _message;

        public ExistException(string message): base(message) { _message = message; }
        public override string ToString() => _message + " ID allresdy exists";
    }

    public class DoesNotExistsException : Exception
    {
        private string _message;

        public DoesNotExistsException(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " does not exists";
    }
}

