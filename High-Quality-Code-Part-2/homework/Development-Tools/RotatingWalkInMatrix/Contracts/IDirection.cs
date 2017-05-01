namespace RotatingWalkInMatrix.Contracts
{
    public interface IDirection
    {
        int Row { get; set; }

        int Column { get; set; }

        int DirectionsCount { get; }

        void ChangeDirection();
    }
}
