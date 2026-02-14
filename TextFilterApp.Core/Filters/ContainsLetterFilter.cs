namespace TextFilterApp.Core.Filters;

public class ContainsLetterFilter : ITextFilter
{
    private readonly char _letter;

    public ContainsLetterFilter(char letter)
    {
        _letter = char.ToLower(letter);
    }

    public IEnumerable<string> Apply(IEnumerable<string> words)
    {
        return words.Where(w => !w.ToLower().Contains(_letter));
    }
}