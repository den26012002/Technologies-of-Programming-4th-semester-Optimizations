using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public class CountedArrayPoolDecorator<T>
    {
        private static int _counter = 0;
        private static int _maxCounterValue = 1500;
        // private static ArrayPool<T> _pool = ArrayPool<T>.Create();
        private static MyArrayPool<T> _pool = new MyArrayPool<T>(new int[]{ 11000, 5500, 5500, 2250, 2250 }, 10000);
        // private static HashSet<int> _sizes = new HashSet<int>();

        public static T[] Rent(int minimumLength)
        {
            // ++_counter;
            /*if (_counter > _maxCounterValue)
            {
                Console.WriteLine($"Warning: number of arrays is bigger than {_maxCounterValue}");
                _maxCounterValue *= 2;
            }*/

            T[] rentedArray = _pool.Rent(minimumLength);
            // _sizes.Add(rentedArray.Length);

            // return ArrayPool<T>.Shared.Rent(minimumLength);
            return rentedArray;
        }

        public static void Return(T[] array)
        {
            //--_counter;
            // ArrayPool<T>.Shared.Return(array);
            _pool.Return(array);
        }

        public static void PrintSizes()
        {
            foreach (int size in _sizes)
            {
                Console.WriteLine($"Size: {size}");
            }
        }
    }
}
