using SSQL.Tokens;

namespace SSQL.Ast;

public class FloatLiteral : IExpression
{
    public Token Token { get; }
    public double Value { get; }

    public FloatLiteral(Token token, double value)
    {
        Token = token;
        Value = value;
    }
}