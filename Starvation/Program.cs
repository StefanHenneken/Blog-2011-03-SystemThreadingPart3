using System;
using System.Threading;

namespace Threading
{
    public class Starvation
    {
        private object lockObject = new object();
        private double pi;

        static void Main(string[] args)
        {
            new Starvation();
        }
        public Starvation()
        {
            Thread hungryThread = new Thread(new ThreadStart(
                delegate ()
                {
                    while (true)
                    {
                        lock (lockObject)
                        {
                            // aufwendige Rechenoperation ...
                            pi = CalculatePi();
                        }
                        Console.WriteLine(pi);
                    }
                } ));
            hungryThread.Start();
            // jede Sekunde den Status der Rechenoperation ausgeben
            while (hungryThread.IsAlive)
            {
                Thread.Sleep(1000);
                lock (lockObject)
                {
                    Console.WriteLine("Calculating ...");
                }
            }
        }
        private double CalculatePi()
        {
            double radius = 1000;
            double kreistreffer = 0;
            for (double y = radius * (-1); y <= radius; y++)
            {
                double end = Math.Pow( radius , 2 );
                for (double x = radius * (-1); x <= end; x++)
                {
                    if ((Math.Pow(x, 2) + Math.Pow(y, 2)) <= Math.Pow(radius, 2))
                    {
                        kreistreffer = kreistreffer + 1;
                    }
                }
            }
            return kreistreffer / Math.Pow(radius, 2);
        }
    }
}
