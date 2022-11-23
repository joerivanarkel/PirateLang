using PirateParser.Node.Interfaces;

namespace PirateParser.Node;

[Serializable]
public class VariableDeclarationNode : IVariableDeclarationNode
{
    public Token TypeToken { get; set; }
    public IValueNode Identifier { get; set; }
    public INode Value { get; set; }

    public VariableDeclarationNode(Token typeToken, IValueNode identifier, INode value)
    {
        TypeToken = typeToken;
        Identifier = identifier;
        Value = value;
    }

    public override string ToString()
    {
        return $"({Identifier.ToString()} = {Value.ToString()})";
    }

    public bool IsValid()
    {
        if (TypeToken.TokenType is not TokenType.INT and not TokenType.STRING and not TokenType.CHAR and not TokenType.FLOAT)
        {
            return false;
        }
        if (Identifier is not IValueNode)
        {
            return false;
        }
        if (Value is not INode)
        {
            return false;
        }
        return true;
    }
}