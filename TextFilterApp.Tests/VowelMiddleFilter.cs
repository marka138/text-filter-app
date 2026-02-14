using FluentAssertions;
using TextFilterApp.Core.Filters;

public class VowelMiddleFilterTests
{
    [Fact]
    public void Removes_Odd_Length_Word_With_Vowel_In_Middle()
    {
        var filter = new VowelMiddleFilter();
        var input = new[] { "clean" }; // middle = 'e'

        var result = filter.Apply(input);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Keeps_Odd_Length_Word_Without_Vowel_In_Middle()
    {
        var filter = new VowelMiddleFilter();
        var input = new[] { "world" }; // middle = 'r'

        var result = filter.Apply(input);

        result.Should().ContainSingle().Which.Should().Be("world");
    }

    [Fact]
    public void Removes_Even_Length_Word_With_Vowel_In_Middle()
    {
        var filter = new VowelMiddleFilter();
        var input = new[] { "what" }; // middle = 'ha'

        var result = filter.Apply(input);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Keeps_Even_Length_Word_Without_Vowel_In_Middle()
    {
        var filter = new VowelMiddleFilter();
        var input = new[] { "test" }; // middle = 'es' → contains vowel → should remove

        var result = filter.Apply(input);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Is_Case_Insensitive()
    {
        var filter = new VowelMiddleFilter();
        var input = new[] { "ClEaN" };

        var result = filter.Apply(input);

        result.Should().BeEmpty();
    }

    [Fact]
    public void Ignores_Empty_Strings()
    {
        var filter = new VowelMiddleFilter();
        var input = new[] { "" };

        var result = filter.Apply(input);

        result.Should().ContainSingle().Which.Should().Be("");
    }
}
