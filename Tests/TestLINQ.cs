using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Microsoft.VisualBasic;

namespace PerfTests
{
    public class TestLinq
    {
        private readonly PerformanceReporter _reporter;

        public TestLinq(PerformanceReporter reporter)
        {
                _reporter = reporter;
        }
        public void Run(int iterations = 100, int numOfTests = 1, bool enabled = false)
        {
            if(enabled)
            {
                Console.WriteLine("\n================ TEST: LINQ ====================");
                for(int i = 1; i <= numOfTests; ++i)
                {
                    int currentIterations = iterations * i * i;
                    new TestIntegers(_reporter).RunTestComparison(currentIterations, "Test Integers", "LINQ", "Loop");
                    new TestDictionary(_reporter).RunTestComparison(currentIterations, "Test Dictionary", "LINQ", "Loop");
                    new TestCustomObjects(_reporter).RunTestComparison(currentIterations, "Test Custom Objects", "LINQ", "Loop");
                }
            }
        }
    }

    public class TestIntegers : TestUtility
    {
        public TestIntegers(PerformanceReporter reporter) : base(reporter) { }

        public override void TestLogic(int iterations)
        {
            var numbers = Enumerable.Range(1, iterations).ToList();
            BigInteger sumWithLinq = numbers
            .Where(n => n % 2 == 0)
            .Aggregate(new BigInteger(0), (acc, n) => acc + n);
        }

        public override void TestLogic2(int iterations)
        {
            var numbers = Enumerable.Range(1, iterations).ToList();
            BigInteger sumWithLoop = new BigInteger(0);
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    sumWithLoop += numbers[i];
                }
            }
        }
    }

    public class TestDictionary : TestUtility
    {
        private readonly Dictionary<int, string> _dictionary = new Dictionary<int, string>();

        public TestDictionary(PerformanceReporter reporter) : base(reporter) { }

        public override void Setup(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                _dictionary.Add(i, "Value" + i);
            }
        }
        public override void TestLogic(int iterations)
        {
            var foundWithLinq = _dictionary.Where(kvp => kvp.Key % 2 == 0).ToList();
        }

        public override void TestLogic2(int iterations)
        {
            var foundWithLoop = new List<KeyValuePair<int, string>>();
            foreach (var kvp in _dictionary)
            {
                if (kvp.Key % 2 == 0)
                {
                    foundWithLoop.Add(kvp);
                }
            }
        }

    }

    public class TestCustomObjects : TestUtility
    {
        private List<CustomObject> customObjects; // Changed to a list of CustomObject

        public TestCustomObjects(PerformanceReporter reporter) : base(reporter) 
        {
            customObjects = new List<CustomObject>(); // Initialize the list
        }

        public override void Setup(int iterations)
        {
            customObjects = Enumerable.Range(1, iterations)
                                    .Select(i => new CustomObject { Id = i, Name = "Name" + i })
                                    .ToList();
        }

        public override void TestLogic(int iterations)
        {
            var filteredWithLinq = customObjects
                .Where(obj => obj.Id % 2 == 0)
                .ToList();
        }

        public override void TestLogic2(int iterations)
        {
            var filteredWithLoop = new List<CustomObject>();
            foreach (var obj in customObjects)
            {
                if (obj.Id % 2 == 0)
                {
                    filteredWithLoop.Add(obj);
                }
            }
        }

        private sealed class CustomObject
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
    }

}
