namespace EqualDivisors;

public static class DivisorSolver
{
    public static int CountDivisors(int n)
    {
        int count = 0;
        for (int i = 1; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                count += (i == n / i) ? 1 : 2;
            }
        }
        return count;
    }

    public static int CountValidPairs(int k)
    {
        int count = 0;
        for (int n = 2; n < k; n++)
        {
            if (CountDivisors(n) == CountDivisors(n + 1))
                count++;
        }
        return count;
    }
}
