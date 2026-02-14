namespace TextFilterApp.Core.Readers;

public class FileTextReader : ITextReader
{
    public string Read(string path)
    {
        return File.ReadAllText(path);
    }
}