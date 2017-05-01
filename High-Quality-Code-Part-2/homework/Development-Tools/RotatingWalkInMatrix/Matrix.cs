namespace RotatingWalkInMatrix
{
    using System;

    using Contracts;

    public class Matrix
    {
       
        public static int[,] CreateMatrix(int rows, int columns, IDirection direction)
        {
            
            if (direction == null)
            {
                throw new ArgumentNullException("Direction cannot be null!");
            }

            if (rows < 0 || columns < 0)
            {
                throw new ArgumentOutOfRangeException("Size values cannot be negative!");
            }

            int[,] matrix = new int[rows, columns];
           
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = 0;
                }
            }
           
            matrix = FillMatrix(matrix, direction);
                        return matrix;
        }

        private static int[,] FillMatrix(int[,] matrix, IDirection direction)
        {
            int number = 1;
            int currentRow = 0;
            int currentColumn = 0;
            int nextRow = 0;
            int nextColumn = 0;

            while (number <= matrix.Length)
            {
                nextRow = currentRow + direction.Row;
                nextColumn = currentColumn + direction.Column;
                matrix[currentRow, currentColumn] = number;
                number++;
                bool hasEmptyCell = false;
                for (int i = 0; i < direction.DirectionsCount; i++)
                {
                    if (!CheckisEmptyNextCell(matrix, nextRow, nextColumn))
                    {
                        direction.ChangeDirection();
                        nextRow = currentRow + direction.Row;
                        nextColumn = currentColumn + direction.Column;
                    }
                    else
                    {
                        currentRow = nextRow;
                        currentColumn = nextColumn;
                        hasEmptyCell = true;
                        break;
                    }
                }

                if (!hasEmptyCell)
                {
                    int[] coord = FindEmptyCell(matrix);
                    currentRow = coord[0];
                    currentColumn = coord[1];
                }
            }

            return matrix;
        }

        private static int[] FindEmptyCell(int[,] matrix)
        {
            int[] coord = new int[] { -1, -1 };
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        coord[0] = i;
                        coord[1] = j;
                        return coord;
                    }
                }
            }

            return coord;
        }

        private static bool CheckisEmptyNextCell(int[,] matrix, int nextRow, int nextColumn)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            if (nextRow < rows && nextColumn < columns && nextRow >= 0 && nextColumn >= 0)
            {
                if (matrix[nextRow, nextColumn] == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static void LoadMatrix(int[,] matrix, ILogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("Looger cannot be null!"); 
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    logger.Log($"{matrix[i, j],3} ");
                }

                logger.LogLine(string.Empty);
            }
        }
    }
}