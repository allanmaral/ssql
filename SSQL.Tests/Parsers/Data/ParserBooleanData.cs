using System.Collections;
using System.Collections.Generic;
using SSQL.Ast;
using SSQL.Tokens;

namespace SSQL.Tests.Parsers.Data;

public class ParserBooleanData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {"eq(true)", new BooleanLiteral(new Token(TokenType.True, "true"), true)},
        new object[] {"eq(false)", new BooleanLiteral(new Token(TokenType.False, "false"), false)},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}