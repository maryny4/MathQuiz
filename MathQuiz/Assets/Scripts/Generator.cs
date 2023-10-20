using System;
using System.Data;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class AdditionSubtractionExpressionGenerator
{
    private static char GetRandomOperation(int number1, int number2)
    {
        char[] operations = { '+', '-' };
        int index = Random.Range(0,operations.Length);
        return operations[index];
    }
    
    public static string GenerateAdditionSubtractionExpression(int minValue, int maxValue)
    {
        int number1 = Random.Range(minValue, maxValue);
        int number2 = Random.Range(minValue, maxValue);
        char operation = GetRandomOperation(number1, number2);

        return $"{number1} {operation} {number2}";
    }
    
    public static double CalculateAdditionSubtractionExpression(string expression)
    {
        string[] elements = expression.Split(' ');

        if (elements.Length != 3)
        {
            throw new ArgumentException("Invalid arithmetic expression format.");
        }

        int number1 = int.Parse(elements[0]);
        int number2 = int.Parse(elements[2]);
        char operation = elements[1][0];

        double result = operation switch
        {
            '+' => number1 + number2,
            '-' => number1 - number2,
            _ => throw new ArgumentException("Invalid arithmetic operation."),
        };

        return result;
    }
}
public class MultiplicationDivisionExpressionGenerator
{
    private static char GetRandomMultiplicationDivisionOperation(int number1, int number2)
    {
        char[] operations;

        if (number2 != 0 && number1 % number2 == 0)
        {
            operations = new char[] { '*', '/' };
        }
        else
        {
            operations = new char[] { '*' };
        }

        int index = Random.Range(0,operations.Length);
        return operations[index];
    }
    
    public static string GenerateMultiplicationDivisionExpression(int minValue, int maxValue)
    {
        int number1;
        do
        {
            number1 = Random.Range(minValue, maxValue);
        } while (number1 == 0);

        int number2;
        do
        {
            number2 = Random.Range(minValue, maxValue);
        } while (number2 == 0);

        char operation = GetRandomMultiplicationDivisionOperation(number1, number2);

        return $"{number1} {operation} {number2}";
    }

    public static double CalculateMultiplicationDivisionExpression(string expression)
    {
        string[] elements = expression.Split(' ');

        if (elements.Length != 3)
        {
            throw new ArgumentException("Invalid arithmetic expression format.");
        }

        int number1 = int.Parse(elements[0]);
        int number2 = int.Parse(elements[2]);
        char operation = elements[1][0];

        double result = operation switch
        {
            '*' => number1 * number2,
            '/' => (double)number1 / number2,
            _ => throw new ArgumentException("Invalid arithmetic operation."),
        };

        return result;
    }
}
public class ExpressionWithoutBracketsGenerator
{
    public static double CalculateExpressionWithoutBrackets(string expression)
    {
        DataTable dt = new DataTable();
        var result = dt.Compute(expression, "");
        return Convert.ToDouble(result);
    }
    private static bool IsIntegerResultWithoutBrackets(string expression, int minRange, int maxRange)
    {
        double result = CalculateExpressionWithoutBrackets(expression);
        return Math.Floor(result) == result && result >= minRange && result <= maxRange;
    }
    
    public static string GenerateExpressionWithoutBrackets(int minNum, int maxNum, int minRange, int maxRange)
    {
        List<char> operators = new List<char> { '+', '-', '*', '/' };
        char operator1 = operators[Random.Range(0,operators.Count)];
        char operator2 = operators[Random.Range(0,operators.Count)];

        int num1, num2, num3;
        string expression;

        do
        {
            num1 = Random.Range(minNum, maxNum + 1);
            num2 = Random.Range(minNum, maxNum + 1);
            num3 = Random.Range(minNum, maxNum + 1);

            expression = $"{num1} {operator1} {num2} {operator2} {num3}";

        } while (!IsIntegerResultWithoutBrackets(expression, minRange, maxRange));

        return expression;
    }
}
public class ExpressionWithBracketsGenerator
{
    public static double CalculateExpressionBrackets(string expression)
    {
        DataTable dt = new DataTable();
        var result = dt.Compute(expression, "");
        return Convert.ToDouble(result);
    }
    private static bool IsIntegerResultBrackets(string expression, int minRange, int maxRange)
    {
        double result = CalculateExpressionBrackets(expression);
        return Math.Floor(result) == result && result >= minRange && result <= maxRange;
    }
    
    public static string GenerateExpressionWithBrackets(int minNum, int maxNum, int minRange, int maxRange)
    {
        char[] operations = { '+', '-', '*', '/' };
        char operator1 = operations[Random.Range(0,operations.Length)];
        char operator2 = operations[Random.Range(0,operations.Length)];

        int num1, num2, num3;
        string expression;

        do
        {
            num1 = Random.Range(minNum, maxNum + 1);
            num2 = Random.Range(minNum, maxNum + 1);
            num3 = Random.Range(minNum, maxNum + 1);

            if (Random.Range(0,2) == 0)
            {
                expression = $"({num1} {operator1} {num2}) {operator2} {num3}";
            }
            else
            {
                expression = $"{num1} {operator1} ({num2} {operator2} {num3})";
            }
        } while (!IsIntegerResultBrackets(expression, minRange, maxRange));

        return expression;
    }
}
public class FloatingPointExpressionGenerator
{
    private static char GetRandomOperatorForFloatingPoint()
    {
        char[] operators = { '+', '-', '*', '/' };
        int index = Random.Range(0, operators.Length);
        return operators[index];
    }
    private static decimal CalculateExpressionForFloatingPoint(int number1, int number2, char selectedOperator)
    {
        switch (selectedOperator)
        {
            case '+':
                return (decimal)(number1 + number2);
            case '-':
                return (decimal)(number1 - number2);
            case '*':
                return (decimal)(number1 * number2);
            case '/':
                if (number2 != 0)
                {
                    return (decimal)(number1) / number2;
                }
                return 0;
            default:
                return 0;
        }
    }

    public static string GenerateRandomExampleForFloatingPoint(decimal targetResult, int minNumber, int maxNumber)
    {
        while (true)
        {
            int number1 = Random.Range(minNumber, maxNumber + 1);
            int number2 = Random.Range(minNumber, maxNumber + 1);
            char selectedOperator = GetRandomOperatorForFloatingPoint();

            decimal result = CalculateExpressionForFloatingPoint(number1, number2, selectedOperator);

            if (Math.Abs(result - targetResult) < 0.0001M)
            {
                return $"{number1} {selectedOperator} {number2}";
            }
        }
    }
}





///
///





