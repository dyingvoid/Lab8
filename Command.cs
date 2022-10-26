using System.Runtime.InteropServices;

namespace Lab8;

public enum Pos
{
    Left,
    Right,
    Top,
    Bottom
}

public class Command
{
    public TimeRange TimeOfRendering;
    public Pos Position;
    public ConsoleColor Colour;
    public bool IsPositionAndColorDefault;
    public string Phrase;
    
    public Command(string line)
    {
        string[] details = line.Split(new char[] {' ', ',', '[', ']', '-'}, 
            StringSplitOptions.RemoveEmptyEntries);
        
        TimeOfRendering = new TimeRange(details[0], details[1]);
        Tuple<Pos, ConsoleColor> posColor = SetPositionAndColor(line, details);
        Position = posColor.Item1;
        Colour = posColor.Item2;
        
        SetCommandPhrase(line);
    }

    private void SetCommandPhrase(string line)
    {
        if (IsPositionAndColorDefault)
            Phrase = line.Substring(14, line.Length - 14);
        else
            Phrase = line.Substring(line.IndexOf(']') + 2, line.Length - line.IndexOf(']') - 2);
    }

    /// <summary>
    /// Setter with roughs checks for right format
    /// </summary>
    /// <param name="line"></param>
    /// <param name="details"></param>
    /// <returns>
    /// Tuple(Position, ConsoleColor), if line does not have information,
    /// or bad format - returns Tuple(Pos.Bottom, ConsoleColor.White)
    /// </returns>
    public Tuple<Pos, ConsoleColor> SetPositionAndColor(string line, string[] details)
    {
        const int firstSquareBracketIdx = 14;
        const int secondSquareBracketMaxIdx = 28;
        int fIdx = line.IndexOf('[');
        int sIdx = line.IndexOf(']');
        Tuple<Pos, ConsoleColor> pair;
        
        IsPositionAndColorDefault = ArePositionAndColorInRightPlace(firstSquareBracketIdx, 
            fIdx, sIdx, secondSquareBracketMaxIdx);
        pair = CreateTuplePosColor(details);

        return pair;
    }

    /// <summary>
    /// Creates Tuple(Pos, ConsoleColor) based on IsPositionAndColorDefault
    /// </summary>
    /// <param name="details"></param>
    /// <returns>
    /// for true IsPositionAndColorDefault return Tuple(Pos, ConsoleColor) from details,
    /// for false and bad input returns Tuple(Pos.Bottom, ConsoleColor.White)
    /// </returns>
    private Tuple<Pos, ConsoleColor> CreateTuplePosColor(string[] details)
    {
        Tuple<Pos, ConsoleColor> pair;
        try
        {
            pair = IsPositionAndColorDefault
                ? Tuple.Create(Pos.Bottom, ConsoleColor.White)
                : Tuple.Create(Enum.Parse<Pos>(details[2]), Enum.Parse<ConsoleColor>(details[3]));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Source} says {ex.Message}. Setting command's color ans position to default.");
            pair = Tuple.Create(Pos.Bottom, ConsoleColor.White);
        }

        return pair;
    }

    /// <summary>
    /// Check used by SetPositionAndColor(), compares indexes of square brackets
    /// to be in right place
    /// </summary>
    /// <param name="firstSquareBracketIdx"></param>
    /// <param name="fIdx"></param>
    /// <param name="sIdx"></param>
    /// <param name="secondSquareBracketMaxIdx"></param>
    /// <returns>false for right place, true for bad place</returns>
    private static bool ArePositionAndColorInRightPlace(int firstSquareBracketIdx, 
        int fIdx, int sIdx, int secondSquareBracketMaxIdx)
    {
        return !(firstSquareBracketIdx == fIdx &&
                 sIdx > firstSquareBracketIdx &&
                 sIdx <= secondSquareBracketMaxIdx);
    }

    public void GetInf()
    {
        Console.WriteLine($"{TimeOfRendering.GetRange()} [{Position}, {Colour}] {Phrase}");
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