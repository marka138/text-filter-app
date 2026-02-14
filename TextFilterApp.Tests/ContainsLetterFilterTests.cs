using FluentAssertions;
using TextFilterApp.Core.Filters;

public class ContainsLetterFilterTests
{
    [Fact]
    public void Removes_Words_Containing_Specified_Letter()
    {
        var filter = new ContainsLetterFilter('t');
        var input = new[] { "cat", "dog", "tree" };

        var result = filter.Apply(input);

        result.Should().BeEquivalentTo(new[] { "dog" });
    }

    [Fact]
    public void Is_Case_Insensitive()
    {
        var filter = new ContainsLetterFilter('t');
        var input = new[] { "Tree", "DOG" };

        var result = filter.Apply(input);

        result.Should().ContainSingle().Which.Should().Be("DOG");
    }

    [Fact]
    public void Keeps_All_When_Letter_Not_Present()
    {
        var filter = new ContainsLetterFilter('z');
        var input = new[] { "cat", "dog" };

        var result = filter.Apply(input);

        result.Should().BeEquivalentTo(input);
    }

    [Fact]
    public void Returns_Empty_When_All_Filtered()
    {
        var filter = new ContainsLetterFilter('a');
        var input = new[] { "cat", "apple" };

        var result = filter.Apply(input);

        result.Should().BeEmpty();
    }
}