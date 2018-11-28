using System;
using System.Linq; 

class Solution2
{
    public int solution(int[] A)
    {
        var sum = A.Sum();
        if (A.Length > 1)
        {
            var ordered = A.OrderBy(i => i).ToArray();
            sum += add(ordered.Take(ordered.Length - 1).ToArray(), 0);
        }
        return sum;

    }

    private int add(int[] A, int curr)
    {
        if (A.Length == 1)
        {
            var bot = A[0];
            return bot + curr;
        }

        return add(A.Take(A.Length - 1).ToArray(), A.Last()) + curr;
    }
}