namespace RotatingWalkInMatrix.Tests
{
    using System;
    using Contracts;
    using Moq;
    using NUnit.Framework;
    using RotatingWalkInMatrix;

    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void CreateMatrix_ShouldThrowArgumentNullException_WhenNullDirectionIsPassed()
        {
            Assert.Throws<ArgumentNullException>(() => Matrix.CreateMatrix(1, 1, null));
        }

        [Test]
        public void CreateMatrix_ShouldThrowArgumentOutOfRangeException_WhenInvalidRowsParameterIsPassed()
        {
            var directionMocked = new Mock<IDirection>();

            Assert.Throws<ArgumentOutOfRangeException>(() => Matrix.CreateMatrix(-1, 1, directionMocked.Object));
        }

        [Test]
        public void CreateMatrix_ShouldThrowArgumentOutOfRangeException_WhenInvalidColumnsParameterIsPassed()
        {
            var directionMocked = new Mock<IDirection>();

            Assert.Throws<ArgumentOutOfRangeException>(() => Matrix.CreateMatrix(1, -1, directionMocked.Object));
        }

        [Test]
        public void CreateMatrix_ShouldCreateMatrix_WhenValidParametersArePassed()
        {
            var directionMocked = new Mock<IDirection>();

            var matrix = Matrix.CreateMatrix(1, 1, directionMocked.Object);

            Assert.IsNotNull(matrix);
        }

        [Test]
        public void CreateMatrix_ShouldFillMatrix_WhenValidParametersArePassed()
        {
            var size = 5;

            int[] directionsRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] directionsCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            var index = 0;
            var directionMocked = new Mock<IDirection>();

            directionMocked.Setup(x => x.Row).Returns(directionsRow[index]);
            directionMocked.Setup(x => x.Column).Returns(directionsCol[index]);
            directionMocked.Setup(x => x.ChangeDirection()).Callback(() => index = (index + 1) % 8);

            var matrix = Matrix.CreateMatrix(size, size, directionMocked.Object);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Assert.AreNotEqual(0, matrix[i, j]);
                }
            }
        }

        [Test]
        public void LoadMatrix_ShouldThrowArgumentNullException_WhenValidLoggerParameterIsNull()
        {
            var size = 5;
            var directionMocked = new Mock<IDirection>();
            var loggerMocked = new Mock<ILogger>();
            loggerMocked.Setup(x => x.Log(It.IsAny<string>()));
            var matrix = Matrix.CreateMatrix(size, size, directionMocked.Object);

            Matrix.LoadMatrix(matrix, loggerMocked.Object);

            loggerMocked.Verify(x => x.Log(It.IsAny<string>()), Times.Exactly(size * size));
        }

        [Test]
        public void LoadMatrix_ShouldCallLogMethod_WhenValidParametersArePassed()
        {
            var size = 5;
            var directionMocked = new Mock<IDirection>();
            var matrix = Matrix.CreateMatrix(size, size, directionMocked.Object);

            Assert.Throws<ArgumentNullException>(() => Matrix.LoadMatrix(matrix, null));
        }
    }
}