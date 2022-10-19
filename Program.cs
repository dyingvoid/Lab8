namespace Lab8
{
    static class Program
    {
        public static void Main()
        {
            FileReader file = new FileReader(@"C:\Users\Administrator\RiderProjects\Lab8\NewFile.txt");
            CommandHandler cmh = new CommandHandler(file.FileContent);
            //Gui gui = new Gui(100, 26);
        }
    }
}