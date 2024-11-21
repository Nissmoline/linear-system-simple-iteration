using System;
using System.Collections.Generic;
using System.Globalization;

namespace SimpleIteration_6._2._1
{
    class Program
    {
        static void PrintMatrix(List<List<double>> A, string title = "Matrix")
        {
            Console.WriteLine($"{title}:");
            for (int i = 0; i < A.Count; i++)
            {
                for (int j = 0; j < A[i].Count; j++)
                {
                    Console.Write($"{A[i][j],10:0.#####} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void PrintVector(List<double> v, string name)
        {
            Console.WriteLine($"{name}:");
            for (int i = 0; i < v.Count; i++)
            {
                Console.WriteLine($"{v[i]:0.#####}");
            }
            Console.WriteLine();
        }

        static List<double> MatrixToVector(List<List<double>> matrix)
        {
            List<double> vector = new List<double>();
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    vector.Add(matrix[i][j]);
                }
            }
            return vector;
        }

        static List<double> SimpleIterationMethod(List<List<double>> A, List<double> b, double tolerance)
        {
            int n = A.Count;
            List<double> x = new List<double>(new double[n]);
            List<double> prevX = new List<double>(new double[n]);
            List<List<double>> alpha = new List<List<double>>(n);
            List<double> beta = new List<double>(n);

            for (int i = 0; i < n; i++)
            {
                List<double> row = new List<double>(new double[n]);
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        row[j] = 0;
                    else
                        row[j] = -A[i][j] / A[i][i];
                }
                alpha.Add(row);
                beta.Add(b[i] / A[i][i]);
            }

            PrintMatrix(alpha, "Alpha Matrix");
            PrintVector(beta, "Beta Vector");

            bool converge = false;
            int iteration = 0;
            List<double> alphaVector = MatrixToVector(alpha);

            while (!converge)
            {
                Console.WriteLine($"Iteration {++iteration}:");
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        sum += alpha[i][j] * prevX[j];
                    }
                    x[i] = beta[i] + sum;
                }

                PrintVector(x, $"Approximation of vector X after iteration {iteration}");

                converge = CheckConvergence(x, prevX, alphaVector, tolerance);
                prevX = new List<double>(x);
            }

            return x;
        }

        static bool CheckConvergence(List<double> x, List<double> prevX, List<double> alpha, double tolerance)
        {
            double norm = 0;
            double alphaNorm = 0;

            for (int i = 0; i < x.Count; i++)
            {
                norm += Math.Pow(x[i] - prevX[i], 2);
            }
            norm = Math.Sqrt(norm);

            for (int i = 0; i < alpha.Count; i++)
            {
                alphaNorm += Math.Pow(alpha[i], 2);
            }
            alphaNorm = Math.Sqrt(alphaNorm);

            double rightHandSide = (1 - alphaNorm / alphaNorm) * tolerance;

            return norm <= rightHandSide;
        }

        static void CalculateAndPrintResiduals(List<List<double>> A, List<double> b, List<double> x)
        {
            List<double> residuals = new List<double>(new double[A.Count]);
            double sumSquaredResidual = 0;

            for (int i = 0; i < A.Count; i++)
            {
                double residual = b[i];
                for (int j = 0; j < A[i].Count; j++)
                {
                    residual -= A[i][j] * x[j];
                }
                residuals[i] = residual;
                sumSquaredResidual += residual * residual;
            }

            Console.WriteLine("Residual Vector:");
            for (int i = 0; i < residuals.Count; i++)
            {
                Console.WriteLine($"r{i + 1} = {residuals[i]:E}");
            }
        }

        static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            List<List<double>> A = new List<List<double>>
            {
               new List<double> { 4, 0.24, -0.08 },
                new List<double> { 0.09, 3, 0.15 },
                new List<double> { 0.04, 0.08, 4 }
            };

            List<double> b = new List<double> { 8, 9, 20 };

            PrintMatrix(A);
            PrintVector(b, "Original Beta Vector:");

            Console.Write("Enter accuracy 'eps': ");
            double tolerance;
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out tolerance))
            {
                Console.Write("Invalid input. Please enter a numerical value for accuracy ε: ");
            }

            List<double> x = SimpleIterationMethod(A, b, tolerance);

            PrintVector(x, "Solution");

            CalculateAndPrintResiduals(A, b, x);

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }
}
