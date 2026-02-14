using FluentAssertions;
using TextFilterApp.Core.Filters;

public class MinLengthFilterTests
{
    [Fact]
    public void Removes_Words_Shorter_Than_MinLength()
    {
        var filter = new MinLengthFilter(3);
        var input = new[] { "a", "to", "cat", "house" };

        var result = filter.Apply(input);

        result.Should().BeEquivalentTo(new[] { "cat", "house" });
    }

    [Fact]
    public void Keeps_Words_Equal_To_MinLength()
    {
        var filter = new MinLengthFilter(3);
        var input = new[] { "cat" };

        var result = filter.Apply(input);

        result.Should().ContainSingle().Which.Should().Be("cat");
    }

    [Fact]
    public void Returns_Empty_When_Input_Is_Empty()
    {
        var filter = new MinLengthFilter(3);

        var result = filter.Apply(Array.Empty<string>());

        result.Should().BeEmpty();
    }

    [Fact]
    public void Handles_Empty_Strings_In_Input()
    {
        var filter = new MinLengthFilter(3);
        var input = new[] { "", "valid" };

        var result = filter.Apply(input);

        result.Should().ContainSingle().Which.Should().Be("valid");
    }
}
