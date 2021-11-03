﻿using System;

namespace FundamentalsCalculator
{
    // TASK 3
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first number");
            var a = Console.ReadLine();
            Console.WriteLine("Enter addition/subtraction/division/multiplication symbol");
            var operationType = Console.ReadLine();
            Console.WriteLine("Enter second number");
            var b = Console.ReadLine();            
            
            var parsedA = Int16.Parse(a);// что будет если не сможет спарсить?
            var parsedB = Int16.Parse(b);// что будет если не сможет спарсить?

            // нужно обработать ситуацию если будте введен символ отличный от +,-,/,*
            // хоршо бы использовать switch
            if (operationType == "+")
                MathHandler.AdditionHelper(parsedA, parsedB);
            if (operationType == "-")
                MathHandler.SubtractionHelper(parsedA, parsedB);
            if (operationType == "/")
                MathHandler.DivisionHelper(parsedA, parsedB);
            if (operationType == "*")
                MathHandler.MultiplicationHelper(parsedA, parsedB);
        }
    }


    class MathHandler  //классы нужно делать в отдельных файлах
    {
        public static void AdditionHelper(int a, int b)
        {
            int sum = a + b;
            Console.WriteLine("Result is " + sum);
        }
        public static void SubtractionHelper(int a, int b)
        {
            int sum = a - b;
            Console.WriteLine("Result is " + sum);
        }
        public static void DivisionHelper(int a, int b)
        {
            int sum = a / b;
            Console.WriteLine("Result is " + sum);
        }
        public static void MultiplicationHelper(int a, int b)
        {
            int sum = a * b;
            Console.WriteLine("Result is " + sum);
        }
    }
}
