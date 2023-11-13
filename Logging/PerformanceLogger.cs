using System;
using System.Diagnostics;

namespace PerfTests
{
    public  class PerformanceLogger
    {
        public  void ReportPerformance(
        string testDescription,
        long firstOperationTicks,
        long secondOperationTicks,
        int iterations,
        string firstOperationName = "First Operation",
        string secondOperationName = "Second Operation")
        {
            double firstOperationMilliseconds = 
            (double)firstOperationTicks / Stopwatch.Frequency * 1000;
            double secondOperationMilliseconds = 
            (double)secondOperationTicks / Stopwatch.Frequency * 1000;

            Console.WriteLine($"{testDescription} - {firstOperationName} " 
            + "Time: {firstOperationMilliseconds} ms, {secondOperationName}"
            + "Time: {secondOperationMilliseconds} ms over {iterations} iterations.");

            if (firstOperationMilliseconds != 0 && secondOperationMilliseconds != 0)
            {
                double ratio;
                string fasterOrSlower;

                if (firstOperationMilliseconds < secondOperationMilliseconds)
                {
                    ratio = secondOperationMilliseconds / firstOperationMilliseconds;
                    fasterOrSlower = $"{secondOperationName} is slower";
                }
                else
                {
                    ratio = firstOperationMilliseconds / secondOperationMilliseconds;
                    fasterOrSlower = $"{firstOperationName} is slower";
                }

                Console.WriteLine($"{fasterOrSlower} than {secondOperationName}. {firstOperationName} is {ratio:F2} times faster.\n---\n");
            }
        }
    }
}
