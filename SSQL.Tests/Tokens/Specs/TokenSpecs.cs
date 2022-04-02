using FluentAssertions;
using SSQL.Tokens;
using Xunit;

namespace SSQL.Tests.Tokens.Specs;

public class TokenSpecs
{
    [Fact]
    public void Should_be_equal_if_they_have_the_same_type_and_literal()
    {
        var tokenA = new Token(TokenType.Identifier, "a");
        var tokenB = new Token(TokenType.Identifier, "a");

        var result = tokenA.Equals(tokenB);

        result.Should().BeTrue();
    }
    
    [Fact]
    public void Should_have_the_same_hash_code_if_they_have_same_type_and_literal()
    {
        var tokenA = new Token(TokenType.Identifier, "a");
        var tokenB = new Token(TokenType.Identifier, "a");

        var hashCodeA = tokenA.GetHashCode();
        var hashCodeB = tokenB.GetHashCode();

        hashCodeA.Should().Be(hashCodeB);
    }
    
    [Fact]
    public void Should_be_able_to_handle_null_comparison()
    {
        var tokenA = (object)new Token(TokenType.Identifier, "a");

        var result = tokenA.Equals(null);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void Should_be_able_to_handle_comparison_with_other_types()
    {
        var tokenA = (object)new Token(TokenType.Identifier, "a");

        var result = tokenA.Equals("any_string");

        result.Should().BeFalse();
    }
}