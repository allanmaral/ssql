using System.Collections;
using System.Collections.Generic;
using SSQL.Tokens;

namespace SSQL.Tests.Lexers.Data;

public class LexerSingleTokenData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {"(", Token.LeftParenthesis},
        new object[] {")", Token.RightParenthesis},
        new object[] {",", Token.Comma},
        new object[] {"$", new Token(TokenType.Illegal, "$")},
        new object[] {"+", new Token(TokenType.Illegal, "+")},
        new object[] {"-", new Token(TokenType.Illegal, "-")}
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}