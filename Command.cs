using System.Runtime.InteropServices;

namespace Lab8;

public enum Pos
{
    Left,
    Right,
    Top,
    Bottom
}

public enum Color
{
    Red,
    Green,
    Blue,
    White
}
public class Command
{
    public TimeRange _timeOfRendering;
    public Pos _position;
    public Color _colour;
    public bool _isPosColorDefault;
    public string _phrase;

    public Command(string line)
    {
        string[] details = line.Split(new char[] {' ', ',', '[', ']', '-'}, 
            StringSplitOptions.RemoveEmptyEntries);
        
        _timeOfRendering = new TimeRange(details[0], details[1]);
        Tuple<Pos, Color> posColor = SetPosColor(line, details);
        _position = posColor.Item1;
        _colour = posColor.Item2;
        
        SetCommandPhrase(line);
    }

    private void SetCommandPhrase(string line)
    {
        if (_isPosColorDefault)
            _phrase = line.Substring(14, line.Length - 14);
        else
            _phrase = line.Substring(line.IndexOf(']') + 2, line.Length - line.IndexOf(']') - 2);
    }

    public Tuple<Pos, Color> SetPosColor(string line, string[] details)
    {
        const int firstSquareBracketIdx = 14;
        const int secondSquareBracketMaxIdx = 28;
        int fIdx = line.IndexOf('[');
        int sIdx = line.IndexOf(']');
        Tuple<Pos, Color> pair;
        
        _isPosColorDefault = DoPosAndColorMatchPosition(firstSquareBracketIdx, 
            fIdx, sIdx, secondSquareBracketMaxIdx);
        pair = CreateTuplePosColor(details);

        return pair;
    }

    private Tuple<Pos, Color> CreateTuplePosColor(string[] details)
    {
        Tuple<Pos, Color> pair;
        try
        {
            pair = _isPosColorDefault
                ? Tuple.Create(Pos.Bottom, Color.White)
                : Tuple.Create(Enum.Parse<Pos>(details[2]), Enum.Parse<Color>(details[3]));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} says {ex.Message}. Setting command's color ans position to default.");
            pair = Tuple.Create(Pos.Bottom, Color.White);
        }

        return pair;
    }

    private static bool DoPosAndColorMatchPosition(int firstSquareBracketIdx, 
        int fIdx, int sIdx, int secondSquareBracketMaxIdx)
    {
        return !(firstSquareBracketIdx == fIdx &&
                 sIdx > firstSquareBracketIdx &&
                 sIdx <= secondSquareBracketMaxIdx);
    }

    public void GetInf()
    {
        Console.WriteLine($"{_timeOfRendering.GetRange()} [{_position}, {_colour}] {_phrase}");
    }

    public class TimeRange
    {
        public DateTime Start;
        public DateTime End;

        public TimeRange(string start, string end)
        {
            Start = DateTime.ParseExact(start, "mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
            
            End = DateTime.ParseExact(end, "mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        public string GetRange()
        {
            return Start.ToString("mm:ss") + " - " + End.ToString("mm:ss");
        }
    }
}