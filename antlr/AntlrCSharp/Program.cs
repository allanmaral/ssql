// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;
using System.Text;
using Antlr4.Runtime;
using AntlrCSharp;

Console.WriteLine("Hello, World!");

var sample = new Sample()
{
    Foo = new Foo()
    {
        Bar = "Hello World"
    },
    Number = 123
};

bool Match<TModel>(string filter, TModel model)
{
    var inputStream = new AntlrInputStream(filter);
    var lexer = new SSQLLexer(inputStream);
    var tokenStream = new CommonTokenStream(lexer);
    var parser = new SSQLParser(tokenStream);

    var visitor = new SuperSimpleQueryLanguageVisitor<TModel>();

    var queryContext = parser.query();
    var match = visitor.GetFilterQuery(queryContext);

    return match(model);
}

try
{
    var filter = "eq(Number, 123)";
    // var input = "and(eq(sku, 123), gte(date, +12.34))";

    var matches = Match(filter, sample);

    // var inputStream = new AntlrInputStream(text.ToString());
    // var speakLexer = new SpeakLexer(inputStream);
    // var commonTokenStream = new CommonTokenStream(speakLexer);
    // var speakParser = new SpeakParser(commonTokenStream);
    //
    // var chatContext = speakParser.chat();
    // var visitor = new BasicSpeakVisitor();
    // visitor.Visit(chatContext);
    //
    // foreach (var line in visitor.Lines)
    // {
    //     Console.WriteLine($"{line.Person} has said {line.Text}");
    // }
}
catch (Exception e)
{
    Console.WriteLine("Error: " + e);
}