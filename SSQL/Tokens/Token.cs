namespace SSQL.Tokens;

public class Token
{
    public static Token LeftParenthesis = new Token(TokenType.LeftParenthesis, '(');
    public static Token RightParenthesis = new Token(TokenType.RightParenthesis, ')');
    public static Token Comma = new Token(TokenType.Comma, ',');
    public static Token Eof = new Token(TokenType.Eof, (char)0);
    
    public TokenType Type { get; }
    public string Literal { get; }

    public Token(TokenType type, char literal)
    {
        Type = type;
        Literal = literal.ToString();
    }
    
    public Token(TokenType type, string literal)
    {
        Type = type;
        Literal = literal;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
    
        if (GetType() != obj.GetType())
        {
            return false;
        }
    
        var token = (Token)obj;
    
        return Equals(token);
    }

    private bool Equals(Token other)
    {
        return Type == other.Type && Literal == other.Literal;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int) Type, Literal);
    }
}