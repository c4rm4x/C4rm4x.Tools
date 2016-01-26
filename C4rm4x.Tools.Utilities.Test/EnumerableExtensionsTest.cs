#region Using

using C4rm4x.Tools.TestUtilities;
using System;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.Utilities.Test
{
    public partial class EnumerableExtensionsTest
    {
        private static int GetRand(int max)
        {
            return new Random().Next(1, max);
        }

        private static IEnumerable<TSource> GetSource<TSource>(int numberOfItems)
        {
            for (int i = 0; i < numberOfItems; i++)
                yield return ObjectMother.Create<TSource>();
        }
    }
}
