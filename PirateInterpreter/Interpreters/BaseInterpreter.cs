using PirateInterpreter.Values;

namespace PirateInterpreter.Interpreters;

public abstract class BaseInterpreter
{
    protected readonly ILogger Logger;
    protected InterpreterFactory _interpreterFactory { get; private set; }
    
    public BaseInterpreter(ILogger logger, InterpreterFactory interpreterFactory)
    {
        Logger = logger;
        _interpreterFactory = interpreterFactory;
    }
    
    public abstract List<BaseValue> VisitNode();

    public BaseValue VisitSingleNode()
    {
        var node = VisitNode();
        if (node.Count > 1 && node.Count < 0) throw new Exception("Value is not a single value");
        return node[0];
    }
}