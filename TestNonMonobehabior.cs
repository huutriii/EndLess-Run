namespace Assets._02_SCRIPTS
{
    internal class TestNonMonobehabior
    {
        static void Main(string[] args)
        {
            int sum = 0;
            int n = 60;
            while (n-- > 0)
            {
                sum += 1;
            }
            System.Console.WriteLine(sum);
        }
    }
}
