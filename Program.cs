namespace Lab8
{
    static class Program
    {
        public static void Main()
        {
            FileReader file = new FileReader(@"C:\Users\Administrator\RiderProjects\Lab8\NewFile.txt");
            Gui gui = new Gui(50, 50, file.FileContent);
        }

        public static string[] SubArray(string[] arr, int start, int end)
        {
            int size = end - start + 1;
            string[] subArr = new string[size];
            for (int i = 0; i < size; i++)
            {
                subArr[i] = arr[i + start];
            }

            return subArr;
        }
    }
}