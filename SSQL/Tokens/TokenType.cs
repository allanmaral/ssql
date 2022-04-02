using System.ComponentModel;

namespace SSQL.Tokens;

public enum TokenType
{
    [Description("ILLEGAL")]
    Illegal,
    [Description("EOF")]
    Eof,
    [Description("IDENTIFIER")]
    Identifier,
    [Description("INT")]
    Int,
    [Description("FLOAT")]
    Float,
    [Description("TRUE")]
    True,
    [Description("FALSE")]
    False,
    [Description("STRING")]
    String,
    [Description(".")]
    Dot,
    [Description(",")]
    Comma,
    [Description("(")]
    LeftParenthesis,
    [Description(")")]
    RightParenthesis
}