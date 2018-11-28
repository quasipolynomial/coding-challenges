using System;
using System.Collections.Generic;
using System.Linq; 

class Solution1
{

    // My code to get process
    public int solution(int[] A, int M)
    {
        var rangeArray = Enumerable.Range(0, A.Length).ToArray();
        var maxMAligned = 0;
        for (var i = 1; i <= A.Length; i++)
        {
            var rangeCombinations = GetKCombs(rangeArray, i);
            foreach (var c in rangeCombinations)
            {
                var allTrue = true;
                var rangeCombination = c.ToArray();
                foreach (var val1 in rangeCombination)
                { 
                    foreach (var val2 in rangeCombination)
                    { 
                        if (Math.Abs(A[val1] - A[val2]) % M != 0)
                        {
                            allTrue = false;
                            break;
                        }
                    }
                }

                if (allTrue)
                {
                    maxMAligned = i > maxMAligned ? i : maxMAligned;
                }
            } 
        }

        return maxMAligned;
    }

    // Copied code to get combinations 
    // REF. Stack overflow
    public IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
    {
        if (length == 1) return list.Select(t => new T[] { t });
        var combs = GetKCombs(list, length - 1); 
        return combs.SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0), (t1, t2) => t1.Concat(new T[] { t2 })); ;
    }
}