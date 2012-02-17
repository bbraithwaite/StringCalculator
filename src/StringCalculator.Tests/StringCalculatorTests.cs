using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculatorTests : BaseTest
    {
        private Mock<IConsole> _mockConsole;

        private Mock<IConsole> MockConsole
        {
            get
            {
                if (_mockConsole == null)
                {
                    _mockConsole = new Mock<IConsole>();
                }

                return _mockConsole;
            }
        }

        private StringCalculator StringCalculator
        {
            get
            {
                return new StringCalculator(MockConsole.Object, new SubStringDelimiterParser());
            }
        }

        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            Assert.AreEqual(0, StringCalculator.Add(""));
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsTheNumber()
        {
            Assert.AreEqual(1, StringCalculator.Add("1"));
        }

        [TestMethod]
        public void Add_MultipleNumbers_ReturnsTheSumOfNumbers()
        {
            Assert.AreEqual(3, StringCalculator.Add("1,2"));
            Assert.AreEqual(6, StringCalculator.Add("1,2,3"));
        }

        [TestMethod]
        public void Add_NumbersWithNewLineDelimiter_ReturnsTheSumOfNumbers()
        {
            Assert.AreEqual(6, StringCalculator.Add("1\n2,3"));
        }

        [TestMethod]
        public void Add_NewLineDelimiter_ReturnsZero()
        {
            Assert.AreEqual(0, StringCalculator.Add("\n"));
        }

        [TestMethod]
        public void Add_WithCustomDelimiter_ReturnsTheSumOfNumbers()
        {
            Assert.AreEqual(3, StringCalculator.Add("//;\n1;2"));
        }

        [TestMethod]
        public void Add_WithSingleNegativeNumber_ThrowsException()
        {
            Assert.Throws(() => StringCalculator.Add("-1"), "negatives not allowed -1");
        }

        [TestMethod]
        public void Add_WithMultipleNegativeNumbers_ThrowsException()
        {
            Assert.Throws(() => StringCalculator.Add("-1,2,-3"), "negatives not allowed -1,-3");
        }

        [TestMethod]
        public void Add_WithNumberGreaterThan1000_IgnoresNumber()
        {
            Assert.AreEqual(2, StringCalculator.Add("2,1001"));
        }

        [TestMethod]
        public void Add_WithMultiCharCustomDelimiter_ReturnsTheSum()
        {
            Assert.AreEqual(6, StringCalculator.Add("//[***]\n1***2***3"));
        }

        [TestMethod]
        public void Add_WithMultipleCustomDelimiter_ReturnsTheSum()
        {
            Assert.AreEqual(6, StringCalculator.Add("//[*][%]\n1*2%3"));
        }

        [TestMethod]
        public void Add_WithMultipleCustomDelimitersMultipleChar_ReturnsTheSum()
        {
            Assert.AreEqual(6, StringCalculator.Add("//[**][%%]\n1**2%%3"));
        }

        [TestMethod]
        public void Add_Numbers_OutputsSumToConsole()
        {
            // Act
            StringCalculator.Add("1,2");

            // Assert
            MockConsole.Verify(c => c.WriteLine("3"));
        }
    }
}
