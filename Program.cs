using System.Numerics;

namespace Lab8
{
    class Program
    {
        static void Main()
        {
            FileReader goodFile = new FileReader(@"C:\Users\Administrator\RiderProjects\Lab8\NewFile2.txt");
            FileReader badFile = new FileReader(@"C:\Users\Administrator\RiderProjects\Lab8\FalseFileTask2.txt");

            FileReader[] fileArr = new FileReader[2];
            fileArr[0] = badFile;
            fileArr[1] = goodFile;

            foreach (var file in fileArr)
            {
                file.PrintContent();
            }

            CommandManager commandManager = new CommandManager(goodFile.FileContent);
            BankAccount bankAccount = new BankAccount(commandManager.CommandList);

            Console.ReadKey();
        }
    }
}