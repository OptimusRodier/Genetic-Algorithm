using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithms.TSP
{
    public class City
    {
        public int x;
        public int y;

        public static double Distance (City c1, City c2)
        {
            double buf1 = (c1.x - c2.x);
            double buf2 = (c1.y - c2.y);
            return Math.Sqrt(buf1 * buf1 + buf2 * buf2);
        }


    }
}
