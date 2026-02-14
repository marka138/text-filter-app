using FluentAssertions;
using TextFilterApp.Core.Filters;
using TextFilterApp.Core.Pipeline;

public class FilterPipelineTests
{
    [Fact]
    public void Applies_All_Filters_In_Order()
    {
        var filters = new ITextFilter[]
        {
            new MinLengthFilter(3),
            new ContainsLetterFilter('t'),
            new VowelMiddleFilter()
        };

        var pipeline = new FilterPipeline(filters);

        var input = "cat dog tree clean sky";

        var result = pipeline.Apply(input);

        result.Should().BeEquivalentTo(new[] { "sky" });
    }

    [Fact]
    public void Returns_Empty_When_All_Filtered_Out()
    {
        var filters = new ITextFilter[]
        {
            new MinLengthFilter(3),
            new ContainsLetterFilter('a')
        };

        var pipeline = new FilterPipeline(filters);

        var result = pipeline.Apply("cat apple banana");

        result.Should().BeEmpty();
    }
}