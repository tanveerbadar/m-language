using System.Linq.Expressions;

namespace m.parser;

public class Parser
{
    Tokenizer tokenizer;

    public Expression Expression { get; private set; }

    public Parser(string fileName)
    {
        tokenizer = new(fileName);
    }

    public void Parse()
    {
        if (tokenizer.Eof)
        {
            Expression = Expression.Empty();
            return;
        }
        Expression = ParseBinaryExpression();
    }

    Expression ParseBinaryExpression()
    {
        var left = tokenizer.Next();
        if (tokenizer.Eof)
        {
            if (int.TryParse(left, out var i))
            {
                return Expression.Constant(i);
            }
            if (left == "+" || left == "-")
            {
                return Expression.Empty();
            }
        }
        var op = GetOperator();
        var right = tokenizer.Next();
        return op.SequenceEqual("+") ?
            Expression.Add(
                Expression.Constant(int.Parse(left)),
                Expression.Constant(int.Parse(right))) :
            Expression.Subtract(
                Expression.Constant(int.Parse(left)),
                Expression.Constant(int.Parse(right)));
    }

    ReadOnlySpan<char> GetOperator()
    {
        var token = tokenizer.Next();
        if (token.SequenceEqual("+") || token.SequenceEqual("-"))
        {
            return token;
        }
        return ReadOnlySpan<char>.Empty;
    }
}
