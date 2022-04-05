using System.Linq.Expressions;
using System.Reflection;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp;

public class SuperSimpleQueryLanguageVisitor<TModel> : SSQLBaseVisitor<Expression>
{
    private readonly PropertyInfo[] Properties;
    private readonly ParameterExpression Parameter;
    public SuperSimpleQueryLanguageVisitor()
    {
        Properties = typeof(TModel).GetProperties();
        Parameter = Expression.Parameter(typeof(TModel), "p");
    }

    public Func<TModel, bool> GetFilterQuery(SSQLParser.QueryContext context)
    {
        var expression = Visit(context);
        return Expression.Lambda<Func<TModel, bool>>(expression, Parameter).Compile();
    }

    private Expression GetProperty(string identifier)
    {
        var expression = identifier
            .Split(".")
            .Aggregate((Expression?)null, (ex, field) => ex == null 
                    ? Expression.Property(Parameter, field) 
                    : Expression.Property(ex, field));

        return expression;
    }

    public override Expression VisitQuery(SSQLParser.QueryContext context)
    {
        return VisitFunction(context.function());
    }

    public override Expression VisitFunction(SSQLParser.FunctionContext context)
    {
        var identifier = context.IDENTIFIER().GetText();
        
        var arguments = context
            .expressionList()
            .expression()
            .Select(VisitExpression)
            .ToList();

        return arguments.Aggregate((Expression)null, 
            (result, exp) => result != null 
                ? Expression.Equal(result, exp)
                : exp);
    }

    public override Expression VisitName(SSQLParser.NameContext context)
    {
        var path = context.GetText();
        return GetProperty(path);
    }

    public override Expression VisitLiteral(SSQLParser.LiteralContext context)
    {
        var token = (CommonToken) context.GetChild(0).Payload;
        object? value;
        switch (token.Type)
        {
            // TRUE_LIT=6
            case 6:
                value = true;
                break;
            // FALSE_LIT=7
            case 7:
                value = false;
                break;
            // DECIMAL_LIT=8
            case 8:
                value = Convert.ToInt64(token.Text);
                break;
            // FLOAT_LIT=9
            case 9:
                value = Convert.ToDouble(token.Text);
                break;
            // STRING_LIT=10
            case 10:
                value = token.Text.Substring(1, token.Text.Length - 2);
                break;
            default:
                value = null;
                break;
        }
        
        return Expression.Constant(value);
    }
}