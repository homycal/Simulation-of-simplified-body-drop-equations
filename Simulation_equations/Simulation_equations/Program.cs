using System;

namespace Simulation_equations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("a : ");
            float a = float.Parse(Console.ReadLine());
            Console.WriteLine("b : ");
            float b = float.Parse(Console.ReadLine());
            Console.WriteLine("c : ");
            float c = float.Parse(Console.ReadLine());

            Equation equation = new Equation(a, b, c);
            Console.WriteLine(equation);
        }
    }
}
