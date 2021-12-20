using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ArrayVisualizer
{
    /*Every algorithm here will follow the same process:
        1) Copy the array as to not actually sort the array being used for the visualization
        2) Perform the Sort
        3) Create a list of tuples, detailing which 2 indices in the array should be swapped to perform the sort
        4) Return the instruction list for the visualizer to slow down and display
     */
    static class Algorithms
    {
        /// <summary>
        /// A sorting algorithm which constantly iterates through the array looking for the next smallest value
        /// </summary>
        /// <param name="startArray">The array to be sorted</param>
        /// <returns>The instructions for the visualizer</returns>
        public static ArrayList SelectionSort(int[] startArray)
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

        /// <summary>
        /// A sorting algorithm which splits the array into a sorted and unsorted section, placing the next unsorted value into the correct spot in the sorted section
        /// </summary>
        /// <param name="startArray">The array to be sorted</param>
        /// <returns>The instructions for the visualizer</returns>
        public static ArrayList InsertSort(int[] startArray)
        {
            int[] array = new int[startArray.Length];
            startArray.CopyTo(array, 0);
            ArrayList instructions = new ArrayList();
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    instructions.Add((j + 1, j));
                    j--;
                }
                array[j + 1] = key;
            }
            return instructions;
        }

        /// <summary>
        /// A sorting algorithm which switches two adjacent values, so long as the next value is lower than the current one
        /// </summary>
        /// <param name="startArray">The array to be sorted</param>
        /// <returns>The instructions for the visualizer</returns>
        public static ArrayList BubbleSort(int[] startArray)
        {
            int[] array = new int[startArray.Length];
            startArray.CopyTo(array, 0);
            ArrayList instructions = new ArrayList();
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        instructions.Add((j + 1, j));
                    }
                }
            }
            return instructions;
        }
    }
}
