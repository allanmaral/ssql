using System.Collections;
using System.Collections.Generic;
using SSQL.Tokens;

namespace SSQL.Tests.Lexers.Data;

public class LexerFloatData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {"10.", new Token(TokenType.Float, "10.")},
        new object[] {".20", new Token(TokenType.Float, ".20")},
        new object[] {"30.0", new Token(TokenType.Float, "30.0")},
        new object[] {"+40.", new Token(TokenType.Float, "+40.")},
        new object[] {"+.50", new Token(TokenType.Float, "+.50")},
        new object[] {"+60.0", new Token(TokenType.Float, "+60.0")},
        new object[] {"-70.", new Token(TokenType.Float, "-70.")},
        new object[] {"-.80", new Token(TokenType.Float, "-.80")},
        new object[] {"-90.0", new Token(TokenType.Float, "-90.0")},
        
        new object[] {"10e10", new Token(TokenType.Float, "10e10")},
        new object[] {"10e+10", new Token(TokenType.Float, "10e+10")},
        new object[] {"10e-10", new Token(TokenType.Float, "10e-10")},
        new object[] {"+10e10", new Token(TokenType.Float, "+10e10")},
        new object[] {"+10e+10", new Token(TokenType.Float, "+10e+10")},
        new object[] {"+10e-10", new Token(TokenType.Float, "+10e-10")},
        new object[] {"-10e10", new Token(TokenType.Float, "-10e10")},
        new object[] {"-10e+10", new Token(TokenType.Float, "-10e+10")},
        new object[] {"-10e-10", new Token(TokenType.Float, "-10e-10")},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}