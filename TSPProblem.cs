using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GeneticAlgorithms;

namespace GeneticAlgorithms.TSP
{
    public class TSPProblem : IProblem
    {
        protected City[] cities;
        protected int[] road;
        protected int Ns;
        protected int numberBitsForTheCity;
        Random r;

        //Default constructor
        public TSPProblem(int max, int numberOfCities)
        {
            Ns = numberOfCities;
            r = new Random(DateTime.Now.Millisecond);
            cities = new City[numberOfCities];
            for(int i=0;i<numberOfCities;i++)
            {
                cities[i] = new City() 
                { 
                    x = r.Next(max), 
                    y = r.Next(max) 
                };
            }
            road = new int[numberOfCities];
            using (StreamWriter sw = new StreamWriter($"TSP-{numberOfCities}.dat"))
            {
                for(int i=0;i<numberOfCities;i++)
                {
                    sw.WriteLine($"{cities[i].x} {cities[i].y}");
                }
                sw.Flush();
                sw.Close();
            }
        }


        /// <summary>
        /// Additional method for generating map of cities
        /// Default file name is "TSP-NumberOfCities.dat"
        /// </summary>
        /// <param name="max">Max value for the map</param>
        /// <param name="numberOfCities">Number of cities in the map</param>
        public void GenerateCitiesToFile(int max, int numberOfCities)
        {
            using (StreamWriter sw = new StreamWriter($"TSP-{numberOfCities}.dat"))
            {
                for (int i = 0; i < numberOfCities; i++)
                {
                    sw.WriteLine($"{r.Next(max)} {r.Next(max)}");
                }
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Additional constructor load cityMap from the file
        /// "TSP-numberOfCities.dat"
        /// </summary>
        /// <param name="numberOfCities">Number of cities for the problem</param>
        public TSPProblem(int numberOfCities)
        {
            Ns = numberOfCities;
            r = new Random(DateTime.Now.Millisecond);
            cities = new City[numberOfCities];
            road = new int[numberOfCities];
            //Finding number of bits for the city
            int buforC = numberOfCities;
            numberBitsForTheCity = 0;
            while (buforC>0)
            {
                buforC /= 2;
                numberBitsForTheCity++;
            }
            numberBitsForTheCity--;
            using (StreamReader sr = new StreamReader($"TSP-{numberOfCities}.dat"))
            {
                for (int i = 0; i < numberOfCities; i++)
                {
                    string bufor = sr.ReadLine();
                    string[] xy = bufor.Split( ' ');
                    cities[i] = new City()
                    {
                        x = Int32.Parse(xy[0]),
                        y = Int32.Parse(xy[1])
                    };
                }
                sr.Close();
            }
        }


        public void FenotypeToGenotype(bool [] solution, int [] road)
        {
            int size = solution.Length / road.Length;
            int maxValueForBinaryVector = (int)Math.Pow(2, size) - 1;
            for (int i = 0; i < road.Length; i++)
            {
                //Konwertowanie liczby na integer 0 - maxValueForBinaryVector
                int buf = road[i];
                //The binary coding
                for (int j = 0; j < size; j++)
                {
                    solution[i * size + j] = buf % 2 == 1; //thats mean the rest from div
                    buf = buf / 2;
                }
            }
        }

        public void GenotypeToFenotype(int[] road, bool[] solution)
        {
            int size = solution.Length / road.Length;
            int maxValueForBinaryVector = (int)Math.Pow(2, size) - 1;
            for (int i = 0; i < road.Length; i++)
            {
                int value = 0;
                int weight = 1;
                for (int m = i * size; m < i * size + size; m++)
                {
                    if (solution[m])
                        value += weight;
                    weight *= 2;
                }
                road[i] = value;
            }
        }

        public void InitSolution(bool [] solution)
        {
            List<int> path = new List<int>();
            while(path.Count<Ns)
            {
                int randomCity = r.Next(Ns);
                if (!path.Contains(randomCity))
                {
                    path.Add(randomCity);
                }
            }
            FenotypeToGenotype(solution, path.ToArray());
        }

        

        public double Evaluate(bool[] solution)
        {
            double distance = 0;
            GenotypeToFenotype(road, solution);
            for(int i=0;i<Ns-1;i++)
            {
                City c1 = cities[road[i]];
                City c2 = cities[road[i+1]];
                distance += City.Distance(c1, c2);
            }
            distance += City.Distance(cities[road[Ns - 1]], cities[road[0]]);
            return distance;
        }

        public void testFeasibilityAndRepair(bool[] solution)
        {
            GenotypeToFenotype(road, solution);
            //Repair of the phenotype
            int[] test = new int[Ns];
            //How many times we visit each city
            for(int i=0;i<Ns;i++)
            {
                test[road[i]]++;
            }
            //Repair
            for(int i=0;i<Ns;i++)
            {
                if (test[road[i]]>1) //if city is used to many times (only one)
                {
                    test[road[i]]--;
                    //we look for not used city
                    for(int j=0;j<Ns;j++)
                    {
                        if (test[j]==0)
                        {
                            test[j] = 1;
                            road[i] = j;
                        }
                    }
                }
            }
            FenotypeToGenotype(solution, road);
        }

        public int getNumberOfBits()
        {
            return Ns * numberBitsForTheCity;  //Number of cities x numberBitsForTheCity
        }
    }
}
