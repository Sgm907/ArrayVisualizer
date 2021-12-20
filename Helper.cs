using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace ArrayVisualizer
{
    static class Helper
    {
        //control variables;
        public static readonly float desiredScreenWPercent = 0.85f;//% of window width the visual should take up
        public static readonly float desiredScreenHPercent = 0.8f;//% of window width the visual should take up

        /// <summary>
        /// Stores a method name to be called later in the program
        /// </summary>
        /// <typeparam name="T">The return type of the Method</typeparam>
        /// <param name="func">the Function name</param>
        /// <returns>Returns a new variable that can be called later</returns>
        public static Func<object> MethodStorage<T>(Func<T> func)
        {
            return () => func();
        }

        /// <summary>
        /// Blocks the thread for a set time
        /// </summary>
        /// <param name="duration">How long to block the thread, in seconds</param>
        public static void BlockThread(double duration)
        {
            var durationTicks = Math.Round(duration * Stopwatch.Frequency);
            var sw = Stopwatch.StartNew();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        }
    }
}
