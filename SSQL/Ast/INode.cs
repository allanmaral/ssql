namespace SSQL.Ast;

public interface INode
{
    // public string TokenLiteral();
}

public interface IStatement : INode
{
    // public INode StatementNode();
}

public interface IExpression : IStatement
{
    // public INode ExpressionNode();
}