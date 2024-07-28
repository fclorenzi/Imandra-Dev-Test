using System.Text;

namespace Imandra_Dev_Test;

class Program
{
    static void Main(string[] args)
    {

        //Help display text
        StringBuilder strHelp = new StringBuilder();
        strHelp.AppendLine("Please enter valid argumenst.");
        strHelp.AppendLine("-For list of stransactions use 'print-messages fix-file-with-path tags-file-with-path'");
        strHelp.AppendLine("-For Part 2 Question 1 use 'part2-q1 fix-file-with-path'");
        strHelp.AppendLine("-For Part 2 Question 1 use 'part2-q2 fix-file-with-path'");
        strHelp.AppendLine("-For Part 2 Question 1 use 'part2-q3 fix-file-with-path'");
        strHelp.AppendLine("-For Part 3 Question 1 use 'part3-q1 fix-file-with-path'");

        if (args.Length == 0)
        {
            //If no arguments given return help
            Console.Write(strHelp);
        }
        else
        {
            switch (args[0])
            {
                case "print-messages":
                    if (args.Length > 2)
                    {
                        Console.Write("print-messages");
                    }
                    else
                    {
                        //if not enough arguments given return help
                        Console.Write(strHelp);
                    }
                    break;
                case "part2-q1":
                    if (args.Length > 1)
                    {
                        Console.WriteLine("part2-q1");
                    }
                    else
                    {
                        //if not enough arguments given return help
                        Console.Write(strHelp);
                    }
                    break;
                case "part2-q2":
                    if (args.Length > 1)
                    {
                        Console.WriteLine("part2-q2");
                    }
                    else
                    {
                        //if not enough arguments given return help
                        Console.Write(strHelp);
                    }
                    break;
                case "part2-q3":
                    if (args.Length > 1)
                    {
                        Console.WriteLine("part2-q3");
                    }
                    else
                    {
                        //if not enough arguments given return help
                        Console.Write(strHelp);
                    }
                    break;
                case "part3-q1":
                    if (args.Length > 1)
                    {
                        Console.WriteLine("part3-q1");
                    }
                    else
                    {
                        //if not enough arguments given return help
                        Console.Write(strHelp);
                    }
                    break;
                default:
                    //if no valid action given return help
                    Console.Write(strHelp);
                    break;
            }
        }



    }
}

