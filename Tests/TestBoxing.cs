using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace PerfTests
{
    public class TestBoxing
    {
        private readonly PerformanceReporter _reporter;

        public TestBoxing(PerformanceReporter reporter)
        {
            _reporter = reporter;
        }

        public void Run(int iterations = 100, int numOfTests = 1, bool enabled = false)
        {
            if (enabled)
            {
                Console.WriteLine("\n================ TEST: Boxing Object ====================");

                for (int i = 1; i <= numOfTests; ++i)
                {
                    int currentIterations = iterations * i * i;

                    new TestIntBoxing(_reporter).RunTestComparison(currentIterations, "Test Int Boxing", "Boxed", "Unboxing"); 
                    new TestStringBoxing(_reporter).RunTestComparison(currentIterations, "Test String Boxing", "Boxed", "Unboxing"); 
                    new TestObjectBoxing(_reporter).RunTestComparison(currentIterations, "Test Object Boxing", "Int", "Object"); 
                }
            }
        }
    }
    
    public class TestIntBoxing : TestUtility
    {
        
        public TestIntBoxing(PerformanceReporter reporter) : base(reporter){ }

        public override void TestLogic(int iterations)
        {
            var numbers = Enumerable.Range(1, iterations).ToList();
            int sumUnboxed = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sumUnboxed += numbers[i];
            }
        }

        public override void TestLogic2(int iterations)
        {
            var numbers = Enumerable.Range(1, iterations).ToList();
            int sumBoxed = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                object boxedNumber = numbers[i]; // Boxing
                sumBoxed += (int)boxedNumber;    // Unboxing
            }
        
        }
    }

    public class TestStringBoxing : TestUtility
    {
        public TestStringBoxing(PerformanceReporter reporter) : base(reporter){ }

        public override void TestLogic(int iterations)
        {
            var strings = Enumerable.Range(1, iterations).Select(x => x.ToString()).ToList();
            string concatenated = string.Empty;
            for (int i = 0; i < strings.Count; i++)
            {
                concatenated += strings[i]; 
            }
        }

        public override void TestLogic2(int iterations)
        {
            var strings = Enumerable.Range(1, iterations).Select(x => x.ToString()).ToList();
            string concatenated = string.Empty;
            for (int i = 0; i < strings.Count; i++)
            {
                object boxedString = strings[i]; // Boxing
                concatenated += (string)boxedString; // Unboxing
            }
        }
    }

    public class TestObjectBoxing : TestUtility
    {
        public TestObjectBoxing(PerformanceReporter reporter) : base(reporter){ }

        public override void TestLogic(int iterations)
        {
            var numbers = Enumerable.Range(1, iterations).ToList(); // Non-boxed integers
            int sumNonBoxed = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                sumNonBoxed += numbers[i];
            }
        }

        public override void TestLogic2(int iterations)
        {
            var objects = Enumerable.Range(1, iterations).Select(x => (object)x).ToList();
            int sumBoxed = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                sumBoxed += (int)objects[i]; // Unboxing
            }
        }
    }
}
