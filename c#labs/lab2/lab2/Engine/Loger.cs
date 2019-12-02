using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    class Loger : ILoger
    {
        public Loger()
        {
            this.isTurnOn = true;
        }
        public bool isTurnOn { get; set; }
        public void log(string message)
        {
            if (this.isTurnOn)
            {
                Console.WriteLine("\nDEBUG:\t"+message);
            }
        }
    }
}
