namespace Lab8;

public class Gui
{
    private int _windowWidth;
    private int _windowHeight;

    public Gui(int width, int height, List<Command> commands)
    {
        if(height >= 0 && width >= 0)
        {
            _windowWidth = width;
            _windowHeight = height;
            //StartGuiStream();
            ProcessCommands(commands);
        }
    }

    private void StartGuiStream()
    {
        PrintScreen();
    }

    private void PrintScreen()
    {
        PrintHorizontalBorder(true);
        for (int i = 0; i < _windowHeight; ++i)
        {
            Console.Write('*');
            for (int j = 0; j < _windowWidth - 2; ++j)
            {
                Console.Write(' ');
            }
            Console.WriteLine('*');
        }

        PrintHorizontalBorder(false);
    }

    public void PrintHorizontalBorder(bool isTop)
    {
        for (int i = 0; i < _windowWidth; ++i)
        {
            Console.Write('*');
        }
        if(isTop)
            Console.WriteLine();
    }

    private void ProcessCommands(List<Command> commandList)
    {
        foreach (var command in commandList)
        {
            Console.WriteLine(HowManyLinesTextTakes(command._phrase));
        }
    }
    private int HowManyLinesTextTakes(string text)
    {
        return text.Length / _windowWidth;
    }
}