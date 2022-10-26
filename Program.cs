using System.Net.Mime;

namespace Lab8
{
    static class Program
    {
        public static void Main()
        {
            string? filePath = "";
            FileReader? file = null;
            
            while (filePath == "")
            {
                InitiateFileReader(ref filePath, ref file);
            }
            
            var cmh = InitiateGuiPrinter(file, out var gui);

            DateTime startTime = cmh.DCommands.Keys.First();
            DateTime endTime = cmh.DCommands.Keys.Last();
            
            // Iterates through time range of file commands
            for (var time = startTime; time <= endTime; time += System.TimeSpan.FromSeconds(1))
            {
                gui.ClearConsole();
                gui.Draw(cmh.DCommands[time]);
                Thread.Sleep(System.TimeSpan.FromSeconds(1));
            }
        }

        // Creates Gui and CommandHandler. Also checks for wrong input file.
        private static CommandHandler InitiateGuiPrinter(FileReader? file, out Gui gui)
        {
            CommandHandler cmh = new CommandHandler(file.FileContent);
            gui = new Gui('*', '$', cmh.LargestPhraseLength());

            // If file is empty or so
            if (cmh.DCommands.Count == 0)
            {
                Console.WriteLine("Wrong input file, or no commands.");
                System.Environment.Exit(-1);
            }

            return cmh;
        }

        // Awaits until good file path.
        private static void InitiateFileReader(ref string? filePath, ref FileReader? file)
        {
            Console.WriteLine("Enter path to a file.");
            filePath = Console.ReadLine();

            try
            {
                file = new FileReader(filePath);
                filePath = file.FilePath;
            }
            catch (NullReferenceException ex)
            {
                filePath = new string ("");
            }
        }
    }
}