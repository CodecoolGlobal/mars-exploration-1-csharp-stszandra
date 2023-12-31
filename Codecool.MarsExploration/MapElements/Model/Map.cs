﻿    using System.Text;

    namespace Codecool.MarsExploration.MapElements.Model;

    public record Map(string?[,] Representation, bool SuccessfullyGenerated = false)
    {
        protected static string CreateStringRepresentation(string?[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < cols; j++)
                {
                    string element = arr[i, j] ?? " ";
                    stringBuilder.Append(element);
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return CreateStringRepresentation(Representation);
        }
    }
