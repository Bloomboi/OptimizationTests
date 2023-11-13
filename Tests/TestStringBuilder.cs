using System;
using System.Diagnostics;
using System.Text;

namespace PerfTests
{
    public  class TestStringBuilder
    {
        private readonly PerformanceReporter _reporter;

        public TestStringBuilder(PerformanceReporter reporter)
        {
                _reporter = reporter;
        }
        public void Run(int iterations = 100, int numOfTests = 1, bool enabled = false)
        {
            if(enabled)
            {
                Console.WriteLine("============ TEST: StringBuilder ================");
                for(int i = 1; i <= numOfTests; ++i)
                {
                
                    int currentIterations = iterations * i;
                    new TestBuilderVsFormat(_reporter).RunTestComparison(currentIterations, "Test String builder vs String Formatter", "StringBuilder", "String Formatter");
                }
            }
        }
    }

    public class TestBuilderVsFormat : TestUtility
    {

        public TestBuilderVsFormat(PerformanceReporter reporter) : base(reporter) {}

        public override void TestLogic(int iterations)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iterations; i++)
            {
                sb.Append("x");
            }
        }

        public override void TestLogic2(int iterations)
        {
            string testString = "";
            for (int i = 0; i < iterations; i++)
            {
                testString += "x";
            }
        }

    }
}
