using Practice.Domain;

namespace Practice.Launcher.App
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var test1 = new List<int>() { 1, 2 };
                var test2 = new List<int>() { 1, 2, 3 };
                var test3 = new List<int>() { 1, 2, 3, 4 };

                Console.WriteLine("[" + string.Join(", ",
                    test2.GetCombinations(2, CollectionsComparer<int>.Instance)
                        .Select(item => "[" + string.Join(", ", item) + "]")) + "]");
                Console.WriteLine("[" + string.Join(", ",
                    test2.GetPermutations(test2.Count, CollectionsComparer<int>.Instance)
                        .Select(item => "[" + string.Join(", ", item) + "]")) + "]");
                Console.WriteLine("[" + string.Join(", ",
                    test2.GetSubset(CollectionsComparer<int>.Instance)
                        .Select(item => "[" + string.Join(", ", item) + "]")) + "]");

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}