using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.Tools
{
    public class MyArrayPool<T>
    {
        private Dictionary<int, List<T[]>> _pools;
        private Dictionary<int, List<bool>> _isUsed;
        private Dictionary<int, List<int>> _unusedTime;
        private Dictionary<int, Queue<int>> _freeIndexes;
        private int _maxUnusedTime;


        public MyArrayPool(int[] startCapacity, int maxUnusedTime)
        {
            _pools = new Dictionary<int, List<T[]>>();
            _isUsed = new Dictionary<int, List<bool>>();
            _unusedTime = new Dictionary<int, List<int>>();
            _freeIndexes =  new Dictionary<int, Queue<int>>();
            for (int i = 0; i < startCapacity.Length; ++i)
            {
                int index = (int)Math.Pow(2, 4 + i);
                _pools[index] = new List<T[]>(startCapacity[i]);
                _isUsed[index] = new List<bool>(startCapacity[i]);
                _unusedTime[index] = new List<int>(startCapacity[i]);
                _freeIndexes[index] = new Queue<int>();
            }

            _maxUnusedTime = maxUnusedTime;
        }

        public T[] Rent(int minimumLength)
        {
            minimumLength = GetCorrectLenght(minimumLength);
            if (_freeIndexes[minimumLength].Count == 0)
            {
                _pools[minimumLength].Add(new T[minimumLength]);
                _isUsed[minimumLength].Add(true);
                _unusedTime[minimumLength].Add(0);
                // Update();
                return _pools[minimumLength][_pools[minimumLength].Count - 1];
            }
            else
            {
                int index = _freeIndexes[minimumLength].Dequeue();
                _isUsed[minimumLength][index] = true;
                _unusedTime[minimumLength][index] = 0;
                // Update();
                return _pools[minimumLength][index];
            }
        }

        public void Return(T[] array)
        {
            List<T[]> pool = _pools[array.Length];
            bool isInPool = false;
            for (int i = 0; i < pool.Count; ++i)
            {
                if (pool[i] == array)
                {
                    _isUsed[array.Length][i] = false;
                    _unusedTime[array.Length][i] = 0;
                    _freeIndexes[array.Length].Enqueue(i);
                    isInPool = true;
                }
            }

            // Update();

            if (!isInPool)
            {
                throw new Exception("Error: try to return the array that wasn't rented");
            }
        }

        private void Update()
        {
            foreach (var item in _pools)
            {
                List<T[]> pool = item.Value;
                for (int i = 0; i < pool.Count; ++i)
                {
                    if (pool[i] == null)
                    {
                        continue;
                    }

                    if (_isUsed[pool[i].Length][i] == false)
                    {
                        ++_unusedTime[pool[i].Length][i];
                    }

                    if (_unusedTime[pool[i].Length][i] > _maxUnusedTime)
                    {
                        pool[i] = null;
                    }
                }
            }
        }
        private int GetCorrectLenght(int minimumLength)
        {
            int start = 16;
            while (start < minimumLength)
            {
                start *= 2;
            }

            return start;
        }
    }
}
