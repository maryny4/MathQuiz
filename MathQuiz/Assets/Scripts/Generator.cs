using System;
using System.Data;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AdditionSubtractionExpressionGenerator
{
    private static char GetRandomOperation(int number1, int number2)
    {
        char[] operations = { '+', '-' };
        int index = Random.Range(0, operations.Length);
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

        int index = Random.Range(0, operations.Length);
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
        char operator1 = operators[Random.Range(0, operators.Count)];
        char operator2 = operators[Random.Range(0, operators.Count)];

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
        char operator1 = operations[Random.Range(0, operations.Length)];
        char operator2 = operations[Random.Range(0, operations.Length)];

        int num1, num2, num3;
        string expression;

        do
        {
            num1 = Random.Range(minNum, maxNum + 1);
            num2 = Random.Range(minNum, maxNum + 1);
            num3 = Random.Range(minNum, maxNum + 1);

            if (Random.Range(0, 2) == 0)
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

public class GeneratorEquation_Find_X
{
    public static string GenerateRandomEquation_Find_X(int minValue, int maxValue)
    {
        int a, b, c;
        char operation;

        do
        {
            a = Random.Range(minValue, maxValue);
            b = Random.Range(minValue, maxValue);
            c = Random.Range(minValue, maxValue);
            operation = Random.Range(0, 2) == 0 ? '+' : '-';
        } while (a == c || b == c);

        return $"{a} {operation} x = {c}";
    }


    public static int CalculateEquationEquation_Find_X(string equation)
    {
        // Разбиваем уравнение на части
        string[] elements = equation.Split(' ');

        if (elements.Length != 5)
        {
            throw new ArgumentException("Invalid equation format.");
        }

        int a = int.Parse(elements[0]);
        char operation = elements[1][0];
        int c = int.Parse(elements[4]);

        if (operation == '+')
        {
            return c - a;
        }
        else if (operation == '-')
        {
            return a - c;
        }
        else
        {
            throw new ArgumentException("Invalid operation.");
        }
    }
}


public static class FunctionForGenerator
{
    public static (string example, double result) EndlessMode(int min, int max, int minWithBrackets,
        int maxWithBrackets, int minRandomExample, int maxRandomExample, decimal value1, decimal value2, decimal value3,
        decimal value4)
    {
        int randomChoice = Random.Range(1, 7);

        string example = "";
        double result = 0;

        switch (randomChoice)
        {
            case 1:
                example = AdditionSubtractionExpressionGenerator.GenerateAdditionSubtractionExpression(min, max);
                result = AdditionSubtractionExpressionGenerator.CalculateAdditionSubtractionExpression(example);
                break;

            case 2:
                example = MultiplicationDivisionExpressionGenerator.GenerateMultiplicationDivisionExpression(min, max);
                result = MultiplicationDivisionExpressionGenerator.CalculateMultiplicationDivisionExpression(example);
                break;

            case 3:
                example = ExpressionWithoutBracketsGenerator.GenerateExpressionWithoutBrackets(minWithBrackets,
                    maxWithBrackets, minRandomExample, maxRandomExample);
                result = ExpressionWithoutBracketsGenerator.CalculateExpressionWithoutBrackets(example);
                break;

            case 4:
                example = ExpressionWithBracketsGenerator.GenerateExpressionWithBrackets(minWithBrackets,
                    maxWithBrackets, minRandomExample, maxRandomExample);
                result = ExpressionWithBracketsGenerator.CalculateExpressionBrackets(example);
                break;

            case 5:
                int randomFunctionChoice = Random.Range(1, 6);

                switch (randomFunctionChoice)
                {
                    case 1:
                        example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value1,
                            minRandomExample, maxRandomExample);
                        result = (double)value1;
                        break;

                    case 2:
                        example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value2,
                            minRandomExample, maxRandomExample);
                        result = (double)value2;
                        break;

                    case 3:
                        example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value3,
                            minRandomExample, maxRandomExample);
                        result = (double)value3;
                        break;

                    case 4:
                        example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(value4,
                            minRandomExample, maxRandomExample);
                        result = (double)value4;
                        break;
                }

                break;

            case 6:
                example = GeneratorEquation_Find_X.GenerateRandomEquation_Find_X(min, max);
                result = GeneratorEquation_Find_X.CalculateEquationEquation_Find_X(example);
                break;
        }

        return (example, result);
    }

    public static (string example, double result) X_3(int MinValueWithandWithoutBrackets,
        int MaxValueWithandWithoutBrackets, int MinRangeResult, int MaxRangeResult)
    {
        int randomChoice = Random.Range(3, 5); // Теперь случаи 3 и 4

        ///если minRange равно -30 и maxRange равно 30, то генератор будет генерировать арифметические выражения, такие как "5 + 6 * 7", и проверять,
        /// что результат этого выражения находится в диапазоне от -30 до 30.
        /// Если результат выражения не соответствует этому диапазону, генерация будет повторена до тех пор, пока не будет получено подходящее выражение.
        /// MinValueRandomExample MinRange
        /// MaxValueRandomExample MaxRange
        string example = "";
        double result = 0;
        switch (randomChoice)
        {
            case 3:
                example = ExpressionWithoutBracketsGenerator.GenerateExpressionWithoutBrackets(
                    MinValueWithandWithoutBrackets, MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
                result = ExpressionWithoutBracketsGenerator.CalculateExpressionWithoutBrackets(example);
                break;

            case 4:
                example = ExpressionWithBracketsGenerator.GenerateExpressionWithBrackets(MinValueWithandWithoutBrackets,
                    MaxValueWithandWithoutBrackets, MinRangeResult, MaxRangeResult);
                result = ExpressionWithBracketsGenerator.CalculateExpressionBrackets(example);
                break;
        }

        return (example, result);
    }

    public static (string example, double result) GenerateEquation_Find_X(int minValue, int maxValue)
    {
        string example = "";
        double result = 0;
        example = GeneratorEquation_Find_X.GenerateRandomEquation_Find_X(minValue, maxValue);
        result = GeneratorEquation_Find_X.CalculateEquationEquation_Find_X(example);
        return (example, result);
    }

    public static (string example, double result) GenerateSumAndSubtractRiddle(int minValue, int maxValue)
    {
        string example = "";
        double result = 0;
        example = AdditionSubtractionExpressionGenerator.GenerateAdditionSubtractionExpression(minValue, maxValue);
        result = AdditionSubtractionExpressionGenerator.CalculateAdditionSubtractionExpression(example);
        return (example, result);
    }

    public static (string example, double result) GenerateDoubleRiddle(int minDouble, int maxDouble)
    {
        int randomChoice = Random.Range(1, 5); // Выбираем случайную функцию

        string example = "";
        double result = 0;

        switch (randomChoice)
        {
            case 1:
                example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.1M, minDouble,
                    maxDouble);
                result = 0.1;
                break;

            case 2:
                example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.25M, minDouble,
                    maxDouble);
                result = 0.25;
                break;

            case 3:
                example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.5M, minDouble,
                    maxDouble);
                result = 0.5;
                break;

            case 4:
                example = FloatingPointExpressionGenerator.GenerateRandomExampleForFloatingPoint(0.75M, minDouble,
                    maxDouble);
                result = 0.75;
                break;
        }

        return (example, result);
    }


    public static (string example, double result) GenerateMultiplicationAndDivisionRiddle(int minValue, int maxValue)
    {
        string example = "";
        double result = 0;
        example = MultiplicationDivisionExpressionGenerator
            .GenerateMultiplicationDivisionExpression(minValue, maxValue);
        result = MultiplicationDivisionExpressionGenerator.CalculateMultiplicationDivisionExpression(example);
        return (example, result);
    }
}

public static class ListForGenerator
{
    static double result1 = 0.1;
    static double result2 = 0.25;
    static double result3 = 0.5;
    static double result4 = 0.75;

    public static (List<string> list, Dictionary<double, List<string>> resultMapping) GenerateListsAndMappings(
        double result, double minValue, double maxValue)
    {
        List<string> list = new List<string>();
        Dictionary<double, List<string>> resultMapping = new Dictionary<double, List<string>>
        {
            {
                result1,
                new List<string> { result1.ToString(), result2.ToString(), result3.ToString(), result4.ToString() }
            },
            {
                result2,
                new List<string> { result2.ToString(), result1.ToString(), result3.ToString(), result4.ToString() }
            },
            {
                result3,
                new List<string> { result3.ToString(), result1.ToString(), result2.ToString(), result4.ToString() }
            },
            {
                result4,
                new List<string> { result4.ToString(), result1.ToString(), result2.ToString(), result3.ToString() }
            }
        };

        if (resultMapping.ContainsKey(result))
        {
            list.AddRange(resultMapping[result]);
        }
        else
        {
            if (result == 0)
            {
                list.Add(result.ToString());
                list.Add(Math.Round(result + 2).ToString());
                list.Add(Math.Round(result - 2).ToString());
                list.Add(Math.Round(result - 5).ToString());
            }
            else
            {
                list.Add(result.ToString());
                list.Add(Math.Round(result * 2).ToString());
                list.Add(Math.Round(result / 2).ToString());
                list.Add(Math.Round(result - 5).ToString());
            }
        }

        return (list, resultMapping);
    }
}

public static class Checkingbrackets
{
    public static string CheckingbracketsProcess(string input)
    {
        // Разделяем строку на отдельные элементы
        string[] elements = input.Split(' ');

        // Обработка чисел с минусом
        string pattern = @"(-\d+)";
        for (int i = 0; i < elements.Length; i++)
        {
            if (Regex.IsMatch(elements[i], pattern))
            {
                elements[i] = $"({elements[i]})"; // Добавляем скобки
            }
        }

        // Склеиваем элементы обратно в строку
        string result = string.Join(" ", elements);

        return result;
    }







    
}