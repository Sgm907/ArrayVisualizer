using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ArrayVisualizer
{
    static class Algorithms
    {
        public static ArrayList Unoptomized(int[] startArray)
        {
            int[] array = new int[startArray.Length];
            startArray.CopyTo(array, 0);
            ArrayList instructions = new ArrayList();
            for (int i = 0; i < array.Length; i++)
            {
                int index = i;
                for (int j = i; j < array.Length; j++)
                {
                    if (array[index] > array[j])
                    {
                        index = j;
                    }
                }
                int temp = array[i];
                array[i] = array[index];
                array[index] = temp;
                instructions.Add((i, index));
            }
            return instructions;
        }

        public static int[] Test(int[] array)
        {
            Random rng = new Random();
            array = Enumerable.Range(1, array.Length).OrderBy(x => rng.Next()).ToArray();
            return array;
        }
    }
}
