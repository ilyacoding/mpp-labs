using System.Configuration;
using Lab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab1Tests
{
    [TestClass]
    public class DynamicListTests
    {
        [TestMethod]
        public void AddTest()
        {
            // arrange
            var elementExpected = 2;
            var sizeExpected = 1;

            // act
            var array = new DynamicList<int>();
            array.Add(elementExpected);

            // assert
            Assert.AreEqual(array.Items[0], elementExpected);
            Assert.AreEqual(array.Count, sizeExpected);
        }

        [TestMethod]
        public void AddArrayTest()
        {
            // arrange
            var elementExpected = new int[] {1, 2, 2, 3, 4, 4, 5, 6};

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }

            // assert
            int i = 0;
            foreach (var obj in array.Items)
            {
                Assert.AreEqual(elementExpected[i++], obj);
            }
            
            Assert.AreEqual(array.Count, elementExpected.Length);
        }

        [TestMethod]
        public void RemoveTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };
            var checkElementExpected = new int[] { 1, 3, 4, 4, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }

            array.Remove(2);

            // assert
            int i = 0;
            foreach (var obj in array.Items)
            {
                Assert.AreEqual(checkElementExpected[i++], obj);
            }

            Assert.AreEqual(array.Count, checkElementExpected.Length);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };
            var checkElementExpected = new int[] { 1, 2, 2, 3, 4, 2, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }

            array.RemoveAt(4);

            // assert
            int i = 0;
            foreach (var obj in array.Items)
            {
                Assert.AreEqual(checkElementExpected[i++], obj);
            }

            Assert.AreEqual(array.Count, checkElementExpected.Length);
        }

        [TestMethod]
        public void ClearTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }

            array.Clear();

            // assert
            Assert.AreEqual(array.Count, 0);
        }

        [TestMethod]
        public void CountTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }
          
            // assert
            Assert.AreEqual(array.Count, elementExpected.Length);
        }

        [TestMethod]
        public void ItemsTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }
            
            // assert
            int i = 0;
            foreach (var obj in array.Items)
            {
                Assert.AreEqual(elementExpected[i++], obj);
            }
        }

        [TestMethod]
        public void ForeachTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }

            // assert
            int i = 0;
            foreach (var obj in array)
            {
                Assert.AreEqual(elementExpected[i++], obj);
            }
        }

        [TestMethod]
        public void IndexatorTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };

            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }

            // assert
            int i = 0;
            for (var j = 0; j < array.Count; j++)
            {
                Assert.AreEqual(elementExpected[i++], array[j]);
            }
        }

        [TestMethod]
        public void ElementSetTest()
        {
            // arrange
            var elementExpected = new int[] { 1, 2, 2, 3, 4, 4, 2, 5, 6 };
            var assignInt = 99;
            // act
            var array = new DynamicList<int>();
            foreach (var el in elementExpected)
            {
                array.Add(el);
            }
            array[2] = assignInt;
            array.Items[3] = assignInt;

            // assert
            Assert.AreEqual(array[2], 99);
            Assert.AreEqual(array.Items[3], assignInt);
        }
    }
}
