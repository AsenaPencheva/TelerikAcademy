namespace RotatingWalkInMatrix.Tests.Fakes
{
    public class DirectionMock : Direction
    {
        private static int[] directionsRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
        private static int[] directionsCol = { 1, 0, -1, -1, -1, 0, 1, 1 };

        public DirectionMock() : base()
        {
            this.Row = directionsRow[0];
            this.Column = directionsCol[0];
        }
    }
}
