using SSQL.Tokens;

namespace SSQL.Ast;

public class BooleanLiteral : IExpression
{
    public Token Token { get; }
    public bool Value { get; }

    public BooleanLiteral(Token token, bool value)
    {
        Token = token;
        Value = value;
    }
}