using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DienMay.ViewModel
{
    public class XuLyChung
    {
        public static IEnumerable<long> DivideEvenly(long numerator, long denominator)
        {
            int rem;
            int div = Math.DivRem((int)numerator, (int)denominator, out rem);

            for (int i = 0; i < denominator; i++)
            {
                yield return i < rem ? div + 1 : div;
            }
        }
    }
}
