namespace Lab8
{
    static class Program
    {
        public static void Main()
        {
            FileReader file = new FileReader(@"C:\Users\Administrator\RiderProjects\Lab8\NewFile.txt");
            CommandHandler cmh = new CommandHandler(file.FileContent);
            Gui gui = new Gui('*', '$', cmh.LargestPhraseLength());

            DateTime startTime = cmh.DCommands.Keys.First();
            DateTime endTime = cmh.DCommands.Keys.Last();
            
            for (var time = startTime; time <= endTime; time += System.TimeSpan.FromSeconds(1))
            {
                gui.ClearConsole();
                gui.Draw(cmh.DCommands[time]);
                Thread.Sleep(System.TimeSpan.FromSeconds(1));
            }
        }
    }
}