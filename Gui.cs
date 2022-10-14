namespace Lab8;

public class Gui
{
    private int _windowHeight;
    private int _windowWidth;
    private Command[] _commands;
    
    public Gui(int height, int width, string[] fileContent)
    {
        if(height >= 0 && width >= 0)
        {
            _windowHeight = height;
            _windowWidth = width;
        }
        foreach (string line in fileContent)
        {
            
        }
    }

    public bool GetCommandsFromFile(string[] fileContent)
    {
        _commands = new Command[fileContent.Length];
        for (int i = 0; i < _commands.Length; i++)
        {
            Command command = new Command(fileContent[i]);
            _commands[i] = command;
        }

        return true;
    }
}