using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EShop.CommonTestComponents
{
    public class EnumerableComparer<T> : IEqualityComparer<T>
    {
        Func<T?, T?, bool> func;
        public EnumerableComparer(Func<T?, T?, bool> comparer)
        {
            func = comparer;
        }
        bool IEqualityComparer<T>.Equals(T? x, T? y)
        {
            return func(x, y);
        }

        int IEqualityComparer<T>.GetHashCode([DisallowNull]T obj)
        {
            return obj.GetHashCode();
        }
    }
}
