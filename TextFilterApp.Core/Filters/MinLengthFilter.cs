namespace TextFilterApp.Core.Filters;

public class MinLengthFilter : ITextFilter
{
    private readonly int _minLength;

    public MinLengthFilter(int minLength)
    {
        _minLength = minLength;
    }

    public IEnumerable<string> Apply(IEnumerable<string> words)
    {
        return words.Where(w => w.Length >= _minLength);
    }
}