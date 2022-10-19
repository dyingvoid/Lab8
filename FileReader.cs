namespace Lab8;

class FileReader
{
    private string[] _fileContent;
    private string _filePath;

    public string[] FileContent
    {
        get => _fileContent;
        set => _fileContent = value;
    }

    public string FilePath
    {
        get => _filePath;
        init
        {
            if (File.Exists(value))
            {
                _filePath = value;
            }
            else
            {
                Console.WriteLine("Wrong path. Path is set to empty string.");
                _filePath = "";
            }
        }
    }

    public FileReader(string filePath)
    {
        FilePath = filePath;
        if(FilePath.Length > 0)
        {
            FileContent = File.ReadAllLines(FilePath);
        }
        else
        {
            FileContent = new string[] { };
        }
    }
}