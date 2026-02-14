using System.Text.RegularExpressions;
using TextFilterApp.Core.Filters;

namespace TextFilterApp.Core.Pipeline;

public class FilterPipeline
{
    private readonly IEnumerable<ITextFilter> _filters;

    public FilterPipeline(IEnumerable<ITextFilter> filters)
    {
        _filters = filters ?? throw new ArgumentNullException(nameof(filters));
    }

    public IEnumerable<string> Apply(string input)
    {
        var words = SplitWords(input);

        foreach (var filter in _filters)
        {
            words = filter.Apply(words);
        }

        return words;
    }

    private IEnumerable<string> SplitWords(string input)
    {
        input = input.ToLower();

        // Replace punctuation with space (avoid word merging)
        input = Regex.Replace(input, "[^a-z\\s]", " ");

        return input
            .Split([' ', '\r', '\n'],
                StringSplitOptions.RemoveEmptyEntries);
    }
}