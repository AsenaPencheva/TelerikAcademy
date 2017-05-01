namespace RotatingWalkInMatrix
{
    using log4net;
    using log4net.Config;
    using System;

    public class StartUp
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter a number");
                int size = int.Parse(Console.ReadLine());
                log.Info("Size was passed.");

                var direction = new Direction();

                var matrix = Matrix.CreateMatrix(size, size, direction);
                log.Info("Matrix was created.");

                var consoleLogger = new ConsoleLogger();
                Matrix.LoadMatrix(matrix, consoleLogger);
                log.Info("Matrix was loaded.");
            }
            catch (Exception e)
            {
                log.Error($"{e.GetType()}:{e}");
            }


        }
    }
}
