# Solving Systems of Linear Equations Using the Simple Iteration Method

This repository contains a program for solving systems of linear algebraic equations (SLAEs) using the **Simple Iteration Method (Jacobi Method)**. The program provides intermediate vectors, solution approximations, and residual vectors for the given system.

## Problem Statement

Solving a system of linear equations involves finding the values of variables that satisfy all equations in the system simultaneously. The **Simple Iteration Method** transforms the system $( A \cdot x = b \)$ into an iterative process of the form: $x^{(k+1)} = \beta + \alpha \cdot x^{(k)}$

Where:
- $( \beta \)$: constant vector derived from the system.
- $( \alpha \)$: coefficient matrix influencing the convergence of the iterative process.

---

## Task Description

### Input:
- Order of the system $( n \)$.
- Coefficient matrix $( A \)$.
- Right-hand side vector $( b \)$.
- Desired precision $( \epsilon \)$ (for iterative methods).

### Output:
- Intermediate approximations of the solution vector.
- Final solution.
- Residual vector.
- Number of iterations.

---

## Example Problem

The following system is solved in this program:

$$
\begin{aligned}
4x_1 + 0.24x_2 - 0.08x_3 &= 8 \\
0.09x_1 + 3x_2 + 0.15x_3 &= 9 \\
0.04x_1 + 0.08x_2 + 4x_3 &= 20
\end{aligned}
$$

### Iterative Transformation:
Transforming the system into iterative form:

$$
\begin{aligned}
x_1 &= 2 - 0.06x_2 + 0.02x_3 \\
x_2 &= 3 - 0.03x_1 - 0.05x_3 \\
x_3 &= 5 - 0.01x_1 - 0.02x_2
\end{aligned}
$$

The matrix representations are:
- $( \beta \): \([2, 3, 5]^T\)$
- $( \alpha \)$:

$$
\begin{bmatrix}
0 & -0.06 & 0.02 \\
-0.03 & 0 & -0.05 \\
-0.01 & -0.02 & 0
\end{bmatrix}
$$

---

## Code Structure

### Main Features
1. **Input Module**:
   - Coefficient matrix $( A \)$, vector $( b \)$, and precision $( \epsilon \)$ are provided as input.
   - Supports dynamic input or hardcoded matrices.

2. **Computation Module**:
   - Initializes $( \beta \)$ and $( \alpha \)$ matrices.
   - Iteratively computes $x^{(k+1)} = \beta + \alpha \cdot x^{(k)}$.

3. **Output Module**:
   - Displays intermediate approximations and final results.
   - Outputs residual vector and iteration count.

4. **Convergence Check**:
   - Validates whether the stopping criterion $||x^{(k+1)} - x^{(k)}|| \leq \epsilon$ is met.

---

### Example Code Snippets

#### Matrix Initialization
```csharp
for (int i = 0; i < n; i++)
{
    List<double> row = new List<double>(new double[n]);
    for (int j = 0; j < n; j++)
    {
        row[j] = (i == j) ? 0 : -A[i][j] / A[i][i];
    }
    alpha.Add(row);
    beta.Add(b[i] / A[i][i]);
}
```

#### Iterative Process
```csharp
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

    PrintVector(x, $"Approximation of X after iteration {iteration}");
    converge = CheckConvergence(x, prevX, tolerance);
    prevX = new List<double>(x);
}
```

#### Convergence Check
```csharp
static bool CheckConvergence(List<double> x, List<double> prevX, double tolerance)
{
    double norm = 0;
    for (int i = 0; i < x.Count; i++)
    {
        norm += Math.Pow(x[i] - prevX[i], 2);
    }
    norm = Math.Sqrt(norm);
    return norm < tolerance;
}
```

#### Residual Calculation
```csharp
static void CalculateAndPrintResiduals(List<List<double>> A, List<double> b, List<double> x)
{
    List<double> residuals = new List<double>();
    for (int i = 0; i < A.Count; i++)
    {
        double residual = b[i];
        for (int j = 0; j < A[i].Count; j++)
        {
            residual -= A[i][j] * x[j];
        }
        residuals.Add(residual);
    }
    Console.WriteLine("Residual Vector:");
    foreach (var r in residuals)
    {
        Console.WriteLine($"{r:E}");
    }
}
```

---

## Example Results

Given $( A \)$, $( b \)$, and $( \epsilon = 0.00001 \)$:
- **Solution:**  $[1.936, 2.695, 4.926]^T$
- **Residuals:** $( r_1 = 4.922 \times 10^{-7}, r_2 = 2.197 \times 10^{-7}, r_3 = 2.257 \times 10^{-7}$

These results confirm the high precision of the method, satisfying the original system with negligible residuals.

---

## Requirements
- **Language:** C#
- **Environment:** .NET Framework or .NET Core
- **Libraries:** `System`, `System.Diagnostics`

---

---

## How to Use

1. Clone the repository:
   ```bash
   git clone https://github.com/Nissmoline/linear-system-simple-iteration.git
   cd linear-system-simple-iteration
   ```
2. Run the program:
   ```bash
   dotnet run
   ```
3. Enter the desired $( A \)$, $( b \)$, and $( \epsilon \)$ when prompted.

---

## License
This project is open-source and available under the Expat License.
