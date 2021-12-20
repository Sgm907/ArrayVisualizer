using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ArrayVisualizer
{
    class Sorter
    {
        private Func<int[], ArrayList> algorithm;
        private Random rng;
        private Stopwatch sortTimer;

        public Sorter(int[] array)
        {
            rng = new Random();
            algorithm = Algorithms.SelectionSort;
            sortTimer = new Stopwatch();
        }

        /// <summary>
        /// Sorts the array using the set algorithm
        /// </summary>
        /// <returns>Returns true when the sort succeeds</returns>
        public ArrayList Sort(int[] targetArray, ref double sortTime)
        {
            sortTimer.Restart();
            ArrayList value = algorithm.Invoke(targetArray);
            sortTimer.Stop();
            sortTime = sortTimer.Elapsed.TotalMilliseconds;
            return value;
        }


        /// <summary>
        /// Scrambles the array so it is no longer sorted
        /// </summary>
        /// <returns>Returns true when the scramble succeeds</returns>
        public bool Scramble(ref int[] array)
        {
            array = Enumerable.Range(1, array.Length).OrderBy(x => rng.Next()).ToArray();
            return true;
        }
        
        public bool SetAlgorithm(Func<int[], ArrayList> function)
        {
            algorithm = function;
            return true;
        }
        
    }
}
