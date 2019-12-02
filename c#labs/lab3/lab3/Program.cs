using System;
using lab3.LParser;
using System.IO;
using System.Text;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";

            try
            {
                input = File.ReadAllText("input.txt");
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine($"An exception occurred, message: {e.Message}");
                return;
            }           

            LParser.LParser lParser = new LParser.LParser();

            try
            {
                lParser.parse(input);
            }
            catch (Exceptions.InvalidFileFormatException e)
            {
                Console.WriteLine($"An exception occurred, message: {e.Message}");
            }
            

            string sectionName;
            string memberName;
            while (true)
            {
                Console.Write("Enter section name: ");
                sectionName = Console.ReadLine();
                Console.Write("Enter member name: ");
                memberName = Console.ReadLine();
                try
                {
                    Console.WriteLine($"output = {lParser.getDblValue(sectionName, memberName)}"); 
                }
                catch (Exceptions.InvalidSectionException e)
                {
                    Console.WriteLine($"An exception occurred, message: {e.Message}");
                }
                catch (Exceptions.InvalidNameException e)
                {
                    Console.WriteLine($"An exception occurred, message: {e.Message}");
                }
                catch (Exceptions.InvalidParameterTypeException e)
                {
                    Console.WriteLine($"An exception occurred, message: {e.Message}");
                }

            }

            

            Console.ReadKey();
        }

        
    }
}
