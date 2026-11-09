using System;

delegate double MathOperation(double a, double b);
delegate void CalculationHandler(string operationName, double result);

class Calculator
{
    public event CalculationHandler CalculationPerformed;

    public double Calculate(double a, double b,
        MathOperation operation, string operationName)
    {
        double result = operation(a, b);

        CalculationPerformed?.Invoke(operationName, result);

        return result;
    }
}

class Program
{
    static void PrintResult(string operationName, double result)
    {
        Console.WriteLine(operationName + ": " + result);
    }

    static double Add(double a, double b)
    {
        return a + b;
    }

    static void Main()
    {
        Calculator calculator = new Calculator();

        CalculationHandler lambda = (name, result) =>
        {
            Console.WriteLine("[lambda] " + name + ": " + result);
        };

        calculator.CalculationPerformed += PrintResult;
        calculator.CalculationPerformed += lambda;

        MathOperation addition = Add;
        MathOperation subtraction = (a, b) => a - b;
        MathOperation multiplication = (a, b) => a * b;
        MathOperation division = (a, b) => a / b;

        Console.WriteLine("Результаты вычислений:\n");

        calculator.Calculate(10, 5, addition, "Сложение");
        calculator.Calculate(10, 5, subtraction, "Вычитание");
        calculator.Calculate(10, 5, multiplication, "Умножение");
        calculator.Calculate(10, 5, division, "Деление");

        calculator.CalculationPerformed -= lambda;

        Console.WriteLine("\nПосле отписки лямбда-обработчика:");

        calculator.Calculate(20, 4, addition, "Сложение");
    }
}