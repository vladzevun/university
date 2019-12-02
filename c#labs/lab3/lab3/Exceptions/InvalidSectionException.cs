using System;
using System.Collections.Generic;
using System.Text;

namespace lab3.Exceptions
{
    class InvalidSectionException : ArgumentException
    {
        public InvalidSectionException(string message)
            : base(message)
        {

        }
    }
}
