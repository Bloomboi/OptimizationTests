
namespace PerfTests
{
    public interface ITest
    {
        void RunTest(int iterations, string testName);
        void RunTestComparison(int iterations, string testName, string metricName1, string metricName2);
        void Setup(int iterations);
        void TestLogic(int iterations);
        void TestLogic2(int iterations);
    }
}
