using SSQL.Tokens;

namespace SSQL.Ast;

public class StringLiteral : IExpression
{
    public Token Token { get; }
    public string Value { get; }

    public StringLiteral(Token token, string value)
    {
        Token = token;
        Value = value;
    }
}