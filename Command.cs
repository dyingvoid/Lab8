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
    Blue
}
public class Command
{
    public TimeRange TimeOfRendering { get; }
    public string Phrase { get; }
    public Pos Position { get; }
    public Color Colour { get; }

    public Command(string line)
    {
        string[] details = line.Split(' ');
        TimeOfRendering = new TimeRange(details[0], details[2]);
    }

    public class TimeRange
    {
        public DateTime Start;
        public DateTime End;

        public TimeRange(string start, string end)
        {
            Start = DateTime.ParseExact(start, "mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
            End = DateTime.ParseExact(start, "mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}