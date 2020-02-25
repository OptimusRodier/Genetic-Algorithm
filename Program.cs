using System;
using System.Collections.Generic;

namespace KnapsackSubmission
{
    class Program
    {

        

        static void Main(string[] args)
        {
            List<Item> knapsack = new List<Item>();
            knapsack.Add(new Item() { Name = "painting", Cost = 2, Value = 7 });
            knapsack.Add(new Item() { Name = "Phone", Cost = 10, Value = 10 });
            knapsack.Add(new Item() { Name = "Food", Cost = 4, Value = 5 });
            knapsack.Add(new Item() { Name = "printer", Cost = 3, Value = 9 });
            knapsack.Add(new Item() { Name = "Game", Cost = 1, Value = 8 });
            knapsack.Add(new Item() { Name = "TV", Cost = 6, Value = 6 });
            knapsack.Add(new Item() { Name = "compuer", Cost = 6, Value = 4 });
            knapsack.Add(new Item() { Name = "diamond", Cost = 5, Value = 5 });
            knapsack.Add(new Item() { Name = "shoes", Cost = 10, Value = 12});
            knapsack.Add(new Item() { Name = "juice", Cost = 3, Value = 9 });
            knapsack.Add(new Item() { Name = "Game", Cost = 1, Value = 8 });
            knapsack.Add(new Item() { Name = "cat", Cost = 6, Value = 3 });
            knapsack.Add(new Item() { Name = "cable", Cost = 1, Value = 3 });
            knapsack.Add(new Item() { Name = "tablet", Cost = 6, Value = 1 });
            knapsack.Add(new Item() { Name = "ketchup", Cost = 1, Value = 2 });
            knapsack.Add(new Item() { Name = "bag", Cost = 3, Value = 7 });
            knapsack.Add(new Item() { Name = "rop", Cost = 1, Value = 12});
            knapsack.Add(new Item() { Name = "fezil", Cost = 4, Value = 12});

            List<String> stat = new List<string>();

            for (double p_c = 0.1; p_c < 1; p_c += 0.1)
            {
                double fits = 0.0;
                for (int r = 0; r < 100; r++)
                {
                    GeneticAlgorithm ga = new GeneticAlgorithm(20, 20, p_c, 0.1,10);
                    ga.knapsackItem = knapsack;
                    ga.runAlg();
                    fits += ga.GetBestfit();
                    //red value of fitness
                }
                stat.Add($"p_c={p_c}, fit={fits / 100.0}");
            }
            Console.WriteLine(String.Join("\n", stat));

        }
    }
}
