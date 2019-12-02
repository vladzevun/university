using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(new Loger());

            engine.run();
        }
    }

    
}
