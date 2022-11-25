using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

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


    public class DoesNotExistsException : Exception
    {




        private string _message;

        public DoesNotExistsException(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " does not exists";
    }
    public class ExistException : Exception
    {
        private string _message;

        public ExistException(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " ID allresdy exists";
    }
    public class InCorrectDetailsException : Exception
    {
        public InCorrectDetailsException() : base() { }
        public override string ToString() => "The details are incorrect.";
    }

    public class MissingAttributeException : Exception
    {
        private string _message;
        public MissingAttributeException(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " is missing";
    }
    public class DetailsAreInCorrctException : Exception
    {
        private string _message;
        public DetailsAreInCorrctException(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " is incorrect";
    }
    public class ItemExistsInOrder : Exception
    {
        private string _message;
        public ItemExistsInOrder(string message) : base(message) { _message = message; }
        public override string ToString() => _message + " exists in an order";
    }

}
