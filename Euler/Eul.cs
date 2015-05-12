using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler
{
    class Eul
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Tools.AdjProd(new long [] { 2, 4, 5, 3, 4, 5, 2 }, 3)); 

        }

    }

    class Solutions
    {
        public static double Phi = 1.6180339887;
        public static double PhiNot = 1 - Phi;
        public static double evenFib(int n)
        {
            double sum = 0;
            if (n < 0) throw new ArgumentOutOfRangeException("Lowest Fib # is 0");
            else
            {
                int i = 0;
                long f = 0;
                do
                {
                    sum += f;
                    f = Tools.FibN(i);
                    i += 3;
                } while (f < n);
            }


            return sum;
        }
        public static int Palindrome(int maxFactor)
        {
            int pal = 0;
            int placeholder = -12;
            for (int i = maxFactor / 100; i < maxFactor; i++)
            {
                for (int j = maxFactor / 100; j < maxFactor; j++)
                {
                    placeholder = i * j;
                    if (Tools.IsPalin(placeholder) && placeholder > pal) pal = placeholder;
                }
            }
            return pal;
        }
    }


    public static class Tools
    {
        

        public static long FibN(int n)
        {
            long f = -1;
            double _f = (Math.Pow(Solutions.Phi, n) - Math.Pow(Solutions.PhiNot, n)) / Math.Sqrt(5);
            f = (long)Math.Round(_f);
            return f;
        }
        public static long gcf(long x, long y, long r = -1)
        {
            if (r == -1)
            {
                long temp1, temp2;
                temp1 = x;
                temp2 = y;
                x = Math.Max(temp1, temp2);
                y = Math.Min(temp1,temp2);
            }
            if(x%y==0)
            {
                return y;
            }
            else
            {
                return gcf(y, x % y, y % (x % y));
            }
        }
        public static long lcm(long x, long y)
        {
            return ((x * y) / Tools.gcf(x, y));
        }
        public static long lcm(int[] nums)
        {
            long lcm = nums [nums.Length-1];
            for (int i = nums.Length - 2; i >0; i--)
            {
                lcm = Tools.lcm(lcm, nums [i]);
            }
            return lcm;
        }
        public static long gcf(int[] nums)
        {
            long gcf = nums[0];
            for (int i =1; i<nums.Length; i++)
            {
                gcf = Tools.gcf(gcf,nums[i]);
            }
            return gcf;
        }
        public static bool IsPalin(int n)
        {
            bool pal = false;
            char[] norm = n.ToString().ToCharArray();
            char[] rev = n.ToString().ToCharArray();
            Array.Reverse(rev);
            string norms = new String(norm);
            string revs = new String(rev);
            int c = string.Compare(norms, revs);
            if (c == 0) pal = true;
            return pal;
        }
        public static long SquareSum(int n)
        {
            long diff = (long)Math.Pow(((n * (n + 1)) / 2),2);
            for (int i = 1; i<=n; i++)
            {
                diff -= (long)Math.Pow(i, 2);
            }
            return diff;
        }
        public static long[,] PrimeFactors(long n)
        {
            Primes p = new Primes((int)Math.Sqrt(n));
            long[] pnums = p.pnums;
            
            long buffer = n;
            long div = 0;

            int f = 0;
            for (int i = 0; i < pnums.Length; i++)
            {
                if ((n % pnums[i]) == 0) f++;
            }
            long[,] list = new long[f, 2];
            int j = 0;
            for (int i = 0; i < p.p; i++)
            {
                
                int k = 0;
                do
                {
                    
                    div = buffer % pnums[i];
                    
                    if (div == 0)
                    {
                        
                        if (i == 0 && k== 0 )
                        {
                            
                            buffer = n / pnums[i];
                            k++;
                        }
                        else
                        {
                            k++;
                            buffer = buffer / pnums[i];
                            
                        }
                    }
                    
                    
                } while (div == 0);
                if (k != 0)
                {
                    list[j, 0] = pnums[i];
                    list[j++, 1] = k;

                }
                
            }
            return list;
        }
        public static long LargestPrimeFactor(long n)
        {
            if (Tools.IsPrime(n)) throw new ArgumentOutOfRangeException("PRIME");
            Primes p = new Primes((long)Math.Sqrt(n));
            
            bool found = false;
            long f = 0;
            long i = p.pnums.Length-1;
            while (!found)
            {
                
                if (n % p.pnums[i] == 0)
                {
                    f = p.pnums [i];
                    found = true;
                }
                i--;
                if (i < 0) found = true;
            }
            return f;
        }
        public static string FactorList(long n)
        {
            
            var list = PrimeFactors(n);
            
            string s = "";
            for (int i = 0; i< list.GetLength(1); i++)
            {
                if (i != 0)
                {
                    s = s + "*";
                }
                s = s + list[i, 0].ToString() + "^" + list[i, 1].ToString();
            }
            

            return s;
        }

        //tests if n is a Prime Number by creating a Primes object and looking in the array of bools contained by the object at that position and ensuring it is false
        public static bool IsPrime(long n)
        {
            Primes p = new Primes((long)Math.Sqrt(n));
            foreach(int i in p.pnums)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        //takes a list of nums and looks for the largest subsequence product of length n
        public static long AdjProd(long[] nums, int n)
        {
            long temp = 0;
            long prod = 0;
            for (int i = 0; i<= nums.Length-n; i++)
            {
                temp = nums [i];
                for(int j = 1; j< n; j++)
                {
                    temp *= nums [i + j];
                }
                if (temp > prod) prod = temp;
            }
            return prod;
        }

    }
    public class Primes
    {
        string[] primes;
        long rangeRoot;
        public bool[] IsComp;
        public int c = 0;
        public int p = 0;
        public long[] pnums;
        public Primes(long range)
        {
            if (range > 2147483647) throw new ArgumentOutOfRangeException("too big");
            rangeRoot = (long)Math.Sqrt(range);
            IsComp = new bool[range + 1];



            for (int i = 2; i < rangeRoot; i++)
            {
                if (!IsComp[i])
                {
                    

                    for (int j = i * i; j <= range; j += i)
                    {
                        if (IsComp[j]) {
                        }
                        else
                        {
                            IsComp[j] = true;
                            c++;
                        }
                    }
                }

            }
            p = (int)range - 1 - c;
            pnums = new long[p];
            primes = new string[p];
            this.reduce();
        }
        public Primes(long range, bool reduce)
        {
            if (range > 2147483647)
            {
                throw new ArgumentOutOfRangeException("too big");
                
            }
            rangeRoot = (long)Math.Sqrt(range);
            IsComp = new bool [range + 1];



            for (int i = 2; i < rangeRoot; i++)
            {
                if (!IsComp [i])
                {


                    for (int j = i * i; j <= range; j += i)
                    {
                        if (IsComp [j])
                        {
                        }
                        else
                        {
                            IsComp [j] = true;
                            c++;
                        }
                    }
                }

            }
            p = (int)range - 1 - c;
            pnums = new long [p];
            primes = new string [p];
            if (reduce) this.reduce();
        }
        public bool[] getSieve()
        {
            return IsComp;
        }

        private void reduce()
        {
            int j = 0;
            for (int i = 2; i< IsComp.Length; i++)
            {
                if (!IsComp[i]&&i!=0) pnums[j++] = i;
            }

        }
        
        public long GetPrime(long n)
        {
            return pnums [n - 1];
        }
        
        override public string ToString()
        {
            
            int k = 0;
            for (int i = 2; i < IsComp.Length; i++)
            {
                if (!IsComp[i])
                {
                    primes[k++] = i.ToString();
                }
            }
            return String.Join(" ",primes);
        }
        
    }


}
