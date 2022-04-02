using System.Collections;
using System.Collections.Generic;
using SSQL.Tokens;

namespace SSQL.Tests.Lexers.Data;

public class LexerStringData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {"\"a\"", new Token(TokenType.String, "a")},
        new object[] {"\"bb\"", new Token(TokenType.String, "bb")},
        new object[] {"\"ccc\"", new Token(TokenType.String, "ccc")},
        new object[] {"\"a.b\"", new Token(TokenType.String, "a.b")},
        new object[] {"\"true\"", new Token(TokenType.String, "true")},
        new object[] {"\"false\"", new Token(TokenType.String, "false")},
        new object[] {"\"'sample'\"", new Token(TokenType.String, "'sample'")},
        new object[] {"\"eq()\"", new Token(TokenType.String, "eq()")},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}