using System;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackSubmission
{

   

    public class GeneticAlgorithm
    {
        int C;                          //Knapsack cannot exceed this Max Cost value.                           
        int popSize;                    //Population Size  
        double crossoverProb;           //Corssover Probability
        double mutationProb;            //Mutation probability     
        int terminate=1;                //Used as a count. Algorithm terminates when terminate value equals generation value
        int generation;                 //Number of generations. Specified by user.
        public List<Item> knapsackItem;

        string[] population;            //Array to store chromosomes as binary strings.
        string chars;
        char[] stringChars;
        Random random;
        int[] chromosomeCost;           //Array to store cost for each chromosome from population array.
        int[] chromosomeValue;          //Array to store value for each chromosome from population array.
        string str1;
        string str2;

        public int GetBestfit()
        {
            int minval = chromosomeCost[0];
            for (int k=0;k<= chromosomeCost.Length; k++)
            {
                if (minval <= k)
                {
                    minval = chromosomeCost.Min();
                }
                

            }
            return minval;
        }

        char[] splitStr1;
        char[] splitStr2;
        int[] splitChromosome;          //Splits one chromosome string at a time. Each gene is at an index of this array. 
        int totalValue;                 //Total Value of a chromosome
        int totalCost;                  //Total cost of a chromosome
        int sumCost;
        int sumValue;
        string[] roulette;
        int Chromosome1;
        int Chromosome2;
        int[] genome1;
        int[] genome2;
        int numItems;                   //Total Number of items in knapsack

        public GeneticAlgorithm(int capacity, int popSizee, double crossover, double mutation,int generationn)
        {
            C = capacity; 
            popSize = popSizee;   
            crossoverProb = crossover;
            mutationProb = mutation;
            generation = generationn;
        }

        public void runAlg()
        {
            numItems = knapsackItem.Count;      
            population = new string[popSize];  
            chars = "01";
            stringChars = new char[numItems];
            chromosomeCost = new int[popSize];        
            chromosomeValue = new int[popSize];      
            random = new Random(System.DateTime.Now.Millisecond);


            Console.WriteLine("Genetic Algorithm for 0/1 Knapsack problem.\n");
            print();
            while (terminate <= generation)
            {
                genFirstRandom();
                roulettewheel();
                mutation();
                crossover();
                Fitness();
                terminate++;
            }
            solution();
        }
        public int solution()
        {
            //Solution of the 0/1 knapsack problem.
            //Pick highest value from population while less than knapsack cost
            //
            Console.WriteLine("\n-----------------------------------------------------------------\n");
            Console.WriteLine("Generations: " + generation);
            Console.WriteLine("Solution when Knapsack Max Cost = " + C + "\n");

            for (int i = 0; i < chromosomeCost.Length; i++)
            {
                if (chromosomeCost[i] > C)
                {
                    chromosomeValue[i] = 0;
                }
            }
            int maxValue = chromosomeValue.Max();
            int maxIndex = chromosomeValue.ToList().IndexOf(maxValue);
            str1 = population[maxIndex];
            splitStr1 = str1.ToCharArray();
            splitChromosome = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
            totalValue = 0;
            totalCost = 0;

            for (int m = 0; m < splitChromosome.Length; m++)
            {
                if (splitChromosome[m] == 1)
                {
                    Console.WriteLine("Name : " + knapsackItem[m].Name + "\tCost : " + knapsackItem[m].Cost + "\tValue : " + knapsackItem[m].Value);
                    totalCost = totalCost + knapsackItem[m].Cost;
                    totalValue = totalValue + knapsackItem[m].Value;
                }
            }
            Console.WriteLine("\nTotal Cost = " + totalCost);
            Console.WriteLine("Total Value = " + totalValue);
            Console.WriteLine("\n-----------------------------------------------------------------");

           // terminate = 1;
            return totalValue;
        }
        private void Fitness()
        {
            //Fitness function. Select the highest valued chromosome while less than knapsack Max Cost.
            //
            if (chromosomeCost[Chromosome1] > C)
            {
                if (chromosomeCost[Chromosome2] <= C)
                {
                    for (int i = 0; i < population.Length; i++)
                    {
                        if (chromosomeValue[i] < chromosomeValue[Chromosome2])
                        {
                            population[i] = roulette[1];
                            chromosomeCost[i] = chromosomeCost[Chromosome2];
                            chromosomeValue[i] = chromosomeValue[Chromosome2];
                            break;
                        }
                        else if (chromosomeCost[i] > C)
                        {
                            population[i] = roulette[1];
                            chromosomeCost[i] = chromosomeCost[Chromosome2];
                            chromosomeValue[i] = chromosomeValue[Chromosome2];
                            break;
                        }
                    }
                }
            }
            else if (chromosomeCost[Chromosome2] > C)
            {
                if (chromosomeCost[Chromosome1] <= C)
                {
                    for (int i = 0; i < population.Length; i++)
                    {
                        if (chromosomeValue[i] < chromosomeValue[Chromosome1])
                        {
                            population[i] = roulette[0];
                            chromosomeCost[i] = chromosomeCost[Chromosome1];
                            chromosomeValue[i] = chromosomeValue[Chromosome1];
                            break;
                        }
                        else if (chromosomeCost[i] > C)
                        {
                            population[i] = roulette[0];
                            chromosomeCost[i] = chromosomeCost[Chromosome1];
                            chromosomeValue[i] = chromosomeValue[Chromosome1];
                            break;
                        }
                    }
                }
            }
            else if (chromosomeValue[Chromosome1] >= chromosomeValue[Chromosome2])
            {
                for (int i = 0; i < population.Length; i++)
                {
                    if (chromosomeValue[i] < chromosomeValue[Chromosome1])
                    {
                        population[i] = roulette[0];
                        chromosomeCost[i] = chromosomeCost[Chromosome1];
                        chromosomeValue[i] = chromosomeValue[Chromosome1];
                        break;
                    }
                    else if (chromosomeCost[i] > C)
                    {
                        population[i] = roulette[0];
                        chromosomeCost[i] = chromosomeCost[Chromosome1];
                        chromosomeValue[i] = chromosomeValue[Chromosome1];
                        break;
                    }
                }
            }
            else if (chromosomeValue[Chromosome2] >= chromosomeValue[Chromosome1])
            {
                for (int i = 0; i < population.Length; i++)
                {
                    if (chromosomeValue[i] < chromosomeValue[Chromosome2])
                    {
                        population[i] = roulette[1];
                        chromosomeCost[i] = chromosomeCost[Chromosome2];
                        chromosomeValue[i] = chromosomeValue[Chromosome2];
                        break;
                    }
                    else if (chromosomeCost[i] > C)
                    {
                        population[i] = roulette[1];
                        chromosomeCost[i] = chromosomeCost[Chromosome2];
                        chromosomeValue[i] = chromosomeValue[Chromosome2];
                        break;
                    }
                }
            }
        }
        private void crossover()
        {
            //Crossover occurs
            //
            double randomDoubleNumber2 = random.NextDouble() * (1.00 - 0.00) + 0.00;
            double crossoverOccurs = Math.Round(randomDoubleNumber2, 2);                    //Number to determine if crossover occurs
            if (crossoverOccurs <= crossoverProb)
            {
                //Console.WriteLine("\n<< CROSSOVER OCCURED >>");
                str1 = roulette[0];
                str2 = roulette[1];
                splitStr1 = str1.ToCharArray();
                splitStr2 = str2.ToCharArray();
                genome1 = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                genome2 = Array.ConvertAll(splitStr2, c => (int)Char.GetNumericValue(c));
                int ranCrossoverFrom = random.Next(0, 4);
                //Console.WriteLine("\nCrossover from index: " + ranCrossoverFrom);
                for (int i = 0; i <= ranCrossoverFrom; i++)
                {
                    int getGene1 = genome1[i];
                    int getGene2 = genome2[i];
                    genome1[i] = getGene2;
                    genome2[i] = getGene1;
                }
                totalValue = 0;
                totalCost = 0;
                for (int m = 0; m < genome1.Length; m++)
                {
                    if (genome1[m] == 1)
                    {
                        totalCost = totalCost + knapsackItem[m].Cost;
                        totalValue = totalValue + knapsackItem[m].Value;
                    }
                    chromosomeCost[Chromosome1] = totalCost;
                    chromosomeValue[Chromosome1] = totalValue;
                }
                totalValue = 0;
                totalCost = 0;
                for (int m = 0; m < genome1.Length; m++)
                {
                    if (genome2[m] == 1)
                    {
                        totalCost = totalCost + knapsackItem[m].Cost;
                        totalValue = totalValue + knapsackItem[m].Value;
                    }
                    chromosomeCost[Chromosome2] = totalCost;
                    chromosomeValue[Chromosome2] = totalValue;
                }

                str1 = string.Join("", genome1);
                str2 = string.Join("", genome2);
                roulette[0] = str1;
                roulette[1] = str2;

                //Console.Write("\nCrossover chromosomes:");
                Console.WriteLine("\n" + roulette[0] + "\t" + chromosomeCost[Chromosome1] + "\t" + chromosomeValue[Chromosome1]);
                //Console.WriteLine(roulette[1] + "\t" + chromosomeCost[Chromosome2] + "\t" + chromosomeValue[Chromosome2]);      
            }
        }
        private void mutation()
        {
            //Mutation of chromosomes
            //
            double randomDoubleNumber1 = random.NextDouble();// * (1.000 - 0.000) + 0.000;
            double mutationOccurs = Math.Round(randomDoubleNumber1, 3);                    //Number to determine if mutation occurs
            
            if (mutationOccurs <= mutationProb)
            {
                //Console.WriteLine("\n<< MUTATION OCCURED >>");
                int ranChooseChromosome = random.Next(0, 2);
                //if (ranChooseChromosome == 0)
                //{
                //    Console.WriteLine("\nMutate 1st chromosome.");
                //}
                //else Console.WriteLine("\nMutate 2nd chromosome.");
                str1 = roulette[ranChooseChromosome];
                splitStr1 = str1.ToCharArray();
                genome1 = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                int ranMutateAt = random.Next(0, 4);
                //Console.WriteLine("Mutation at index: " + ranMutateAt);
                if (genome1[ranMutateAt] == 0)
                {
                    genome1[ranMutateAt] = 1;
                }
                else if (genome1[ranMutateAt] == 1)
                {
                    genome1[ranMutateAt] = 0;
                }

                totalValue = 0;
                totalCost = 0;
                for (int m = 0; m < genome1.Length; m++)
                {
                    if (genome1[m] == 1)
                    {
                        totalCost = totalCost + knapsackItem[m].Cost;
                        totalValue = totalValue + knapsackItem[m].Value;
                    }
                }
                if (ranChooseChromosome == 0)
                {
                    chromosomeCost[Chromosome1] = totalCost;
                    chromosomeValue[Chromosome1] = totalValue;
                }
                else if (ranChooseChromosome == 1)
                {
                    chromosomeCost[Chromosome2] = totalCost;
                    chromosomeValue[Chromosome2] = totalValue;
                }

                str1 = string.Join("", genome1);
                roulette[ranChooseChromosome] = str1;

                //Console.Write("\nMutated chromosomes:");
                //Console.WriteLine("\n" + roulette[0] + "\t" + chromosomeCost[Chromosome1] + "\t" + chromosomeValue[Chromosome1]);
                //Console.WriteLine(roulette[1] + "\t" + chromosomeCost[Chromosome2] + "\t" + chromosomeValue[Chromosome2]);                            
            }
        }
        public int roulettewheel()
        {
            //Roulette wheel for selecting 2 chromosomes from population
            //
            int[] array = new int[110];
            float percent;                          //Percent cost for one chromosome
            int totalPercent = 0;                   //To work out total percent.
            int q = 0;
            int s = 0;
            //Console.Write("\n");
            for (int c = 0; c < population.Length; c++)
            {
                percent = ((float)chromosomeCost[c] / (float)sumCost) * 100;
                percent = Convert.ToInt32(percent);
                //Console.WriteLine(percent);
                for (int r = 0; r < percent; r++)
                {
                    array[s] = q;
                    s++;
                }
                q++;
                totalPercent = totalPercent + Convert.ToInt32(percent);
            }

            /**
            Console.WriteLine("\n% Values for Roulette Wheel");
            for (int n = 0; n < totalPercent; n++)
            {
                Console.Write(array[n]);
            }
            **/

            int randomNumber1 = random.Next(0, totalPercent);                       //Random number for index of array to select a chromosome.
            int randomNumber2 = random.Next(0, totalPercent);                       //Random number for index of array to select a chromosome.
                                                                                    //Console.WriteLine("\n\nRoulette random no.1 = " + randomNumber1);
                                                                                    //Console.WriteLine("Roulette random no.2 = " + randomNumber2);
            roulette = new string[2];                                      //Array to store the two selected values by roulette wheel.

            Chromosome1 = array[randomNumber1];                                 //Index of first chromosome selected from array
            Chromosome2 = array[randomNumber2];                                 //Index of second chromosome selected from array
            roulette[0] = population[Chromosome1];                                  //Chromosome is selected from population array based on chromosome 1 index
            roulette[1] = population[Chromosome2];                                  //Chromosome is selected from population array based on chromosome 2 index

            //Console.Write("\n<< ROULETTE WHEEL >>\n");
            //Console.Write("\nSelected chromosomes by roulette wheel:");
            //Console.WriteLine("\n" + roulette[0] + "\t" + chromosomeCost[Chromosome1] + "\t" + chromosomeValue[Chromosome1]);
            //Console.WriteLine(roulette[1] + "\t" + chromosomeCost[Chromosome2] + "\t" + chromosomeValue[Chromosome2]);
            return chromosomeValue[Chromosome1];
        }
        private void genFirstRandom()
        {
            //Generate first random population chromosomes
            //
            if (terminate == 1)
            {
                for (int j = 0; j < popSize; j++)
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }
                    population[j] = new String(stringChars);
                }
            }

            for (int i = 0; i < popSize; i++)
            {
                str1 = population[i];
                splitStr1 = str1.ToCharArray();
                splitChromosome = Array.ConvertAll(splitStr1, c => (int)Char.GetNumericValue(c));
                totalValue = 0;
                totalCost = 0;

                for (int m = 0; m < splitChromosome.Length; m++)
                {
                    if (splitChromosome[m] == 1)
                    {
                        totalCost = totalCost + knapsackItem[m].Cost;
                        totalValue = totalValue + knapsackItem[m].Value;
                    }
                }
                chromosomeCost[i] = totalCost;
                chromosomeValue[i] = totalValue;
            }
            sumCost = chromosomeCost.Sum();
            sumValue = chromosomeValue.Sum();
            
        }
        public void print()
        {
            Console.WriteLine("<Items>\n");
            for (int a = 0; a < knapsackItem.Count; a++)        //Show Items at Start.
            {
                Console.WriteLine(knapsackItem[a]);
            }
        }
    }
}