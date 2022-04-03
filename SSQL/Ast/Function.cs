using System.Linq.Expressions;
using SSQL.Tokens;

namespace SSQL.Ast;

public class Function : IExpression
{
    public Token Identifier { get; }
    public List<IExpression> Arguments { get; }

    public Function(Token identifier, List<IExpression> arguments)
    {
        Identifier = identifier;
        Arguments = arguments;
    }
    
    // public string TokenLiteral()
    // {
    //     return Identifier.Literal;
    // }
    //
    // public INode StatementNode()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public INode ExpressionNode()
    // {
    //     throw new NotImplementedException();
    // }
}