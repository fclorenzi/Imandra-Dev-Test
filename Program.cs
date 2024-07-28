using System.Text;
using Newtonsoft.Json.Linq;

namespace Imandra_Dev_Test;

class Program
{

    static string getJsonMessages(string[] args, string ProtocolHeader)
    {
        string retVal = "";

        //Getting files from arguments
        string fixFileName = args[1];
        string tagFileName = args[2];

        //Reading and parsing json tags from files
        string jsonTagsString = File.ReadAllText(tagFileName);
        JObject jsonTags = JObject.Parse(jsonTagsString);

        //Reading fix file
        StreamReader sr = new StreamReader(fixFileName);
        string line = sr.ReadLine();

        retVal = line;

        return retVal;
    }

    static void Main(string[] args)
    {
        //Define protocol header as a variable for parsing easier - This could be a app setting or a parameter if needed
        string strProtocolHeader = "8=FIX.4.4";

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
                        var jsonMessages = getJsonMessages(args, strProtocolHeader);
                        Console.Write(jsonMessages);
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

