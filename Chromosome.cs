using System;
using System.Collections.Generic;
using System.Text;

namespace JobShop
{
    public class Chromosome
    {
        public int[] values;
        public int[][] matX;
        public int[] individualObjVal;
        public int objValue;
        public double fitness;
        public double probabilty;
        public double sumProbabilty;
        public double selectionRandom;

        public Chromosome(int n)
        {
            values = new int[n];
        }

        public void printX()
        {
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < matX.Length; i++)
                {
                    Console.Write("" + matX[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public Chromosome clone()
        {
            Chromosome cloned = new Chromosome(values.Length);

            cloned.values = (int[])values.Clone();

            int[][] clonedMat = new int[matX.Length][];
            for (int i = 0; i < matX.Length; i++)
            {
                
                clonedMat[i] = (int[])matX[i].Clone();
            }
            cloned.matX = clonedMat;

            cloned.individualObjVal = (int[])individualObjVal.Clone();
            cloned.objValue = objValue;
            cloned.fitness = fitness;
            cloned.probabilty = probabilty;
            cloned.sumProbabilty = sumProbabilty;
            cloned.selectionRandom = selectionRandom;

            return cloned;
        }

    }
}
