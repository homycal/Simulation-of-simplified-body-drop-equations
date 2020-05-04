using System;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            Console.WriteLine("Initial speed : ");
            float s = float.Parse(Console.ReadLine());
            Console.WriteLine("angle : ");
            float a = float.Parse(Console.ReadLine());
            Console.WriteLine("gravity : ");
            float g = float.Parse(Console.ReadLine());

            Equation equation = new Equation(s, a, g, 0);

            Console.WriteLine(equation);
            Console.WriteLine(equation.getHeight(2));
            Console.WriteLine(equation.getZeroHeight());
            Console.WriteLine(equation.getPosition(2));
            Console.WriteLine(equation.getSpeed(2));
            Console.WriteLine(equation.getAcceleration());

        }
    }
}
