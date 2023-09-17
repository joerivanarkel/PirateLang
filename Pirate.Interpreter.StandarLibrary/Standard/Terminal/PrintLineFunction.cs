using Pirate.Common.Logger.Interfaces;
using Pirate.Interpreter.Values;
using Pirate.Interpreter.Values.Function;

namespace Pirate.Interpreter.StandardLibrary.Standard.Terminal;

public class PrintLineFunction : CSharpFunction
{
    public PrintLineFunction(ILogger logger) : base(null, logger) { }

    public override string Name => "Standard.Terminal.PrintLine";
    public override string Description => "Prints the given values to the terminal on a new line";
    public override string Parameters => "Multiple strings";

    public override List<BaseValue> Execute(List<object> arguments)
    {
        Logger.Info($"[{Name}] called with {arguments.Count} parameters");

        var result = "";
        foreach (var argument in arguments)
        {
            if (argument is BaseValue value)
            {
                Console.Write(value.Value?.ToString());
                result += value.Value?.ToString();
            }
            else
            {
                Console.Write(argument.ToString());
                result += argument.ToString();
            }
        }
        Console.WriteLine();
        return new List<BaseValue> { new StringValue(result, Logger) };
    }
}
