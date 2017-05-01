namespace RotatingWalkInMatrix.Contracts
{
    public interface ILogger
    {
        void Log(string message);

        void LogLine(string message);
    }
}