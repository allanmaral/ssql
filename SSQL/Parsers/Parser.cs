using SSQL.Lexers;
using SSQL.Tokens;
using SSQL.Ast;

namespace SSQL.Parsers;

public class Parser
{
    private Lexer Lexer { get; }
    private Token CurrentToken { get; set; }
    private Token PeekToken { get; set; }
    public List<string> Errors { get; }


    public Parser(Lexer lexer)
    {
        Lexer = lexer;
        CurrentToken = Lexer.NextToken();
        PeekToken = Lexer.NextToken();
        Errors = new List<string>();
    }

    /// <summary>
    /// Parse the query from the input.
    /// </summary>
    /// <returns>A Query AST.</returns>
    public Query ParseQuery()
    {
        var expressions = new List<IExpression>();

        while (CurrentToken != Token.Eof)
        {
            var expression = ParseFunctionCallExpression();
            if (expression != null)
            {
                expressions.Add(expression);
            }
        }

        return new Query(expressions);
    }

    /// <summary>
    /// Read the next token from the input.
    /// </summary>
    private void NextToken()
    {
        CurrentToken = PeekToken;
        PeekToken = Lexer.NextToken();
    }

    private IExpression? ParseExpression()
    {
        IExpression? expression;
        switch (CurrentToken.Type)
        {
            case TokenType.True:
            case TokenType.False:
                expression = new BooleanLiteral(CurrentToken, CurrentToken.Type == TokenType.True);
                break;
            case TokenType.String:
                expression = new StringLiteral(CurrentToken, CurrentToken.Literal);
                break;
            case TokenType.Int:
                expression = new IntegerLiteral(CurrentToken, Convert.ToInt64(CurrentToken.Literal));
                break;
            case TokenType.Float:
                expression = new FloatLiteral(CurrentToken, Convert.ToDouble(CurrentToken.Literal));
                break;
            case TokenType.Identifier:
                if (PeekTokenIs(TokenType.LeftParenthesis))
                {
                    return ParseFunctionCallExpression();
                } 
                expression = new Identifier(CurrentToken, CurrentToken.Literal);
                break;
            default:
                expression = null;
                break;
        }

        NextToken();
        return expression;
    }

    private IExpression? ParseFunctionCallExpression()
    {
        var identifier = CurrentToken;

        if (!ExpectPeek(TokenType.LeftParenthesis))
        {
            return null;
        }

        NextToken();
        var parameters = ParseFunctionParameters();

        if (!ExpectToken(TokenType.RightParenthesis))
        {
            return null;
        }

        return new Function(identifier, parameters);
    }

    private List<IExpression> ParseFunctionParameters()
    {
        var expressions = new List<IExpression>();
        
        while (!CurrentTokenIs(TokenType.RightParenthesis))
        {
            var expression = ParseExpression();
            if (expression != null)
            {
                expressions.Add(expression);
            }
        }

        return expressions;
    }

    /// <summary>
    /// Check if current token matches a given type.
    /// </summary>
    /// <param name="type">Type to match.</param>
    /// <returns>True if the current token type is the same as `type`, false otherwise.</returns>
    private bool CurrentTokenIs(TokenType type)
    {
        return CurrentToken.Type == type;
    }

    /// <summary>
    /// Check if the next token matches a given type.
    /// </summary>
    /// <param name="type">Type to match.</param>
    /// <returns>True if the next token type is the same as `type`, false otherwise.</returns>
    private bool PeekTokenIs(TokenType type)
    {
        return PeekToken.Type == type;
    }

    private bool ExpectPeek(TokenType type)
    {
        if (PeekTokenIs(type))
        {
            NextToken();
            return true;
        }
        PeekError(type);
        return false;
    }
    
    private bool ExpectToken(TokenType type)
    {
        if (CurrentTokenIs(type))
        {
            NextToken();
            return true;
        }
        CurrentError(type);
        return false;
    }

    /// <summary>
    /// Emit a syntactical error.
    /// </summary>
    /// <param name="type">Expected token type.</param>
    private void PeekError(TokenType type)
    {
        Errors.Add($"Expected next token to be {type}, got {PeekToken.Type} instead");
    }
    
    /// <summary>
    /// Emit a syntactical error.
    /// </summary>
    /// <param name="type">Expected token type.</param>
    private void CurrentError(TokenType type)
    {
        Errors.Add($"Expected next token to be {type}, got {CurrentToken.Type} instead");
    }
}