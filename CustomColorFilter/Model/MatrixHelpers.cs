using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CustomColorFilter.Model
{
    internal class MatrixHelpers
    {
        public static float[] ToFlatArray(float[,] matrix)
        {
            var result = new float[matrix.GetLength(0) * matrix.GetLength(1)];
            for (int row = 0; row < matrix.GetLength(0); row += 1)
            {
                for (int column = 0; column < matrix.GetLength(1); column += 1)
                {
                    result[row * matrix.GetLength(0) + column] = matrix[row, column];
                }
            }
            return result;
        }

        public static float[,] ToMatrix(float[] array, int numberOfRows)
        {
            if (array.Length == 0 || array.Length % numberOfRows != 0)
            {
                throw new ArgumentException("bad matrix size");
            }

            var numberOfColumns = array.Length / numberOfRows;
            var result = new float[numberOfRows, numberOfColumns];
            
            for (int row = 0; row < numberOfRows; row += 1)
            {
                for (int column = 0; column < numberOfColumns; column += 1)
                {
                    result[row, column] = array[row * numberOfRows + column];
                }
            }

            return result;
        }
    }
}
