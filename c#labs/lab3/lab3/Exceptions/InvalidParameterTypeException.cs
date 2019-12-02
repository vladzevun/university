using System;
using System.Collections.Generic;
using System.Text;

namespace lab3.Exceptions
{
    class InvalidParameterTypeException : InvalidCastException
    {
        public InvalidParameterTypeException(string message)
            : base(message)
        {

        }
    }
}
