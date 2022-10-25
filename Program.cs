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
                CommandManager commandManager = new CommandManager(file.FileContent);
                BankAccount bankAccount = new BankAccount(commandManager.CommandList);

                string time = "";
                while (time != "q")
                {
                    Console.WriteLine("Enter date time");
                    time = Console.ReadLine();
                    CheckBalanceAtTime(bankAccount, time);
                }
            }

            Console.ReadKey();
        }

        private static void CheckBalanceAtTime(BankAccount bankAccount, string? time)
        {
            try
            {
                BigInteger balance =  bankAccount.CheckBalanceAtTime(DateTime.ParseExact(time, "yyyy-MM-dd hh:mm",
                    System.Globalization.CultureInfo.InvariantCulture));
                Console.WriteLine(balance.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wrong date.");
            }
        }
    }
}