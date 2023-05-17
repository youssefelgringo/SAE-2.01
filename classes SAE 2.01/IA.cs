using System;
using System.Collections.Generic;
using System.Linq;

namespace Power4
{
    class AI
    {
        public class Power4AI
        {
            public static int MinMax(int depth, bool evalMax)
            {
                if (depth == 0 || IsGameOver())
                {
                    return GetHeuristicValue();
                }

                int bestValue = evalMax ? int.MinValue : int.MaxValue;
                foreach (var move in GetPossibleMoves())
                {
                    int childValue = MinMax(depth - 1, !evalMax);
                    if (evalMax)
                    {
                        bestValue = Math.Max(bestValue, childValue);
                    }
                    else
                    {
                        bestValue = Math.Min(bestValue, childValue);
                    }
                }

                return bestValue;
            }
        }
    }
}