using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PerfTests
{
    public abstract class TestUtility : ITest
    {
        protected readonly PerformanceReporter _reporter;

        protected TestUtility(PerformanceReporter reporter)
        {
            _reporter = reporter;
        }

        public virtual void Setup(int iterations)
        {
            // Default is empty
        }

        public abstract void TestLogic(int iterations);
        public abstract void TestLogic2(int iterations);

        public void RunTest(int iterations, string testName)
        {
            CommonLogicMeasure(() => TestLogic(iterations), iterations, testName);
        }

        public void RunTestComparison(int iterations, string testName, string metricName1, string metricName2)
        {
            CommonLogicCompare(() => TestLogic(iterations), () => TestLogic2(iterations), iterations, testName, metricName1, metricName2);
        }

        protected void CommonLogicMeasure(Action testAction, int iterations, string testName)
        {
            Stopwatch stopwatch = new Stopwatch();
            Setup(iterations);
            stopwatch.Start();
            testAction();
            stopwatch.Stop();
            _reporter.Report(testName, stopwatch.ElapsedTicks, iterations, testName);
        }

        protected void CommonLogicCompare(Action testAction1, Action testAction2, int iterations, string testName, string metricName1, string metricName2)
        {
            Stopwatch stopwatch = new Stopwatch();
            Setup(iterations);

            stopwatch.Start();
            testAction1();
            stopwatch.Stop();
            long metricTicks1 = stopwatch.ElapsedTicks;

            stopwatch.Reset();

            stopwatch.Start();
            testAction2();
            stopwatch.Stop();
            long metricTicks2 = stopwatch.ElapsedTicks;

            _reporter.Compare(testName, metricTicks1, metricTicks2, iterations, metricName1, metricName2);
        }
    }
}
