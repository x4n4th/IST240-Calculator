/*
 * Programming Excersises #1
 * Calculator Project 
 * IST 240
 * Author: Daniel Lafey
 * 
 * The idea of this program is to allow the user to enter multiple operations
 * This program will show the user step by step how the operations are being done.
 * 
 */ 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplicationCalcLafey
{
    class Program
    {
        static void Main(string[] args)
        { 
            String ans;

            Console.WriteLine("Wecome to a calcualtor program");
            do
            {
                Console.WriteLine("Please Enter an equation");
                Console.WriteLine("This calculator Accepts *, /, +, -, %, ^");
                Console.WriteLine("You can also enter Boolean operators <, > to find the biggest or smallest number");
                String input = Console.ReadLine(); //Asks user for equation

                //We only want the operators, spaces, and numbers
                while (!Regex.IsMatch(input, "^[0-9/*%+-<>^/ /]+$"))
                {
                    Console.WriteLine("Invalid Please Re-Enter");
                    input = Console.ReadLine();
                }

                /*
                 * ArrayList for order of Operation
                 * The calculator will handle boolean operators first
                 * followed by Multiplication and division and Modulus
                 * finally Addition and Subtraction
                 */
                ArrayList booleanOp = new ArrayList();
                ArrayList multDiv = new ArrayList();
                ArrayList addSub = new ArrayList();

                /*
                 * We will traverse the user input and add them 
                 * to out order of operation ArrayLists
                 */
                foreach (char a in input)
                {
                    if (Regex.IsMatch(a.ToString(), "^[*/%^]+$")) 
                    {
                        multDiv.Add(a);
                    }
                    else if (Regex.IsMatch(a.ToString(), "^[+-]+$"))
                    {
                        addSub.Add(a);
                    }
                    else if (Regex.IsMatch(a.ToString(), "^[><]+$"))
                    {
                        addSub.Add(a);
                    }
                }

                /*
                 * Possible imputs 1 + 2 / 3 * 4 - 2 % 7 > 5 < 3 + 4 ^ 2
                 */
                foreach (char operation in multDiv)
                {
                    String response = doNextOperation(operation, input);
                    input = response;
                    Console.WriteLine(response);
                }
                foreach (char operation in addSub)
                {
                    String response = doNextOperation(operation, input);
                    input = response;
                    Console.WriteLine(response);
                }

                Console.WriteLine("Would you like to continue? y or n");
                ans = Console.ReadLine();
            } while (ans.Contains('y'));
            
        }
        /*
         * Called to do the next operation within a string
         *  it will do the next left most operation within the string
         *  
         * @param operation can be +-/*%<>
         * @param input The equation
         * 
         * @return Will return the operation that was done plus the rest of the equation minus the operation
         *
         */
        public static String doNextOperation(char operation, String input)
        {
            Console.WriteLine("Doing next operation " + operation + " ON '" + input + "'");
            if (input == null)
            {
                return null;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Equals(operation))
                {
                    int leftCursor = 0;
                    int rightCursor = 0;

                    float left = 0;
                    //Get the left side of the operation
                    for (int r = i - 1; r >= 0; r--)
                    {
                        leftCursor = r;
                        if ((input[r].Equals('*') || input[r].Equals('/')) || (input[r].Equals('+') || input[r].Equals('-')
                            || input[r].Equals('>') || input[r].Equals('<') || input[r].Equals('%') || input[r].Equals('^')))
                        {
                            String temp = input.Substring(r + 1, i - (r + 1));
                            temp.Replace(" ", String.Empty);
                            left = float.Parse(temp);
                            break;
                        }
                        else if (r == 0)
                        {
                            String temp = input.Substring(0, i);
                            temp.Replace(" ", String.Empty);
                            left = float.Parse(temp);
                            break;
                        }
                    }

                    //Find where the left edge of the operation is 
                    leftCursor = leftCursor != 0 ? leftCursor-- : 0; 

                    //Get the right side of the operation
                    float right = 0;

                    for (int r = i + 1; r < input.Length; r++)
                    {
                        // We have to find the next operator or end of line
                        rightCursor = r;

                        if((input[r].Equals('*') || input[r].Equals('/')) || (input[r].Equals('+') || input[r].Equals('-')
                            || input[r].Equals('>') || input[r].Equals('<') || input[r].Equals('%') || input[r].Equals('^')))
                        {
                            //We found the next operator r - 1 is our end of integer
                            String temp = input.Substring(i + 1, r - i - 1);
                            temp.Replace(" ", String.Empty);
                            right = float.Parse(temp);
                            break;
                        }
                        else if (r == input.Length - 1)
                        {
                            String temp = input.Substring(i + 1, r - i);
                            temp.Replace(" ", String.Empty);
                            right = float.Parse(temp);
                            break;
                        }
                    }

                    //Do to the most right operand
                    rightCursor = rightCursor != (input.Length - 1) ? rightCursor-- : (input.Length - 1);

                    float finalOp = 0;

                    //Complete the operation
                    switch (input[i])
                    {
                        case '*':
                            finalOp = left * right;
                            Console.WriteLine(left + " * " + right + " = " + finalOp);
                            break;
                        case '/':
                            finalOp = ((float)left / right);
                            Console.WriteLine(left + " / " + right + " = " + finalOp);
                            break;
                        case '%':
                            finalOp = left % right;
                            Console.WriteLine(left + " % " + right + " = " + finalOp);
                            break;
                        case '+':
                            finalOp = left + right;
                            Console.WriteLine(left + " + " + right + " = " + finalOp);
                            break;
                        case '-':
                            finalOp = left - right;
                            Console.WriteLine(left + " - " + right + " = " + finalOp);
                            break;
                        case '<':
                            finalOp = (left < right) ? left : right;
                            Console.WriteLine(left + " < " + right + " = " + finalOp);
                            break;
                        case '>':
                            finalOp = (left > right) ? left : right;
                            Console.WriteLine(left + " > " + right + " = " + finalOp);
                            break;
                        case '^':
                            finalOp = (float)Math.Pow(left, right);
                            Console.WriteLine(left + " ^ " + right + " = " + finalOp);
                            break;
                    }

                    //Final operation into string and return it;

                    String beginOfString = leftCursor != 0 ? input.Substring(0, leftCursor + 1) : String.Empty;
                    String endOfString = rightCursor != input.Length - 1 ? input.Substring(rightCursor, ((input.Length) - rightCursor)) : String.Empty;

                    return beginOfString + finalOp + endOfString;
                }
            }
            return null;
        }
    }
}
