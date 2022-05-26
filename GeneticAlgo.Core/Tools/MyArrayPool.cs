﻿using System;
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
        private Dictionary<int, HashSet<int>> _freeIndexes;
        // private Dictionary<int, int> _capacities;
        private int _maxUnusedTime;


        public MyArrayPool(int[] startCapacity, int maxUnusedTime)
        {
            _pools = new Dictionary<int, List<T[]>>();
            _isUsed = new Dictionary<int, List<bool>>();
            _unusedTime = new Dictionary<int, List<int>>();
            _freeIndexes =  new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < startCapacity.Length; ++i)
            {
                int index = (int)Math.Pow(2, 4 + i);
                _pools[index] = new List<T[]>(startCapacity[i]);
                _isUsed[index] = new List<bool>(startCapacity[i]);
                _unusedTime[index] = new List<int>(startCapacity[i]);
                _freeIndexes[index] = new HashSet<int>();
                /*for (int j = 0; j < startCapacity[i]; ++j)
                {
                    _freeIndexes[index].Add(j);
                }*/
            }

            _maxUnusedTime = maxUnusedTime;
        }

        /*public T[] Rent(int minimumLength)
        {
            minimumLength = GetCorrectLenght(minimumLength);
            List<T[]> pool = _pools[minimumLength];
            int rentedIndex = -1;
            int rentedArrayLength = -1;
            int firstNullIndex = -1;
            foreach (var i in _freeIndexes[minimumLength])
            {
                if (pool[i] == null)
                {
                    firstNullIndex = i;
                    continue;
                }

                if (_isUsed[minimumLength][i] || pool[i].Length < minimumLength)
                {
                    continue;
                }

                if (rentedArrayLength == -1 || pool[i].Length < rentedArrayLength)
                {
                    rentedIndex = i;
                    rentedArrayLength = pool[i].Length;
                }
            }

            if (rentedIndex == -1)
            {
                if (firstNullIndex == -1)
                {
                    pool.Add(new T[minimumLength]);
                    _isUsed[minimumLength].Add(true);
                    _unusedTime[minimumLength].Add(0);
                    for (int i = _capacities[minimumLength] + 1; i < pool.Capacity; ++i)
                    {
                        _freeIndexes[minimumLength].Add(i);
                    }
                    _capacities[minimumLength] = pool.Capacity;

                    rentedIndex = pool.Count - 1;
                    rentedArrayLength = minimumLength;
                }
                else
                {
                    pool[firstNullIndex] = new T[minimumLength];
                    rentedIndex = firstNullIndex;
                    rentedArrayLength = minimumLength;
                }
            }

            _isUsed[minimumLength][rentedIndex] = true;
            _unusedTime[minimumLength][rentedIndex] = 0;
            Update();
            return pool[rentedIndex];
        }*/

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
                int index = _freeIndexes[minimumLength].First();
                _freeIndexes[minimumLength].Remove(index);
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
                    _freeIndexes[array.Length].Add(i);
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
