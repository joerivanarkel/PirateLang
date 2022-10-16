﻿// See https://aka.ms/new-console-template for more information
using Interpreter;
using Parser;
using Lexer;
using Common;

Console.WriteLine("Hello, World!");
var Logger = new Logger("Test");

while (true)
{
    var input = Console.ReadLine();
    var lexer = new Lexer.Lexer("test", input, Logger);
    var result = lexer.MakeTokens();

    if (result.tokens == null)
    {
        Console.WriteLine(result.error.ToString());
    }

    ObjectSerializer objectSerializer = new(".", Logger);

    var parser = new Parser.Parser(result.tokens, Logger, objectSerializer, "Test");
    var parseResult = parser.StartParse();

    if (parseResult.Nodes == null)
    {
        Console.WriteLine("stuk");
    }

    var interpreter = new Interpreter.Interpreter("Test", objectSerializer, Logger);
    var Result = interpreter.StartInterpreter();

    if (Result == null)
    {
        Console.WriteLine("Why is this null?");
    }
    foreach (var item in Result)
    {
        Console.WriteLine(item.Value);
    }
    


}
