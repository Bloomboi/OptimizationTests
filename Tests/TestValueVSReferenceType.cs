using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace PerfTests
{
    public class TestValueVSReferenceType
    {
        private readonly PerformanceReporter _reporter;

        public TestValueVSReferenceType(PerformanceReporter reporter)
        {
            _reporter = reporter;
        }

        public void Run(int iterations = 100, int numOfTests = 1, bool enabled = false)
        {
            if (enabled)
            {
                Console.WriteLine("\n================ TEST: Value Type vs Reference Type ====================");

                for (int i = 1; i <= numOfTests; ++i)
                {
                    int currentIterations = iterations * i * i;
                    
                    new TestStructVsClass(_reporter).RunTestComparison(currentIterations, "Test Struct Vs. Class", "Struct", "Class");
                }
            }
        }
    }

    public class TestStructVsClass : TestUtility
    {
        public TestStructVsClass(PerformanceReporter reporter) : base(reporter) { }

        public override void TestLogic(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                MyStruct data = new MyStruct { Number = i, Text = $"Item {i}" };
                ProcessData(data);
            }
        }

        public override void TestLogic2(int iterations)
        {
             for (int i = 0; i < iterations; i++)
            {
                MyClass data = new MyClass { Number = i, Text = $"Item {i}" };
                ProcessData(data);
            }
        }

        private struct MyStruct
        {
            public int Number;
            public string Text;
        }

        private sealed class MyClass
        {
            public int Number;
            public string? Text;
        }

        private void ProcessData<T>(T data)
        {
            if (data is MyStruct myStruct)
            {
                myStruct.Number *= 2; 
                var combined = $"{myStruct.Text}-{myStruct.Number}";
            }
            else if (data is MyClass myClass)
            {
               myClass.Number *= 2; 
               var combined = $"{myClass.Text}-{myClass.Number}";
            }
        }
    }
}
