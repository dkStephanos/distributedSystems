using System;
using System.Diagnostics;
using System.Threading;

namespace LecAsync
{
    class Program
    {
        readonly Random _generator = new Random();
        readonly Stopwatch _stopWatch = new Stopwatch();

        static void Main(string[] args)
        {
            Program chef = new Program();
            chef.Cook();
        }

        private void Cook()
        {
            _stopWatch.Start();
            FixBreakfast(1);
            Console.WriteLine();
            FixBreakfast(2);
            Console.WriteLine();
            FixBreakfast(3);
            Console.WriteLine();
            _stopWatch.Stop();
            PrintElapsedTime();
        }

        void FixBreakfast(int customerNumber)
        {
            HeatPans(customerNumber);
            FryEggs(customerNumber);
            FryBacon(customerNumber);
            Toast toast = ToastBread(customerNumber);
            ButterToast(toast, customerNumber);
            PourOJ(customerNumber);
            PourCoffee(customerNumber);
        }

        void HeatPans(int customerNumber)
        {
            Console.WriteLine($"Heating pans {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            Thread.Sleep(milliseconds);
            Console.WriteLine($"Done heating pans {customerNumber}");
        }

        void FryEggs(int customerNumber)
        {
            Console.WriteLine($"Frying eggs {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            Thread.Sleep(milliseconds);
            Console.WriteLine($"Done frying eggs {customerNumber}");
        }

        void FryBacon(int customerNumber)
        {
            Console.WriteLine($"Frying bacon {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            Thread.Sleep(milliseconds);
            Console.WriteLine($"Done frying bacon {customerNumber}");
        }

        Toast ToastBread(int customerNumber)
        {
            Console.WriteLine($"Toasting bread {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            Thread.Sleep(milliseconds);
            Console.WriteLine($"Done toasting bread {customerNumber}");
            return new Toast();
        }

        void ButterToast(Toast toast, int customerNumber)
        {
            Console.WriteLine($"Buttering toast {customerNumber}...");
            Thread.Sleep(1000);
            Console.WriteLine($"Done buttering toast {customerNumber}");
        }

        void PourCoffee(int customerNumber)
        {
            Console.WriteLine($"Pouring coffee {customerNumber}...");
            Thread.Sleep(1000);
            Console.WriteLine($"Done pouring coffee {customerNumber}");
        }

        void PourOJ(int customerNumber)
        {
            Console.WriteLine($"Pouring OJ {customerNumber}...");
            Thread.Sleep(1000);
            Console.WriteLine($"Done pouring OJ {customerNumber}");
        }

        void PrintElapsedTime()
        {
            TimeSpan ts = _stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        class Toast
        {

        }
    }
}