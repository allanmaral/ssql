using System.Collections;
using System.Collections.Generic;
using SSQL.Ast;
using SSQL.Tokens;

namespace SSQL.Tests.Parsers.Data;

public class ParserNestedFunctionData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] {"eq(fun())", new List<IExpression>
        {
            new Function(new Token(TokenType.Identifier, "eq"), new List<IExpression>
            {
                new Function(new Token(TokenType.Identifier, "fun"), new List<IExpression>())
            })
        }},
        new object[] {"eq(fun(id))", new List<IExpression>
        {
            new Function(new Token(TokenType.Identifier, "eq"), new List<IExpression>
            {
                new Function(new Token(TokenType.Identifier, "fun"), new List<IExpression>
                {
                    new Identifier(new Token(TokenType.Identifier, "id"), "id")
                })
            })
        }},
        new object[] {"eq(fun(123))", new List<IExpression>
        {
            new Function(new Token(TokenType.Identifier, "eq"), new List<IExpression>
            {
                new Function(new Token(TokenType.Identifier, "fun"), new List<IExpression>
                {
                    new IntegerLiteral(new Token(TokenType.Int, "123"), 123)
                })
            })
        }},
        new object[] {"eq(fun(12.3))", new List<IExpression>
        {
            new Function(new Token(TokenType.Identifier, "eq"), new List<IExpression>
            {
                new Function(new Token(TokenType.Identifier, "fun"), new List<IExpression>
                {
                    new FloatLiteral(new Token(TokenType.Float, "12.3"), 12.3)
                })
            })
        }},
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}