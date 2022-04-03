using SSQL.Tokens;

namespace SSQL.Ast;

public class Identifier : IExpression
{
    public Token Token { get; }
    public string Value { get; }

    public Identifier(Token token, string value)
    {
        Token = token;
        Value = value;
    }
    
    // public string TokenLiteral()
    // {
    //     return Token.Literal;
    // }
    //
    // public INode StatementNode()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public INode ExpressionNode()
    // {
    //     throw new NotImplementedException();
    // }
}