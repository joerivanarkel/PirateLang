using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewParserTest.Node.Interfaces;
using NewPirateLexer.Tokens;

namespace NewParserTest.Node;

public class VariableAssignNode : INode
{
    public Token TypeToken { get; set; }
    public INode Identifier { get; set; }
    public INode Value { get; set; }

    public VariableAssignNode(Token typeToken, INode identifier, INode value)
    {
        TypeToken= typeToken;
        Identifier = identifier;
        Value = value;
    }

    public string Display()
    {
        return $"({Identifier.Display()} = {Value.Display()})";
    }
}