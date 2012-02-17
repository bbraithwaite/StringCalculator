using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace StringCalculator.Tests
{
    [TestClass]
    public class CalculatorConsoleTests
    {
        [TestMethod]
        public void Main_WithValidCommandAndNumbers_DisplaysTheSumTheConsole()
        {
            // Arrange
            Mock<IStringCalculator> mockCalculator = new Mock<IStringCalculator>();
            mockCalculator.Setup(c => c.Add("1,2,3")).Returns(6);
            mockCalculator.Setup(c => c.Add("1,2,1")).Returns(4);

            // Act & Assert
            VerifyConsoleOutput("scalc '1,2,1'", "The result is 4", mockCalculator);
            VerifyConsoleOutput("scalc '1,2,3'", "The result is 6", mockCalculator);
        }

        [TestMethod]
        public void Main_WithAValidCommand_PromptsForAnotherNumber()
        {
            // Arrange
            Mock<IStringCalculator> mockCalculator = new Mock<IStringCalculator>();
            Mock<IConsole> mockConsole = new Mock<IConsole>();
            mockCalculator.Setup(c => c.Add("1,2,3")).Returns(6);
            StringCalculatorConsole stringCalculatorConsole = CreateCalcConsole(mockCalculator, mockConsole);

            // Act
            stringCalculatorConsole.Main(new string[] { "scalc '1,2,3'" });

            // Assert
            mockConsole.Verify(c => c.WriteLine("The result is 6"));
            mockConsole.Verify(c => c.WriteLine("another input please"));
            mockConsole.Verify(c => c.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void Main_WithTwoValidCommands_ExitsProgramWhenEmptyStringIsEntered()
        {
            // Arrange
            Mock<IStringCalculator> mockCalculator = new Mock<IStringCalculator>();
            Mock<IConsole> mockConsole = new Mock<IConsole>();
            mockCalculator.Setup(c => c.Add("1,2,3")).Returns(6);
            mockCalculator.Setup(c => c.Add("3")).Returns(3);
            mockCalculator.Setup(c => c.Add("5")).Returns(5);

            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("3")
                .Returns("5")
                .Returns("");

            StringCalculatorConsole stringCalculatorConsole = CreateCalcConsole(mockCalculator, mockConsole);

            // Act
            stringCalculatorConsole.Main(new string[] { "scalc '1,2,3'" });

            // Assert
            mockConsole.Verify(c => c.WriteLine("The result is 6"));
            mockConsole.Verify(c => c.WriteLine("another input please"), Times.Exactly(3));
            mockConsole.Verify(c => c.WriteLine("The result is 3"));
            mockConsole.Verify(c => c.WriteLine("The result is 5"));
            mockConsole.Verify(c => c.ReadLine(), Times.Exactly(3));
        }

        [TestMethod]
        public void Main_WithInvalidCommand_DisplaysNothingToTheConsole()
        {
            // Arrange
            Mock<IConsole> mockConsole = new Mock<IConsole>();
            StringCalculatorConsole scConsole = CreateCalcConsole(mockConsole);


            // Act
            scConsole.Main(new string[] { "1,2,3" });

            // Assert
            mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Never());
        }

        private void VerifyConsoleOutput(string input, string consoleOutput, Mock<IStringCalculator> mockCalculator)
        {
            // Arrange
            Mock<IConsole> mockConsole = new Mock<IConsole>();
            StringCalculatorConsole scConsole = CreateCalcConsole(mockCalculator, mockConsole);

            // Act
            scConsole.Main(new string[] { input });

            // Assert
            mockConsole.Verify(c => c.WriteLine(consoleOutput));
        }

        private static StringCalculatorConsole CreateCalcConsole(Mock<IConsole> mockConsole)
        {
            return new StringCalculatorConsole(new Mock<IStringCalculator>().Object, mockConsole.Object);
        }

        private static StringCalculatorConsole CreateCalcConsole(Mock<IStringCalculator> mockCalculator, Mock<IConsole> mockConsole)
        {
            return new StringCalculatorConsole(mockCalculator.Object,
                                               mockConsole.Object);
        }
    }
}
