using System.Collections.Generic;

namespace PirateParser.Test;

public class ParserTest
{
    [Fact]
    public void ShouldReturnBinaryOperationNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.OPERATORS, TokenType.PLUS, "+"));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<BinaryOperationNode>(result.node);
    }

    [Fact]
    public void ShouldReturnComparisonOperationNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.COMPARISONOPERATORS, TokenType.DOUBLEEQUALS, "=="));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<ComparisonOperationNode>(result.node);
    }

    [Fact]
    public void ShouldReturnValueNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<ValueNode>(result.node);
    }

    [Fact]
    public void ShouldReturnVariableAssignNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.TYPEKEYWORD, TokenType.VAR));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.IDENTIFIER, "a"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.EQUALS));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<VariableDeclarationNode>(result.node);
    }

    [Fact]
    public void ShouldReturnIfStatementNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.CONTROLKEYWORD, TokenType.IF));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.COMPARISONOPERATORS, TokenType.DOUBLEEQUALS, "=="));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTCURLYBRACE));
        
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<IfStatementNode>(result.node);
    }

    [Fact]
    public void ShouldReturnIfStatementNodeWithElseNodes()
    {
                // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.CONTROLKEYWORD, TokenType.IF));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.COMPARISONOPERATORS, TokenType.DOUBLEEQUALS, "=="));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.CONTROLKEYWORD, TokenType.ELSE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTCURLYBRACE));
        
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<IfStatementNode>(result.node);
        Assert.NotNull(((IfStatementNode)result.node).ElseNode);
    }

    [Fact]
    public void ShouldReturnWhileLoopStatementNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();
        
        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.CONTROLKEYWORD, TokenType.WHILE));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.COMPARISONOPERATORS, TokenType.DOUBLEEQUALS, "=="));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTCURLYBRACE));
        
        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<WhileLoopStatementNode>(result.node);
    }

    [Fact]
    public void ShouldReturnForLoopStatementNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();

        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.CONTROLKEYWORD, TokenType.FOR));
        tokens.Add(new Token(TokenGroup.TYPEKEYWORD, TokenType.VAR));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.IDENTIFIER, "a"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.EQUALS));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "1"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.TO));
        tokens.Add(new Token(TokenGroup.VALUE, TokenType.INT, "10"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTCURLYBRACE));

        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<ForLoopStatementNode>(result.node);
    }

    [Fact]
    public void ShouldReturnFunctionDeclarationNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();

        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.TYPEKEYWORD, TokenType.FUNC));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.IDENTIFIER, "a"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTPARENTHESES));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTPARENTHESES));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.COLON));
        tokens.Add(new Token(TokenGroup.TYPEKEYWORD, TokenType.VOID));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTCURLYBRACE));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTCURLYBRACE));

        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<FunctionDeclarationNode>(result.node);
    }

    [Fact]
    public void ShouldReturnFunctionCallNode()
    {
        // Arrange
        var logger = A.Fake<ILogger>();

        var tokens = new List<Token>();
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.IDENTIFIER, "a"));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.LEFTPARENTHESES));
        tokens.Add(new Token(TokenGroup.SYNTAX, TokenType.RIGHTPARENTHESES));

        var parserFactory = new ParserFactory();
        var parser = parserFactory.GetParser(0, tokens, logger);

        // Act
        var result = parser.CreateNode();

        // Assert
        Assert.IsType<FunctionCallNode>(result.node);
    }
}