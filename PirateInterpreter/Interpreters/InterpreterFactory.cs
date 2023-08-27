using Pirate.Common.Interfaces;
using Pirate.Interpreter.Interpreters;
using Pirate.Interpreter.StandardLibrary.Interfaces;
using Pirate.Interpreter.Interpreters.Interfaces;
using Pirate.Parser.Node;
using Pirate.Parser.Node.Interfaces;

namespace Pirate.Interpreter.Interpreters;

public class InterpreterFactory : IInterpreterFactory
{
    private IStandardLibraryCallManager StandardLibraryFactory;
    private ILogger Logger;

    public InterpreterFactory(IStandardLibraryCallManager standardLibraryFactory, ILogger logger)
    {
        StandardLibraryFactory = standardLibraryFactory;
        Logger = logger;
    }

    public BaseInterpreter GetInterpreter(INode node)
    {
        switch (node)
        {
            case FunctionDeclarationNode:
                return new FunctionDeclarationInterpreter(node, this, Logger);
            case FunctionCallNode:
                return new FunctionCallInterpreter(node, this, Logger, StandardLibraryFactory);
            case IfStatementNode:
                return new IfStatementInterpreter(node, this, Logger);
            case WhileLoopStatementNode:
                return new WhileLoopStatementInterpreter(node, this, Logger);
            case ForLoopStatementNode:
                return new ForLoopStatementInterpreter(node, this, Logger);
            case VariableDeclarationNode:
                return new VariableDeclarationInterpreter(node, this, Logger);
            case VariableAssignmentNode:
                return new VariableAssignmentInterpreter(node, Logger, this);
            case BinaryOperationNode:
                return new BinaryOperationInterpreter(node, this, Logger);
            case ComparisonOperationNode:
                return new ComparisonOperationInterpreter(node, this, Logger);
            case ValueNode:
                return new ValueInterpreter(node, this, Logger);
            case CommentNode:
                return new CommentInterpreter(node, this, Logger);
        }
        throw new ArgumentNullException("node", $"Factory cannot find interpreter for {node.GetType().Name}");
    }
}
