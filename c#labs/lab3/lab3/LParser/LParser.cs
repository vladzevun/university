using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab3.LParser
{
    class LParser 
    {
        public LParser()
        {
            this.db = new List<Tuple<Dictionary<string, string>, string>>();

            sectionCounter = -1;
        }

        

        public List<Tuple<Dictionary<string, string>, string>> db { get; private set; }

        private int sectionCounter;

        public long getIntValue(string sectionName, string memberName)
        {
            Dictionary<string, string> dict = null;
            foreach (var item in this.db)
            {
                if (sectionName == item.Item2)
                {
                    dict = item.Item1;
                }
            }
            if (dict == null)
                throw new Exceptions.InvalidSectionException($"There is no section with name {sectionName}");
            string str;
            if (!dict.TryGetValue(memberName, out str))
            {
                throw new Exceptions.InvalidNameException($"There is no member with name {memberName}");
            }
            long result;
            if (!long.TryParse(str, out result))
            {
                throw new Exceptions.InvalidParameterTypeException($"Can not cast member with name {str} to int. number");
            }
            return result;
        }
        public double getDblValue(string sectionName, string memberName)
        {
            Dictionary<string, string> dict = null;
            foreach (var item in this.db)
            {
                if (sectionName == item.Item2)
                {
                    dict = item.Item1;
                    break;
                }
            }
            if (dict == null)
                throw new Exceptions.InvalidSectionException($"There is no section with name {sectionName}");
            string str;
            if (!dict.TryGetValue(memberName, out str))
            {
                throw new Exceptions.InvalidNameException($"There is no member with name {memberName}");
            }
            double result;
            if (!Double.TryParse(str, out result))
            {
                throw new Exceptions.InvalidParameterTypeException($"Can not cast member with name {str} to float number");
            }
            return result;
        }
        public string getStrValue(string sectionName, string memberName)
        {
            Dictionary<string, string> dict = null;
            foreach (var item in this.db)
            {
                if (sectionName == item.Item2)
                {
                    dict = item.Item1;
                }
            }
            if (dict == null)
                throw new Exceptions.InvalidSectionException($"There is no section with name {sectionName}");
            string str;
            if (!dict.TryGetValue(memberName, out str))
            {
                throw new Exceptions.InvalidNameException($"There is no member with name {memberName}");
            }
            return str;
        }

        public void parse(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);

            MemoryStream ms = new MemoryStream(bytes);
            StreamReader sr = new StreamReader(ms);

            StringBuilder sb = new StringBuilder();
            
            while (!sr.EndOfStream)
            {
                int c = sr.Read();

                //MarkUp chars are usless - skip them
                if (c < 32)
                    continue;

                //c == ; - comment
                if (sr.Peek() == 59 || c == 59)
                {
                    //Skip comment
                    sr.ReadLine();
                    continue;
                }

                //c == [ - SECTION name
                if (c == 91)
                {
                    //new SECTION
                    

                    // sb = SECTIONNAME]
                    sb.Append(sr.ReadLine());

                    //if true - wrong format
                    if ((int)sb[sb.Length-1] != 93)
                        throw new Exceptions.InvalidFileFormatException("Wrong format. Section name is not closed by ] bracket.");

                    //remove ] bracket
                    sb.Remove(sb.Length - 1, 1);

                    this.sectionCounter++;

                    this.db.Add(new Tuple<Dictionary<string, string>, string>(new Dictionary<string, string>(), sb.ToString()));

                    //clear sb for next SECTION name
                    sb.Clear();
                    
                    continue;
                }

                //c == other chars
                if (c != 32)
                {
                    //NAME
                    string leftPart = LParser.ReadNameMember(ref sr, c);

                    //_=_ - skip 3 chars
                    c = sr.Read(); //" "
                    if (!((int)c == 32))
                        throw new Exceptions.InvalidFileFormatException($"Wrong format. After Name member must be space(32) char, but not '{(char)c}'");
                    c = sr.Read(); //"="
                    if (!((int)c == 61))
                        throw new Exceptions.InvalidFileFormatException($"Wrong format. After Name member and space(32) char must be '=' char, but not '{(char)c}'");
                    c = sr.Read(); //" "
                    if (!((int)c == 32))
                        throw new Exceptions.InvalidFileFormatException($"Wrong format. After '=' char must be space(32) char, but not '{(char)c}'");
                    
                    //VALUE
                    c = sr.Read();
                    string rightPart = LParser.ReadValueMember(ref sr, c);

                    //Adding pair <key=NAME, value=VALUE> to dictionary db
                    this.db[sectionCounter].Item1.Add(leftPart, rightPart);
                }
            }
        }
        private static string ReadNameMember(ref StreamReader sr, int current)
        {
            StringBuilder sb = new StringBuilder();
            int c = current;
            if (!((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || (c == 95)))
                throw new Exceptions.InvalidFileFormatException($"Wrong format. Name member can not start|consist with '{(char)c}'");
            sb.Append((char)c);
            while (!sr.EndOfStream && (sr.Peek() != 32))
            {
                if (sr.Peek() == 12 || sr.Peek() == 13)
                    break;
                c = sr.Read();
                if (!((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || (c == 95)))
                    throw new Exceptions.InvalidFileFormatException($"Wrong format. Name member can not start|consist with '{(char)c}'");
                sb.Append((char)c);
            }
            return sb.ToString();
        }
        private static string ReadValueMember(ref StreamReader sr, int current)
        {
            StringBuilder sb = new StringBuilder();
            int c = current;
            if (!((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || (c == 95) || (c == 47) || (c == 46)))
                throw new Exceptions.InvalidFileFormatException($"Wrong format. Name member can not start with '{(char)c}'");
            sb.Append((char)c);
            while (!sr.EndOfStream && (sr.Peek() != 32))
            {
                if (sr.Peek() == 12 || sr.Peek() == 13)
                    break;
                c = sr.Read();
                if (!((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || (c == 95) || (c == 47) || (c == 46)))
                    throw new Exceptions.InvalidFileFormatException($"Wrong format. Name member can not start with '{(char)c}'");
                sb.Append((char)c);
            }

            ////check for VALUE format (str, int or float)
            //bool hasChars = false; // = string(path)
            //bool hasNums  = false; // = int or float
            //bool hasDot   = false; // = float
            //bool hasMoreThanOneDot = false; //
            //for (int i = 0; i < sb.Length; i++)
            //{
            //    int tempChar = (int)sb[i];
            //    if (tempChar == 46) // char '.'
            //    {
            //        if (hasDot)
            //        {
            //            hasMoreThanOneDot = true;
            //        }
            //        hasDot = true;
            //        continue;
            //    }
            //    if ((tempChar > 64 && tempChar < 91) || (tempChar > 96 && tempChar < 123) || (tempChar == 95) || (tempChar == 47))
            //    {
            //        hasChars = true;
            //        continue;
            //    }
            //    if ((tempChar > 47 && tempChar < 58))
            //    {
            //        hasNums = true;
            //    }
            //}

            //if (!hasChars)
            //{
            //    if (hasMoreThanOneDot && hasNums)
            //        throw new Exceptions.InvalidValueException("Wrong value member. ");
            //}

            return sb.ToString();
        }
        public static bool isCorrectWord(string word)
        {
            int c = 0;
            for (int i = 0; i < word.Length; i++)
            {
                c = (int)word[i];
                if ((c > 64 && c < 91) || (c > 96 && c < 123) || (c > 47 && c < 58) || (c == 95))
                    continue;
                return false;
            }
            return true;
        }
    }
}
