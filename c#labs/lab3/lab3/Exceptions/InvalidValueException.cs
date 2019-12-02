using System;
using System.Collections.Generic;
using System.Text;

namespace lab3.Exceptions
{
    class InvalidValueException : ArgumentException
    {
        public InvalidValueException(string message)
            :base(message)
        {

        }
    }
}
