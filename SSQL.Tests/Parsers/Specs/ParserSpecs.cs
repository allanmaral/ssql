using System.Collections.Generic;
using FluentAssertions;
using SSQL.Ast;
using SSQL.Lexers;
using SSQL.Parsers;
using SSQL.Tests.Parsers.Data;
using SSQL.Tokens;
using Xunit;

namespace SSQL.Tests.Parsers.Specs;

public class ParserSpecs
{
    [Fact]
    public void Should_return_empty_query_with_empty_input()
    {
        const string input = "";
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);

        var query = parser.ParseQuery();

        query.Expressions.Should().BeEmpty();
    }

    [Fact]
    public void Should_be_able_to_parse_empty_function_calls()
    {
        const string input = "fun()";
        var expectedResult = new Query(new List<IExpression>
        {
            new Function(new Token(TokenType.Identifier, "fun"), new List<IExpression>())
        });
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);

        var result = parser.ParseQuery();

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Theory]
    [ClassData(typeof(ParserNestedFunctionData))]
    public void Should_be_able_to_parse_nested_function_calls(string input, List<IExpression> output)
    {
        var expectedResult = new Query(output);
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);

        var result = parser.ParseQuery();

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Theory]
    [ClassData(typeof(ParserBooleanData))]
    public void Should_be_able_to_parse_functions_with_boolean_literal(string input, IExpression output)
    {
        var expectedResult = new Query(
            new List<IExpression>
            {
                new Function(new Token(TokenType.Identifier, "eq"), new List<IExpression>
                {
                    output
                })
            });
        var lexer = new Lexer(input);
        var parser = new Parser(lexer);

        var result = parser.ParseQuery();

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Theory]
    [ClassData(typeof(ParserIntegerData))]
    public void Should_be_able_to_parse_functions_with_integer_literal(string input, IExpression output)
    {
        var expectedResult = new Query(
            new List<IExpression>
            {
                new Function(new Token(TokenType.Identifier, "eq"), new List<IExpression>
                {
                    output
                })
            });
        var lexer = new Lexer($"eq({input})");
        var parser = new Parser(lexer);

        var result = parser.ParseQuery();

        result.Should().BeEquivalentTo(expectedResult);
    }
}