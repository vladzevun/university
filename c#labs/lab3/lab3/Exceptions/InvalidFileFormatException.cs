using System;
using System.Collections.Generic;
using System.Text;

namespace lab3.Exceptions
{
    class InvalidFileFormatException : FormatException
    {
        public InvalidFileFormatException(string message)
            :base(message)
        {

        }
    }
}
