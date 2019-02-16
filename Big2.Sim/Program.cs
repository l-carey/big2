using System;

namespace Big2.Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            var big2 = new Big2();
            int n;

            if (args.Length > 0 && int.TryParse(args[0], out n))
            {
                big2.Run(n);
            }
            else
            {
                big2.Run();
            }

            Console.ReadKey();
        }
    }
}
