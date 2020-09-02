using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LecAsync
{
    class Program
    {
        readonly Random _generator = new Random();
        readonly Stopwatch _stopWatch = new Stopwatch();

        static async Task Main(string[] args)
        {
            Program chef = new Program();
            await chef.CookAsync();
        }

        private async Task CookAsync()
        {
            _stopWatch.Start();
            Task breakfast1 = FixBreakfastAsync(1);
            Task breakfast2 = FixBreakfastAsync(2);
            Task breakfast3 = FixBreakfastAsync(3);
            await Task.WhenAll(breakfast1, breakfast2, breakfast3);

            _stopWatch.Stop();
            PrintElapsedTime();
        }

        async Task FixBreakfastAsync(int customerNumber)
        {
            await HeatPansAsync(customerNumber);
            Task fryEggs = FryEggsAsync(customerNumber);
            Task fryBacon = FryBaconAsync(customerNumber);
            await Task.WhenAll(fryEggs, fryBacon);

            Task toastAndButter = Task.Run(async () => {
                Toast toast = await ToastBreadAsync(customerNumber);
                await ButterToastAsync(toast, customerNumber);
            });
            await toastAndButter;
            
            await PourOJAsync(customerNumber);
            await PourCoffeeAsync(customerNumber);
        }

        async Task HeatPansAsync(int customerNumber)
        {
            Console.WriteLine($"Heating pans {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            await Task.Delay(milliseconds);
            Console.WriteLine($"Done heating pans {customerNumber}");
        }

        async Task FryEggsAsync(int customerNumber)
        {
            Console.WriteLine($"Frying eggs {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            await Task.Delay(milliseconds);
            Console.WriteLine($"Done frying eggs {customerNumber}");
        }

        async Task FryBaconAsync(int customerNumber)
        {
            Console.WriteLine($"Frying bacon {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            await Task.Delay(milliseconds);
            Console.WriteLine($"Done frying bacon {customerNumber}");
        }

        async Task<Toast> ToastBreadAsync(int customerNumber)
        {
            Console.WriteLine($"Toasting bread {customerNumber}...");
            int milliseconds = _generator.Next(1, 3) * 1000;
            await Task.Delay(milliseconds);
            Console.WriteLine($"Done toasting bread {customerNumber}");
            return new Toast();
        }

        async Task ButterToastAsync(Toast toast, int customerNumber)
        {
            Console.WriteLine($"Buttering toast {customerNumber}...");
            await Task.Delay(1000);
            Console.WriteLine($"Done buttering toast {customerNumber}");
        }

        async Task PourCoffeeAsync(int customerNumber)
        {
            Console.WriteLine($"Pouring coffee {customerNumber}...");
            await Task.Delay(1000);
            Console.WriteLine($"Done pouring coffee {customerNumber}");
        }

        async Task PourOJAsync(int customerNumber)
        {
            Console.WriteLine($"Pouring OJ {customerNumber}...");
            await Task.Delay(1000);
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