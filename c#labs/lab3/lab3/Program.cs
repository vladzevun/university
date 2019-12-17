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

            //Console.Write("Enter file name: ");
            //input = Console.ReadLine();

            try
            {
                input = File.ReadAllText("LostValues.ini");
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
                return;
            }
            

            string sectionName;
            string memberName;
            
            while (true)
            {
                Console.Write("Enter section name: ");
                sectionName = Console.ReadLine();
                Console.Write("Enter member name: ");
                memberName = Console.ReadLine();
                Console.Write("Choose value type you want to get (1-double,2-integer and 3-string): ");
                char option = (char)Console.Read();


                try
                {
                    switch (option)
                    {
                        case '1':
                            Console.WriteLine($"{memberName} = {lParser.getDblValue(sectionName, memberName)}");
                            break;
                        case '2':
                            Console.WriteLine($"{memberName} = {lParser.getIntValue(sectionName, memberName)}");
                            break;
                        case '3':
                            Console.WriteLine($"{memberName} = {lParser.getStrValue(sectionName, memberName)}");
                            break;
                        default:
                            Console.WriteLine("Wrong option. Try again. Use 1 for double,2 for integer and 3 for string");
                            break;
                    }
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
                catch (Exception e)
                {
                    Console.WriteLine($"An exception occurred, message: {e.Message}");
                }
                Console.ReadLine();
            }

            

            
        }

        
    }
}
