using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
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

