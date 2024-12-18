﻿using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Imandra_Dev_Test;

class transactionJson
{
    [JsonProperty("35")]
    public string _35 { get; set; }
    [JsonProperty("150")]
    public string _150 { get; set; }
    [JsonProperty("32")]
    public string _32 { get; set; }
}

class Program
{

    static List<transactionJson> getTransactionListFromTextNoTags(List<string> transactions)
    {
        List<transactionJson> retVal = new List<transactionJson>();

        //Iterating the list of text transactions
        foreach (string trans in transactions)
        {
            transactionJson newTrans = new transactionJson();

            //Iterating the fields on each transaction
            foreach (string field in trans.Split("|"))
            {
                //evaluate field number
                switch (field.Split("=")[0])
                {
                    case "35":
                        newTrans._35 = field.Split("=")[1];
                        break;
                    case "150":
                        newTrans._150 = field.Split("=")[1];
                        break;
                    case "32":
                        newTrans._32 = field.Split("=")[1];
                        break;
                }
            }
            //Add transaction object to return list
            retVal.Add(newTrans);
        }
        return retVal;
    }

    //Gets the name of the tag from json
    static string getTagName(JObject json, string itemId)
    {
        return json[itemId]["name"].ToString();
    }

    //Gets the data type of the tag from json
    static string getTagType(JObject json, string itemId)
    {
        return json[itemId]["type"].ToString();
    }

    //Gets the enum text value of the tag from json
    static string getTagEnumValue(JObject json, string itemId, string valueId)
    {
        return json[itemId]["values"][valueId].ToString();
    }

    //Converts list of string transactions to object lists for JSON formatting
    static List<object> getTransactionListFromText(List<string> transactions, JObject jsonTags)
    {
        List<object> retVal = new List<object>();

        //Iterating the list of transactions
        foreach (string trans in transactions)
        {
            //Define a new transaction as a list of fields
            List<KeyValuePair<string, object>> lisNewTransaction = new List<KeyValuePair<string, object>>();

            //Iterating the fields on each transaction
            foreach (string field in trans.Split("|"))
            {
                //Getting field label
                string fieldNumber = field.Split("=")[0];
                //Getting field value
                string fieldValue = field.Split("=")[1];

                KeyValuePair<string, object> dictField = new KeyValuePair<string, object>();

                string finalName = getTagName(jsonTags, fieldNumber);
                //Evaluating field data type and additional enum values
                switch (getTagType(jsonTags, fieldNumber))
                {
                    case "enum":
                        fieldValue = getTagEnumValue(jsonTags, fieldNumber, fieldValue);
                        dictField = new KeyValuePair<string, object>(finalName, fieldValue);
                        break;
                    case "int":
                        dictField = new KeyValuePair<string, object>(finalName, int.Parse(fieldValue));
                        break;
                    case "decimal":
                        dictField = new KeyValuePair<string, object>(finalName, decimal.Parse(fieldValue));
                        break;
                    default:
                        dictField = new KeyValuePair<string, object>(finalName, fieldValue);
                        break;
                }
                //Adding field to transaction
                lisNewTransaction.Add(dictField);
            }
            //Converting KeyValuePair transaction fields to Dictionary for JSON ease of serialization
            var dictionary = lisNewTransaction.ToDictionary(x => x.Key, x => x.Value);
            //Add transaction to final list
            retVal.Add(dictionary);
        }
        return retVal;
    }

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

        //Splitting fix file into a list of text transactions
        //Using the header text and removing the first item because it is empty
        List<string> lisTransactions = (from x in line.Split(ProtocolHeader).Skip(1).ToList() select ProtocolHeader + x.Remove(x.Length - 1)).ToList();

        //Convert list of text transactions to list of objects for easy JSON serialization
        List<object> lisFinalTransactions = getTransactionListFromText(lisTransactions, jsonTags);

        //Serializing and displaying list of transactions
        retVal = System.Text.Json.JsonSerializer.Serialize(lisFinalTransactions);

        return retVal;
    }

    static int getPart2Question1(string[] args, string ProtocolHeader)
    {
        int retVal = 0;

        //Getting files from arguments
        string fixFileName = args[1];

        //Reading fix file
        StreamReader sr = new StreamReader(fixFileName);
        string line = sr.ReadLine();

        //Splitting fix file into a list of text transactions
        List<string> lisTransactions = (from x in line.Split(ProtocolHeader).Skip(1).ToList() select ProtocolHeader + x.Remove(x.Length - 1)).ToList();

        //Convert list of text transactions to list of objects for easy JSON serialization
        List<transactionJson> lisFinalTransactions = getTransactionListFromTextNoTags(lisTransactions);

        //Query to look for all transactions with value "D" in field "35"
        retVal = (from item in lisFinalTransactions where item._35 == "D" select item).Count();
        return retVal;
    }

    static int getPart2Question2(string[] args, string ProtocolHeader)
    {
        int retVal = 0;

        //Getting files from arguments
        string fixFileName = args[1];

        //Reading fix file
        StreamReader sr = new StreamReader(fixFileName);
        string line = sr.ReadLine();

        //Splitting fix file into a list of text transactions
        List<string> lisTransactions = (from x in line.Split(ProtocolHeader).Skip(1).ToList() select ProtocolHeader + x.Remove(x.Length - 1)).ToList();

        //Convert list of text transactions to list of objects for easy JSON serialization
        List<transactionJson> lisFinalTransactions = getTransactionListFromTextNoTags(lisTransactions);

        //Query to look for all transactions with value "0" in field "150"
        retVal = (from item in lisFinalTransactions where item._150 == "0" select item).Count();
        return retVal;
    }

    static int getPart2Question3(string[] args, string ProtocolHeader)
    {
        int retVal = 0;

        //Getting files from arguments
        string fixFileName = args[1];

        //Reading fix file
        StreamReader sr = new StreamReader(fixFileName);
        string line = sr.ReadLine();

        //Splitting fix file into a list of text transactions
        List<string> lisTransactions = (from x in line.Split(ProtocolHeader).Skip(1).ToList() select ProtocolHeader + x.Remove(x.Length - 1)).ToList();

        //Convert list of text transactions to list of objects for easy JSON serialization
        List<transactionJson> lisFinalTransactions = getTransactionListFromTextNoTags(lisTransactions);

        //Query to look for all transactions with value "F" in field "150" and summarizing the values from field "32"
        retVal = (from item in lisFinalTransactions where item._150 == "F" select int.Parse(item._32)).Sum();
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
                        //Completed
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
                        int answerP1Q1 = getPart2Question1(args, strProtocolHeader);
                        Console.WriteLine(answerP1Q1);
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
                        int answerP1Q2 = getPart2Question2(args, strProtocolHeader);
                        Console.WriteLine(answerP1Q2);
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
                        int answerP1Q3 = getPart2Question3(args, strProtocolHeader);
                        Console.WriteLine(answerP1Q3);
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

