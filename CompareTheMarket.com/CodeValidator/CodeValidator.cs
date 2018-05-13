using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeValidator
{
    /// <summary>
    /// Write a function that will be passed some javascript code and it will return TRUE if the code validates correctly.
    /// There are 3 types of brackets in javascript: { [ ( . Your function must check if the code is written correctly, meaning
    /// if all opened brackets have been closed correctly. 
    /// { ( [ code ] ) } should return TRUE
    /// { ( [ code ] )  should return FALSE
    /// { [ ( code ] ) } should return FALSE
    /// { code ) should return FALSE
    /// ( code { code } [ code ] {{code}} [[[code]]] ) should return TRUE
    /// </summary>
    public class CodeValidator
    {
        public bool Validate(string input)
        {
            Stack<char> s = new Stack<char>();

            foreach (char c in input)
            {
                if (c == '{')
                {
                    s.Push(c);

                }
                else if (c == '[')
                {
                    s.Push(c);
                }
                else if (c == '(')
                {
                    s.Push(c);
                }
                else if (c == '}')
                {
                    char lastOpenBracket = s.Pop();
                    if (lastOpenBracket != '{') return false;
                }
                else if (c == ']')
                {
                    char lastOpenBracket = s.Pop();
                    if (lastOpenBracket != '[') return false;
                }
                else if (c == ')')
                {
                    char lastOpenBracket = s.Pop();
                    if (lastOpenBracket != '(') return false;
                }
            }

            if (s.Count > 0) return false;

            return true;
        }
    }
}
