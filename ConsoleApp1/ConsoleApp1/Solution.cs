using System;
using System.Collections.Generic;

class Solution
{
    private int _shipCount = 0;
    private int _hitCount = 0;
    public string solution(int N, string S, string T)
    {

        var map = BuildMap(N);
        map = PopulateShips(map, S);
        map = PopulateHits(map, T); 
        var sunk = GetSunk(map, N);
        return $"{sunk},{_hitCount}";
    }

    private int GetSunk(int[,] map, int N)
    {
        var sunkCount = 0;
        for (var i = 2; i < 2 + _shipCount; i++)
        {
            var sunk = true;
            for (var j = 0; j < N; j++)
            {
                for (var k = 0; k < N; k++)
                {
                    if (map[j, k] == i)
                    {
                        sunk = false;
                        break;
                    }
                }
            }

            if (sunk)
            {
                sunkCount += 1;
            }
        }
        return sunkCount;
    }

    private int[,] BuildMap(int N)
    {
        var map = new int[N, N];

        for (var i = 0; i != N; i++)
        {
            for (var j = 0; j != N; j++)
            {
                map[i, j] = 0;
            }
        }
        return map;
    }

    private int[,] PopulateShips(int[,] map, string s)
    {
        var ships = s.Split(",");
        var shipNumber = 2;

        foreach (var ship in ships)
        {
            _shipCount += 1;
            var tlx = int.Parse(ship[0].ToString());
            var tly = GetYValue(ship[1].ToString());
            var brx = int.Parse(ship[3].ToString());
            var bry = GetYValue(ship[4].ToString());

            if (tlx == brx && tly == bry)
            {
                // Single dotted ship
                map[tlx, tly] = shipNumber;
            }
            else if (tlx != brx && tly != bry)
            {
                // Square ship
                map[tlx, tly] = shipNumber;
                map[tlx, tly + 1] = shipNumber;
                map[tlx + 1, tly] = shipNumber;
                map[tlx + 1, tly + 1] = shipNumber;
            }
            else
            {
                // Linear ship
                for (var x = brx; x <= tlx; x++)
                {
                    for (var y = tly; y <= bry; y++)
                    {
                        map[x, y] = shipNumber;
                    }
                }
            }

            shipNumber += 1;
        }
        return map;
    }

    private int[,] PopulateHits(int[,] map, string t)
    {
        List<int> hits = new List<int>();
        var coords = t.Split(" ");
        foreach (var coord in coords)
        { 
            var x = int.Parse(coord[0].ToString());
            var y = GetYValue(coord[1].ToString());
            if (map[x, y] != 0 && !hits.Contains(map[x, y]))
            {
                hits.Add(map[x, y]);
            }
            map[x, y] = 1;
        }

        _hitCount = hits.Count;
        return map;
    }

    private int GetYValue(string y)
    {
        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return alphabet.IndexOf(y.ToUpper(), StringComparison.Ordinal);
    }
     
}