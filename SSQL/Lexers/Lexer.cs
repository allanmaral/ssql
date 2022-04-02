using SSQL.Tokens;

namespace SSQL.Lexers;

public class Lexer
{
    private string Input { get; }
    private int Position { get; set; }
    private int ReadPosition { get; set; }
    private char CurrentSymbol { get; set; }

    public Lexer(string input)
    {
        Input = input;
        Position = 0;
        ReadPosition = 0;
        CurrentSymbol = (char) 0;
        ReadChar();
    }

    public Token NextToken()
    {
        SkipWhitespace();
        Token token;
        switch (CurrentSymbol)
        {
            case '(':
                token = Token.LeftParenthesis;
                break;
            case ')':
                token = Token.RightParenthesis;
                break;
            case ',':
                token = Token.Comma;
                break;
            case (char) 0:
                token = Token.Eof;
                break;
            case '"':
                token = ReadString();
                break;
            default:
                if (IsLetter(CurrentSymbol))
                {
                    var identifier = ReadIdentifier();
                    var type = LookupIdentifierType(identifier);
                    token = new Token(type, identifier);
                    return token;
                }
                else if (IsDigit(CurrentSymbol) || CurrentSymbol == '-' || CurrentSymbol == '+' || CurrentSymbol == '.')
                {
                    token = ReadNumber();
                    return token;
                }
                else
                {
                    token = new Token(TokenType.Illegal, CurrentSymbol);
                }
                break;
        }

        ReadChar();
        return token;
    }

    /// <summary>
    /// Reads the next character in the input
    /// </summary>
    private void ReadChar()
    {
        if (ReadPosition >= Input.Length)
        {
            CurrentSymbol = (char) 0;
        }
        else
        {
            CurrentSymbol = Input[ReadPosition];
        }

        Position = ReadPosition;
        ReadPosition += 1;
    }

    /// <summary>
    /// Looks ahead in the input for an identifier and return its value
    /// </summary>
    /// <returns>The identifier string</returns>
    private string ReadIdentifier()
    {
        var position = Position;
        while (IsLetter(CurrentSymbol))
        {
            ReadChar();
        }

        return Input[position..Position];
    }

    /// <summary>
    /// Looks ahead in the input for an number and return its value
    /// </summary>
    /// <returns>The number token</returns>
    private Token ReadNumber()
    {
        var position = Position;
        var hasDecimal = false;
        var hasExponent = false;
        
        // Skip '+' and '-' symbols
        if (CurrentSymbol == '+' || CurrentSymbol == '-')
        {
            ReadChar();
            if (!IsDigit(CurrentSymbol) && CurrentSymbol != '.')
            {
                return new Token(TokenType.Illegal, Input[position..Position]);
            }
        }

        while (true)
        {
            // Number with multiple decimal cases are illegal
            if (CurrentSymbol == '.' && hasDecimal)
            {
                return new Token(TokenType.Illegal, Input[position..Position]);
            }
            // Number with multiple exponential characters are illegal
            if (CurrentSymbol == 'e' && hasExponent)
            {
                return new Token(TokenType.Illegal, Input[position..Position]);
            }
            
            
            if (CurrentSymbol == '.')
            {
                hasDecimal = true;
            }
            else if (CurrentSymbol == 'e')
            {
                hasExponent = true;
                if (PeekCharacter() == '+' || PeekCharacter() == '-')
                {
                    ReadChar();
                    if (!IsDigit(PeekCharacter()))
                    {
                        return new Token(TokenType.Illegal, Input[position..Position]);
                    }
                }
            }
            else if (!IsDigit(CurrentSymbol))
            {
                break;
            }
            ReadChar();
        }

        var literal = Input[position..Position];
        if (hasDecimal || hasExponent)
        {
            return new Token(TokenType.Float, literal);
        }

        return new Token(TokenType.Int, literal);
    }
    
    /// <summary>
    /// Looks ahead in the input for an string and return its value
    /// </summary>
    /// <returns>The string token</returns>
    private Token ReadString()
    {
        var position = Position + 1;
        while (true)
        {
            ReadChar();
            if (CurrentSymbol == '\\' && PeekCharacter() == '"')
            {
                ReadChar();
                continue;
            }
            
            if (CurrentSymbol == '"')
            {
                break;
            }

            if (CurrentSymbol == (char) 0)
            {
                return new Token(TokenType.Illegal, Input[position..Position]);
            }
        }

        return new Token(TokenType.String, Input[position..Position]);
    }

    /// <summary>
    /// Skips the input whitespaces
    /// </summary>
    private void SkipWhitespace()
    {
        while (CurrentSymbol == ' ' || CurrentSymbol == '\t' || CurrentSymbol == '\n' || CurrentSymbol == '\r')
        {
            ReadChar();
        }
    }

    /// <summary>
    /// Look one character ahead without incrementing the current position
    /// </summary>
    /// <returns>The next character in the input</returns>
    private char PeekCharacter()
    {
        if (ReadPosition >= Input.Length)
        {
            return (char) 0;
        }

        return Input[ReadPosition];
    }

    /// <summary>
    /// Check if symbol is a letter
    /// </summary>
    /// <param name="symbol">symbol to be checked</param>
    /// <returns>True if it is a letter, false otherwise</returns>
    private static bool IsLetter(char symbol)
    {
        return ('a' <= symbol && symbol <= 'z') || ('A' <= symbol && symbol <= 'Z');
    }

    /// <summary>
    /// Check if symbol is a digit
    /// </summary>
    /// <param name="symbol">symbol to be checked</param>
    /// <returns>True if it is a digit, false otherwise</returns>
    private static bool IsDigit(char symbol)
    {
        return '0' <= symbol && symbol <= '9';
    }

    /// <summary>
    /// Check if the identifier is a keyword
    /// </summary>
    /// <param name="identifier">Identifier to lookup</param>
    /// <returns>The type of the keyword if it is one, TokenType.Identifier otherwise</returns>
    private static TokenType LookupIdentifierType(string identifier)
    {
        if (_keywords.TryGetValue(identifier, out var type))
        {
            return type;
        }

        return TokenType.Identifier;
    }

    private static readonly Dictionary<string, TokenType> _keywords = new()
    {
        {"false", TokenType.False},
        {"true", TokenType.True}
    };
}