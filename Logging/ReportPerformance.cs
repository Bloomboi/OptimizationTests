using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace PerfTests
{
    public class PerformanceReporter
    {
        public static readonly double StopwatchConst = Stopwatch.Frequency * 1000;
        private readonly ILogger<PerformanceReporter> _logger;

        public PerformanceReporter(ILogger<PerformanceReporter> logger)
        {
            _logger = logger;
        }

        public void Compare(string testDescription,
        long firstMetric,
        long secondMetric,
        int iterations,
        string metricName1 = "Metric1",
        string metricName2 = "Metric2")
        {
   
            double firstMilliseconds = 
            (double)firstMetric / StopwatchConst;
            double secondMilliseconds = 
            (double)secondMetric / StopwatchConst;

            _logger.LogInformation($"{testDescription} - {metricName1}: "
            + $"{firstMilliseconds} ms, {metricName2}: "
            + $"{secondMilliseconds} ms over {iterations} iterations.");

            if (firstMilliseconds != 0 && secondMilliseconds != 0)
            {
                double ratio;
                string fasterOrSlower;

                if (firstMilliseconds < secondMilliseconds)
                {
                    ratio = secondMilliseconds / firstMilliseconds;
                    fasterOrSlower = "slower";
                }
                else
                {
                    ratio = firstMilliseconds / secondMilliseconds;
                    fasterOrSlower = "faster";
                }

                _logger.LogInformation($"{metricName2} is {ratio:F2} "
                + $"times {fasterOrSlower} than {metricName1}.\n---\n");
            }
        }

        public void Report(string testDescription,
        long Metric,
        int iterations,
        string metricName = "Metric")
        {
            double Milliseconds = (double)Metric / StopwatchConst;

            _logger.LogInformation($"{testDescription}: {Milliseconds} "
            + $"ms over {iterations} iterations.\n");
        }
    }
}
