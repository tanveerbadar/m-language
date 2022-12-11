using System.Text.Json;
using Xunit.Abstractions;

namespace m.parser.tests;

public class TokenizerTests
{
    ITestOutputHelper helper;

    public TokenizerTests(ITestOutputHelper helper) => this.helper = helper;

    [Fact]
    public void EmptyFileTest()
    {
        Tokenizer t = new("data/tokenizer/empty.m");
        var result = t.Next();

        Assert.True(result == ReadOnlySpan<char>.Empty);
        Assert.True(t.Eof);
    }

    [Fact]
    public void SimpleTests()
    {
        foreach (var file in Directory.EnumerateFiles("data/tokenizer", "simple*.m"))
        {
            helper.WriteLine($"running test for {Path.GetFileName(file)}.");

            var name = Path.GetFileName(Path.ChangeExtension(file, "json"));
            Tokenizer t = new(file);
            var results = JsonSerializer.Deserialize<string[]>(File.ReadAllText(Path.Combine("results/tokenizer", name)));

            for (var i = 0; i < results.Length; ++i)
            {
                var tok = t.Next();
                Assert.True(tok.SequenceEqual(results[i]));
            }

            var result = t.Next();
            Assert.True(result == ReadOnlySpan<char>.Empty);
            Assert.True(t.Eof);
        }
    }
}