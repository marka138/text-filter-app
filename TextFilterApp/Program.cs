using TextFilterApp.Core.Filters;
using TextFilterApp.Core.Pipeline;
using TextFilterApp.Core.Readers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var minLength = configuration.GetValue<int?>("FilterSettings:MinLength")
    ?? throw new InvalidOperationException("FilterSettings:MinLength is missing.");

var excludedLetter = configuration.GetValue<string>("FilterSettings:ExcludedLetter")
    ?? throw new InvalidOperationException("FilterSettings:ExcludedLetter is missing.");

if (excludedLetter.Length != 1)
    throw new InvalidOperationException("ExcludedLetter must be a single character.");

var filePath = configuration.GetValue<string>("InputSettings:FilePath")
    ?? throw new InvalidOperationException("InputSettings:FilePath is missing.");

var services = new ServiceCollection();

// Filters
services.AddSingleton<ITextFilter>(new MinLengthFilter(minLength));
services.AddSingleton<ITextFilter>(new ContainsLetterFilter(excludedLetter[0]));
services.AddSingleton<ITextFilter, VowelMiddleFilter>();

// Pipeline
services.AddSingleton<FilterPipeline>();

// Reader
services.AddSingleton<ITextReader, FileTextReader>();

var provider = services.BuildServiceProvider();

// Resolve everything
var pipeline = provider.GetRequiredService<FilterPipeline>();
var reader = provider.GetRequiredService<ITextReader>();

var text = reader.Read(filePath);
var result = pipeline.Apply(text);
Console.WriteLine(string.Join(' ', result));