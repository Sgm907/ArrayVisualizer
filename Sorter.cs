using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ArrayVisualizer
{
    class Sorter
    {
        Func<int[], ArrayList> algorithm;
        Random rng;

        public Sorter(int[] array)
        {
            rng = new Random();
            algorithm = Algorithms.Unoptomized;
            //targetArray = Enumerable.Range(1, size).ToArray();
        }

        /// <summary>
        /// Sorts the array using the set algorithm
        /// </summary>
        /// <returns>Returns true when the sort succeeds</returns>
        public ArrayList Sort(int[] targetArray)
        {
            return algorithm.Invoke(targetArray);
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
