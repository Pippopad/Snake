using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGEngine
{
    public static class Random
    {
        private static System.Random rnd;

        public static void Init()
        {
            rnd = new System.Random();
        }

        public static int NextInt(int maxValue)
        {
            return rnd.Next(maxValue);
        }
    }
}
