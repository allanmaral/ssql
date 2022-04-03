namespace SSQL.Ast;

public class Query : INode
{
    public List<IExpression> Expressions { get; }

    public Query(List<IExpression> expressions)
    {
        Expressions = expressions;
    }
    
    // public string TokenLiteral()
    // {
    //     return Expressions.Count > 0 ? Expressions.First().TokenLiteral() : "";
    // }
}