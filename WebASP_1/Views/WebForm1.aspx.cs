using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebASP_1.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static int score = 0;
        private static int[,] matrix;
        private static string firstButtonValue;
        private static string firstButtonId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                score = 0;
                matrix = null;
                firstButtonValue = null;
                firstButtonId = null;
            }
        }

        [WebMethod]
        public static object GenerateMatrix(int matrixSize)
        {
            matrix = GenerateMatrixWithRandomNumbers(matrixSize);
            SaveMatrixToHiddenField(matrixSize, matrix);

            string matrixHtml = DisplayMatrix(matrix);
            return new { MatrixHtml = matrixHtml, Score = score };
        }

        private static int[,] GenerateMatrixWithRandomNumbers(int size)
        {
            Random rand = new Random();
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rand.Next(1, 10); 
                }
            }
            return matrix;
        }

        private static string DisplayMatrix(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] != 0) // Только если значение не равно 0
                    {
                        string btnId = "btn_" + i + "_" + j;
                        sb.AppendFormat("<button type='button' id='{0}' onclick='buttonClick(\"{0}\")'>{1}</button>", btnId, matrix[i, j]);
                    }
                }
                sb.Append("<br />");
            }
            return sb.ToString();
        }

        private static void SaveMatrixToHiddenField(int size, int[,] matrix)
        {
            
        }

        [WebMethod]
        public static object ButtonClick(string buttonId)
        {
            string[] ids = buttonId.Split('_');
            int row = int.Parse(ids[1]);
            int col = int.Parse(ids[2]);

            if (string.IsNullOrEmpty(firstButtonValue))
            {
                firstButtonValue = matrix[row, col].ToString();
                firstButtonId = buttonId;
            }
            else
            {
                int firstValue = int.Parse(firstButtonValue);
                int secondValue = matrix[row, col];

                if (firstValue + secondValue == 10)
                {
                    score++;

                    matrix[int.Parse(firstButtonId.Split('_')[1]), int.Parse(firstButtonId.Split('_')[2])] = 0;
                    matrix[row, col] = 0;
                }

                firstButtonValue = null;
                firstButtonId = null;
            }

            string matrixHtml = DisplayMatrix(matrix);
            return new { MatrixHtml = matrixHtml, Score = score };
        }

        [WebMethod]
        public static void ResetGame()
        {
            score = 0;
            matrix = null;
            firstButtonValue = null;
            firstButtonId = null;
        }
    }
}