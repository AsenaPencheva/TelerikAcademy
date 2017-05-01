namespace RotatingWalkInMatrix
{
    using Contracts;
   
    public class Direction : IDirection
    {
        private const int PosibleDiretions = 8;
        private static readonly int[] DirectionsRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
        private static readonly int[] DirectionsCol = { 1, 0, -1, -1, -1, 0, 1, 1 };

        public Direction()
        {
            this.Row = DirectionsRow[0];
            this.Column = DirectionsCol[0];
            this.DirectionsCount = PosibleDiretions;
        }

        public int Column { get; set; }

        public int Row { get; set; }

        public int DirectionsCount { get; }

        public void ChangeDirection()
        {
            for (int i = 0; i < PosibleDiretions; i++)
            {
                if (this.Row == DirectionsRow[i] && this.Column == DirectionsCol[i])
                {
                    this.Row = DirectionsRow[(i + 1) % PosibleDiretions];
                    this.Column = DirectionsCol[(i + 1) % PosibleDiretions];
                    break;
                }
            }
        }
    }
}
