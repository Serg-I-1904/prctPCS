using System;

class Calculator
{
    private double currentValue = 0;
    private bool useCurrentValue = false;
    
    public void Run()
    {
        Console.WriteLine("Калькулятор");
        Console.WriteLine("Доступные операции: +, -, *, /, %, 1/x, x^2, sqrt, C (очистка), EXIT");
        Console.WriteLine("Формат ввода: операция число (например: + 3) или специальная команда");
        
        while (true)
        {
            try
            {
                Console.Write($"Текущее значение: {currentValue} > ");
                string input = Console.ReadLine()?.Trim().ToUpper();
                
                if (string.IsNullOrEmpty(input))
                    continue;
                    
                if (input == "EXIT")
                    break;
                    
                if (input == "C")
                {
                    currentValue = 0;
                    useCurrentValue = false;
                    Console.WriteLine("Значение сброшено на 0");
                    continue;
                }
                
                ProcessInput(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
    
    private void ProcessInput(string input)
    {
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (parts.Length == 1)
        {
            ProcessUnaryOperation(parts[0]);
        }
        else if (parts.Length == 2)
        {
            ProcessBinaryOperation(parts[0], parts[1]);
        }
        else
        {
            throw new ArgumentException("Неверный формат ввода. Используйте: операция число (например: + 3)");
        }
    }
    
    private void ProcessUnaryOperation(string operation)
    {
        switch (operation)
        {
            case "1/X":
                if (currentValue == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                currentValue = 1 / currentValue;
                Console.WriteLine($"Результат: {currentValue}");
                break;
                
            case "X^2":
                currentValue *= currentValue;
                Console.WriteLine($"Результат: {currentValue}");
                break;
                
            case "SQRT":
                if (currentValue < 0)
                    throw new ArgumentException("Корень из отрицательного числа невозможен");
                currentValue = Math.Sqrt(currentValue);
                Console.WriteLine($"Результат: {currentValue}");
                break;
                
            default:
                throw new ArgumentException($"Неизвестная операция: {operation}");
        }
    }
    
    private void ProcessBinaryOperation(string operation, string numberStr)
    {
        if (!double.TryParse(numberStr, out double number))
            throw new ArgumentException("Неверный числовой формат");
            
        if (!useCurrentValue)
        {
            currentValue = number;
            useCurrentValue = true;
            Console.WriteLine($"Установлено значение: {currentValue}");
            return;
        }
        
        double result = Calculate(currentValue, number, operation);
        currentValue = result;
        Console.WriteLine($"Результат: {result}");
    }
    
    private double Calculate(double firstNumber, double secondNumber, string operation)
    {
        switch (operation)
        {
            case "+":
                return firstNumber + secondNumber;
                
            case "-":
                return firstNumber - secondNumber;
                
            case "*":
                return firstNumber * secondNumber;
                
            case "/":
                if (secondNumber == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                return firstNumber / secondNumber;
                
            case "%":
                if (secondNumber == 0)
                    throw new DivideByZeroException("Деление на ноль невозможно");
                return firstNumber % secondNumber;
                
            default:
                throw new ArgumentException($"Неизвестная операция: {operation}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        calculator.Run();
    }
}