using System.Linq.Expressions;
using System.Text.Json;
using Xunit.Abstractions;

namespace m.parser.tests;

public class ParserTests
{
    ITestOutputHelper helper;

    public ParserTests(ITestOutputHelper helper) => this.helper = helper;

    class ResultData
    {
        public ExpressionType Type { get; set; }
    }

    [Fact]
    public void Parse1Tests()
    {
        Parser p = new("data/tokenizer/empty.m");
        p.Parse();

        var results = JsonSerializer.Deserialize<ResultData>(File.ReadAllText("results/parser/empty.json"), new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        Assert.True(p.Expression.NodeType == results.Type);
    }


    [Fact]
    public void SimpleTests()
    {
        foreach (var file in Directory.EnumerateFiles("data/tokenizer", "simple*.m"))
        {
            helper.WriteLine($"running test for {Path.GetFileName(file)}.");

            Parser p = new(file);
            p.Parse();
            var e = p.Expression;
        }
    }
}