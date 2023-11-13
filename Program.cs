using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PerfTests
{
    public static class PerfTests
    {
        public const bool enabled = true;
        public const int numOfTests = 1;
        public const int testIter = 1000; 
        public static void Main()
        {
            Console.WriteLine("Program started");
            var serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddTransient<PerformanceReporter>()
            .AddTransient<TestStringBuilder>()
            .AddTransient<TestLinq>()
            .AddTransient<TestBoxing>()
            .AddTransient<TestValueVSReferenceType>()
            .AddTransient<TestForVsForeach>()
            .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger>();
            logger?.LogInformation("Performance Tests Started");

            var testLongstringBuilder = serviceProvider.GetService<TestStringBuilder>();
            var testLINQ = serviceProvider.GetService<TestLinq>();
            var testBoxing = serviceProvider.GetService<TestBoxing>();
            var testValueVSReferenceType = serviceProvider.GetService<TestValueVSReferenceType>();
            var testForVSForeach = serviceProvider.GetService<TestForVsForeach>();
            
            
            try
            {
                testLongstringBuilder?.Run(testIter, numOfTests);
                testLINQ?.Run(testIter, numOfTests);
                testBoxing?.Run(testIter, numOfTests);
                testValueVSReferenceType?.Run(testIter, numOfTests);
                testForVSForeach?.Run(testIter, numOfTests, enabled);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
