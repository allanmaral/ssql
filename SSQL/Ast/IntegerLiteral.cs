using SSQL.Tokens;

namespace SSQL.Ast;

public class IntegerLiteral : IExpression
{
    public Token Token { get; }
    public long Value { get; }

    public IntegerLiteral(Token token, long value)
    {
        Token = token;
        Value = value;
    }
}