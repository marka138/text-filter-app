namespace TextFilterApp.Core.Filters;

public interface ITextFilter
{
    IEnumerable<string> Apply(IEnumerable<string> words);
}