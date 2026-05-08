using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Program1
{
    class Program1
    {
        static void Main(string[] args)
        {
            // Konfiguracja 
            int size = 700;
            int threadCount = 12;
            int trials = 5;
            
            Console.WriteLine("Generowanie macierzy...");
            double[,] A = GenerateMatrix(size);
            double[,] B = GenerateMatrix(size);
            double[,] C = new double[size, size];

            Console.WriteLine($"\nPARAMETRY: Rozmiar {size}x{size}, Wątki: {threadCount}, Próby: {trials}");
            Console.WriteLine("-------------------------------------------------------------");

            // Zadanie 1: Parallel.For 
            long parallelTime = Benchmark(() => MultiplyParallel(A, B, C, threadCount), trials);
            Console.WriteLine($"[Zadanie 1] Parallel.For: {parallelTime} ms");

            // Zadanie 2: Klasa Thread
            long threadTime = Benchmark(() => MultiplyThreads(A, B, C, threadCount), trials);
            Console.WriteLine($"[Zadanie 2] Klasa Thread:  {threadTime} ms");

            Console.WriteLine("-------------------------------------------------------------");

            
            Console.WriteLine("\nGOTOWE! Spisz wyniki do tabeli.");
            Console.WriteLine("Naciśnij klawisz ENTER, aby zamknąć to okno...");
            Console.ReadLine();
        }
        // Pomocnicza metoda do generowania losowych macierzy
        static double[,] GenerateMatrix(int size)
        {
            Random rand = new Random();
            double[,] matrix = new double[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix[i, j] = rand.NextDouble();
            return matrix;
        }
        // Pomocnicza metoda do benchmarkowania czasu wykonania
        static long Benchmark(Action action, int trials)
        {
            Stopwatch sw = new Stopwatch();
            long totalMs = 0;
            for (int i = 0; i < trials; i++)
            {
                sw.Restart();
                action();
                sw.Stop();
                totalMs += sw.ElapsedMilliseconds;
            }
            return totalMs / trials;
        }
        // Zadanie 1: Mnożenie macierzy z użyciem Parallel.For
        static void MultiplyParallel(double[,] A, double[,] B, double[,] C, int threads)
        {
            int size = A.GetLength(0);
            var opt = new ParallelOptions { MaxDegreeOfParallelism = threads };
            Parallel.For(0, size, opt, i =>
            {
                for (int j = 0; j < size; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < size; k++) sum += A[i, k] * B[k, j];
                    C[i, j] = sum;
                }
            });
        }
        
        static void MultiplyThreads(double[,] A, double[,] B, double[,] C, int threadCount)
            // Implementacja mnożenia macierzy z użyciem klasy Thread (niski poziom)
        {
            
            int size = A.GetLength(0);
            Thread[] threads = new Thread[threadCount];
            int rowsPerThread = size / threadCount;
            // Każdy wątek będzie odpowiedzialny za obliczenie określonego zakresu wierszy macierzy C
            for (int t = 0; t < threadCount; t++)
            {
                int startRow = t * rowsPerThread; // Początkowy wiersz dla tego wątku
                int endRow = (t == threadCount - 1) ? size : (t + 1) * rowsPerThread; // Ostatni wątek może przejąć pozostałe wiersze jeśli size nie jest idealnie podzielne przez threadCount
                
                threads[t] = new Thread(() =>
                {// Każdy wątek wykonuje mnożenie dla swojego zakresu wierszy
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            double sum = 0;
                            for (int k = 0; k < size; k++) sum += A[i, k] * B[k, j];
                            C[i, j] = sum;
                        }
                    }
                });
                threads[t].Start();
            }
            foreach (var t in threads) t.Join(); // Czekamy na zakończenie wszystkich wątków przed kontynuacją
        }
    }
}