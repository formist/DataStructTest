using DataStructTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    
    
    /// <summary>
    ///这是 VectorTest 的测试类，旨在
    ///包含所有 VectorTest 单元测试
    ///</summary>
    [TestClass()]
    public class VectorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///MergeSort 的测试
        ///</summary>
        public void MergeSortTestHelper<T>()
            where T : IComparable
        {
            Random rnd = new Random();
            int capacity = 0; // TODO: 初始化为适当的值
            int s = 0; // TODO: 初始化为适当的值
            
            int lo = 0; // TODO: 初始化为适当的值
            int hi = 100; // TODO: 初始化为适当的值
            Vector<int> target = new Vector<int>(); // TODO: 初始化为适当的值
            for (int i = 0; i < hi; i++)
            {
                int value = rnd.Next(-hi, hi);
                target.Add(value);
            }
            target.MergeSort(lo, hi);
            Assert.AreEqual(0, target.Disordered());
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void MergeSortTest()
        {
            MergeSortTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///BubbleSort 的测试
        ///</summary>
        public void BubbleSortTestHelper<T>()
            where T : IComparable
        {
            Random rnd = new Random();
            int lo = 0; // TODO: 初始化为适当的值
            int hi = 100; // TODO: 初始化为适当的值
            Vector<int> target = new Vector<int>(); // TODO: 初始化为适当的值
            for (int i = 0; i < hi; i++)
            {
                int value = rnd.Next(-hi, hi);
                target.Add(value);
            }
            target.BubbleSort(lo, hi);
            Assert.AreEqual(0, target.Disordered());
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void BubbleSortTest()
        {
            BubbleSortTestHelper<GenericParameterHelper>();
        }
    }
}
