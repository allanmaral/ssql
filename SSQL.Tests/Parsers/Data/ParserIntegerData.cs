using System.Collections;
using System.Collections.Generic;
using SSQL.Ast;
using SSQL.Tokens;

namespace SSQL.Tests.Parsers.Data;

public class ParserIntegerData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {"0", new IntegerLiteral(new Token(TokenType.Int, "0"), 0)},
        new object[] {"1", new IntegerLiteral(new Token(TokenType.Int, "1"), 1)},
        new object[] {"9", new IntegerLiteral(new Token(TokenType.Int, "9"), 9)},
        new object[] {"10", new IntegerLiteral(new Token(TokenType.Int, "10"), 10)},
        new object[] {"-1", new IntegerLiteral(new Token(TokenType.Int, "-1"), -1)},
        new object[] {"-9", new IntegerLiteral(new Token(TokenType.Int, "-9"), -9)},
        new object[] {"-10", new IntegerLiteral(new Token(TokenType.Int, "-10"), -10)},
        new object[] {"+1", new IntegerLiteral(new Token(TokenType.Int, "+1"), 1)},
        new object[] {"+9", new IntegerLiteral(new Token(TokenType.Int, "+9"), 9)},
        new object[] {"+10", new IntegerLiteral(new Token(TokenType.Int, "+10"), 10)},
        new object[] {"9223372036854775807", new IntegerLiteral(new Token(TokenType.Int, "9223372036854775807"), 9223372036854775807)},
        new object[] {"-9223372036854775808", new IntegerLiteral(new Token(TokenType.Int, "-9223372036854775808"), -9223372036854775808)},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}