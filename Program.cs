using System.Numerics;

namespace Lab8
{
    class Program
    {
        static void Main()
        {
            string? filePath = "";
            FileReader? file = null;
            
            while(filePath == "")
            {
                InitiateFileReader(ref filePath, ref file);
            }
            
            CommandManager commandManager = new CommandManager(file.FileContent);
            BankAccount bankAccount = new BankAccount(commandManager.CommandList);

            string time = "";
            while (time != "q")
            {
                Console.WriteLine("Enter date time or 'q' to exit.");
                time = Console.ReadLine();
                CheckBalanceAtTime(bankAccount, time);
            }
            
        }

        private static void InitiateFileReader(ref string? filePath, ref FileReader? file)
        {
            Console.WriteLine("Enter path to a file.");
            filePath = Console.ReadLine();

            try
            {
                file = new FileReader(filePath);
            }
            catch (NullReferenceException ex)
            {
                filePath = new string ("");
            }
        }

        private static void CheckBalanceAtTime(BankAccount bankAccount, string? time)
        {
            try
            {
                BigInteger balance =  bankAccount.CheckForErrorsFindBalanceAtTime(DateTime.ParseExact(time, "yyyy-MM-dd hh:mm",
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