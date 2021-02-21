using System;

namespace P3Net.Arx
{
    public static class ArrayExtensions
    {
        /// <summary>Sets all the values of an array to a specific value.</summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        public static void Set<T> ( this T[] source, T value )
        {
            for (var index = 0; index < source.Length; ++index)
                source[index] = value;
        }
    }
}
