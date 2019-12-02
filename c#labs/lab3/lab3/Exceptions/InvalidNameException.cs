using System;
using System.Collections.Generic;
using System.Text;

namespace lab3.Exceptions
{
    class InvalidNameException : ArgumentException
    {
        public InvalidNameException(string message)
            : base(message)
        {

        }
    }
}
