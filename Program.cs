namespace Lab8
{
    class Program
    {
        private static void Main()
        {
            var list = new List<string>() { "code", "setting", "doce", "ecod", "framer", "frame", "setting", "testing" };
            
            IterateList(list);
            PrintList(list);
        }

        
        // Iterating through list and making every possible pair
        public static void IterateList(List<string> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                RemoveOtherElementsIfAnagram(list, i);
            }
        }

        // If second word in pair is anagram, deletes it
        private static void RemoveOtherElementsIfAnagram(List<string> list, int i)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (IsAnagram(list[i], list[j]))
                {
                    list.RemoveAt(j);
                    j -= 1;
                }
            }
        }

        public static void PrintList(List<string> list)
        {
            Console.WriteLine(String.Join(", ", list));
        }

        public static bool IsAnagram(string left, string right)
        {
            List<char> leftList = new List<char>(left.ToArray());
            List<char> rightList = new List<char>(right.ToArray());

            leftList.Sort();
            rightList.Sort();
            
            // Are the same length, and are letters equal
            return Enumerable.SequenceEqual(leftList, rightList);
        }
    }
}