namespace RotatingWalkInMatrix.Tests
{
    using Fakes;
    using NUnit.Framework;

    [TestFixture]
    public class DirectionTests
    {
        [Test]
        public void Constructor_ShouldAssignRowValueProperly()
        {
            var expectedRowValue = 1;
            var direction = new DirectionMock();
            Assert.AreEqual(direction.Row, expectedRowValue);
        }

        [Test]
        public void Constructor_ShouldAssignColumnValueProperly()
        {
            var expectedColumnValue = 1;
            var direction = new DirectionMock();
            Assert.AreEqual(direction.Column, expectedColumnValue);
        }

        [Test]
        public void Constructor_ShouldAssignDirectionsCountValueProperly()
        {
            var expectedDirectionsCount = 8;

            var direction = new DirectionMock();

            Assert.AreEqual(direction.DirectionsCount, expectedDirectionsCount);
        }

        [Test]
        public void ChangeDirection_ShouldChangeDirection()
        {           
            var direction = new DirectionMock();
            var directionRow = direction.Row;
            var directionColumn = direction.Column;
            var coord = new int[] { directionRow, directionColumn };

            direction.ChangeDirection();
            var newCoord = new int[] { direction.Row, direction.Column };

            Assert.AreNotEqual(coord, newCoord);
        }

        [Test]
        public void ChangeDirection_ShouldChangeAtFirstDirection_WhenReachedLastDirection()
        {
            var direction = new Direction();
            var startDirectionRow = direction.Row;
            var startDirectionColumn = direction.Column;
            var startCoord = new int[] { startDirectionRow, startDirectionColumn };
            var directionsCount = direction.DirectionsCount;

            for (int i = 0; i < directionsCount; i++)
            {
                direction.ChangeDirection();
            }   
                     
            var newCoord = new int[] { direction.Row, direction.Column };

            Assert.AreEqual(startCoord, newCoord);
        }
    }
}
