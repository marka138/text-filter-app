namespace TextFilterApp.Core.Filters;

public class VowelMiddleFilter : ITextFilter
{
    private static readonly HashSet<char> Vowels = 
        new() { 'a', 'e', 'i', 'o', 'u' };

    public IEnumerable<string> Apply(IEnumerable<string> words)
    {
        return words.Where(w => !HasMiddleVowel(w));
    }

    private bool HasMiddleVowel(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return false;

        word = word.ToLower();

        int length = word.Length;

        if (length % 2 == 1)
        {
            int middle = length / 2;
            return Vowels.Contains(word[middle]);
        }
        else
        {
            int middle1 = length / 2 - 1;
            int middle2 = length / 2;
            return Vowels.Contains(word[middle1]) || 
                   Vowels.Contains(word[middle2]);
        }
    }
}