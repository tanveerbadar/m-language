namespace m.parser;

class Tokenizer
{
    string[] lines;
    int currentLine, currentCharacter;

    string CurrentLine => lines[currentLine];

    public bool Eof { get; private set; }

    public Tokenizer(string fileName)
    {
        lines = File.ReadAllLines(fileName);

        if (lines.Length == 0)
        {
            Eof = true;
        }
    }

    public ReadOnlySpan<char> Next()
    {
        if (Eof)
        {
            return Span<char>.Empty;
        }

        var end = CurrentLine.AsSpan(currentCharacter).IndexOfAny(new[] { ' ', '+', '-', ';', '\n', '\r' });
        var effectiveLength = end switch
        {
            -1 => CurrentLine.Length - currentCharacter,
            0 => 1,
            _ => end,
        };
        var s = CurrentLine.AsSpan(currentCharacter, effectiveLength);
        currentCharacter += effectiveLength;

        if (CurrentLine.Length == currentCharacter)
        {
            ++currentLine;
            if (lines.Length >= currentLine)
            {
                Eof = true;
            }
        }
        return s;
    }
}
