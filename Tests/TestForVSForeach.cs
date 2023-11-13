using System;
using System.Collections;
using System.Diagnostics;

namespace PerfTests
{
    public class TestForVsForeach
    {
        private readonly PerformanceReporter _reporter;

        public TestForVsForeach(PerformanceReporter reporter)
        {
            _reporter = reporter;
        }

        public void Run(int iterations = 100, int numOfTests = 1, bool enabled = false)
        {
            if (enabled)
            {
                Console.WriteLine("============ TEST: For vs Foreach ================");
                for (int i = 1; i <= numOfTests; ++i)
                {
                    int currentIterations = iterations * i;
                   
                    new TestGenericList(_reporter).RunTestComparison(currentIterations, "Generic List Test", "for", "foreach");
                    Console.WriteLine("---Non-Generics---\n");
                    new TestArrayList(_reporter).RunTestComparison(currentIterations, "ArrayList Test", "for", "foreach");
                    new TestHashtable(_reporter).RunTestComparison(currentIterations, "Hashtable Test", "for", "foreach");
                    new TestQueue(_reporter).RunTestComparison(currentIterations, "Queue Test", "for", "foreach");
                    new TestStack(_reporter).RunTestComparison(currentIterations, "Stack Test", "for", "foreach");
                }
            }
        }
    }

    public class TestGenericList : TestUtility
    {
        private readonly List<int> _list;

        public TestGenericList(PerformanceReporter reporter) : base(reporter)
        {
            _list = new List<int>();
        }
        public override void Setup(int iterations)
        {
       
            for (int i = 0; i < iterations; i++)
            {
                _list.Add(i);
            }
        }

        public override void TestLogic(int iterations)
        {
            int sumFor = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                sumFor += _list[i];
            }
        }

        public override void TestLogic2(int iterations)
        {
            int sumForeach = 0;
            foreach (var item in _list)
            {
                sumForeach += item;
            }
        }
    }

    public class TestArrayList : TestUtility
    {
        private ArrayList _arrayList;

        public TestArrayList(PerformanceReporter reporter) : base(reporter) 
        { 
            _arrayList = new ArrayList();
        }

        public override void Setup(int iterations)
        {
            _arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                _arrayList.Add(i);
            }
        }

        public override void TestLogic(int iterations)
        {
            for (int i = 0; i < _arrayList.Count; i++)
            {
                var item = (int)_arrayList[i];
            }
        }

        public override void TestLogic2(int iterations)
        {
            foreach (var item in _arrayList)
            {
                var currentItem = (int)item;
            }
        }
    }

    public class TestHashtable : TestUtility
    {
        private Hashtable _hashTable;

        public TestHashtable(PerformanceReporter reporter) : base(reporter) 
        {
            _hashTable = new Hashtable();
        }

        public override void Setup(int iterations)
        {
            _hashTable = new Hashtable();
            for (int i = 0; i < iterations; i++)
            {
                _hashTable.Add(i, i);
            }
        }

        public override void TestLogic(int iterations)
        {
            var keys = new ArrayList(_hashTable.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                var key = (int)keys[i];
                var value = (int)_hashTable[key]; // Access value using key
            }
        }

        public override void TestLogic2(int iterations)
        {
            foreach (DictionaryEntry entry in _hashTable)
            {
                var key = (int)entry.Key;
                var value = (int)entry.Value;
            }
        }
    }

    public class TestQueue : TestUtility
    {
        private Queue _queue;

        public TestQueue(PerformanceReporter reporter) : base(reporter) 
        {
            _queue = new Queue();
        }

        public override void Setup(int iterations)
        {
            _queue = new Queue();
            for (int i = 0; i < iterations; i++)
            {
                _queue.Enqueue(i);
            }
        }

        public override void TestLogic(int iterations)
        {
            var queueForTest = new Queue(_queue);
            while (queueForTest.Count > 0)
            {
                queueForTest.Dequeue();
            }
        }

        public override void TestLogic2(int iterations)
        {
            var queueForeachTest = new Queue(_queue);
            foreach (var item in queueForeachTest)
            {
                // Iterating over the queue
            }
        }
    }

    public class TestStack : TestUtility
    {
        private Stack _originalStack;

        public TestStack(PerformanceReporter reporter) : base(reporter) 
        {
            _originalStack = new Stack();
        }

        public override void Setup(int iterations)
        {
            _originalStack = new Stack();
            for (int i = 0; i < iterations; i++)
            {
                _originalStack.Push(i);
            }
        }

        public override void TestLogic(int iterations)
        {
            var stackForTest = new Stack(_originalStack);
            while (stackForTest.Count > 0)
            {
                stackForTest.Pop();
            }
        }

        public override void TestLogic2(int iterations)
        {
            var stackForeachTest = new Stack(_originalStack);
            foreach (var item in stackForeachTest)
            {
                var currentItem = (int)item; // Unboxing
            }
        }
    }
}