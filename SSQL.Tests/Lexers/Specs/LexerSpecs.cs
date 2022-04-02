using System.Collections.Generic;
using FluentAssertions;
using SSQL.Tests.Lexers.Data;
using SSQL.Tokens;
using Xunit;

namespace SSQL.Tests.Lexers.Specs;

public class LexerSpecs
{
    [Theory]
    [ClassData(typeof(LexerSingleTokenData))]
    public void Should_be_able_to_read_single_tokens(string input, Token expectedResult)
    {
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = lexer.NextToken();

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_read_multiple_tokens()
    {
        const string input = "(),";
        var expectedResult = new List<Token>
        {
            Token.LeftParenthesis,
            Token.RightParenthesis,
            Token.Comma,
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_read_identifiers()
    {
        const string input = "eq()";
        var expectedResult = new List<Token>
        {
            new(TokenType.Identifier, "eq"),
            Token.LeftParenthesis,
            Token.RightParenthesis,
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_skip_whitespaces()
    {
        const string input = "eq \t(\r\n)";
        var expectedResult = new List<Token>
        {
            new(TokenType.Identifier, "eq"),
            Token.LeftParenthesis,
            Token.RightParenthesis,
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_read_boolean()
    {
        const string input = "true false";
        var expectedResult = new List<Token>
        {
            new(TokenType.True, "true"),
            new(TokenType.False, "false"),
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_read_integer_literal()
    {
        const string input = "123";
        var expectedResult = new Token(TokenType.Int, "123");
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = lexer.NextToken();

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Theory]
    [ClassData(typeof(LexerFloatData))]
    public void Should_be_able_to_read_float_literal(string input, Token output)
    {
        var expectedResult = new List<Token>
        {
            output,
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_be_able_to_emit_illegal_on_invalid_floats()
    {
        const string input = "123ee1 456e+ 789..0";
        var expectedResult = new List<Token>
        {
            new(TokenType.Illegal, "123e"),
            new(TokenType.Identifier, "e"),
            new(TokenType.Int, "1"),
            
            new(TokenType.Illegal, "456e"),
            new(TokenType.Illegal, "+"),
            
            new(TokenType.Illegal, "789."),
            new(TokenType.Float, ".0"),
            
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    // [Fact]
    // public void Should_be_able_to_read_string_literal()
    // {
    //     const string input = "\"any_string\"";
    //     var expectedResult = new Token(TokenType.String, "any_string");
    //     var lexer = new Lexers.Lexer(input);
    //     
    //     var result = lexer.NextToken();
    //
    //     result.Should().BeEquivalentTo(expectedResult);
    // }
    
    [Theory]
    [ClassData(typeof(LexerStringData))]
    public void Should_be_able_to_read_string_literal(string input, Token output)
    {
        var expectedResult = new List<Token>
        {
            output,
            Token.Eof
        };
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = new List<Token>();
        for (var i = 0; i < expectedResult.Count; i++)
        {
            result.Add(lexer.NextToken());
        }

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_emit_illegal_on_unclosed_strings()
    {
        const string input = "\"any_string\\";
        var expectedResult = new Token(TokenType.Illegal, "any_string\\");
        var lexer = new SSQL.Lexers.Lexer(input);
        
        var result = lexer.NextToken();

        result.Should().BeEquivalentTo(expectedResult);
    }
    
    [Fact]
    public void Should_be_able_to_read_string_with_scape_character()
    {
        const string input = "\"any\\\"string\"";
        var expectedResult = new Token(TokenType.String, "any\\\"string");
        var lexer = new SSQL.Lexers.Lexer(input);

        var result = lexer.NextToken();

        result.Should().BeEquivalentTo(expectedResult);
    }
}