using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    interface ILoger
    {
        public bool isTurnOn { get; set; }
        public void log(string message);
    }
}
