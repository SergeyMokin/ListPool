﻿using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace ListPool.Benchmarks
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    [GcServer(true)]
    [GcConcurrent]
    public class ListPoolCreateAndAddAndEnumerateAReferenceBenchmarks
    {
        [Params(48, 1_024, 10_240)]
        public int N { get; set; }

        private static readonly string _stringToAdd = Guid.NewGuid().ToString();

        [Benchmark(Baseline = true)]
        public int List()
        {
            string stringToAdd = _stringToAdd;
            int count = 0;
            List<string> list = new List<string>(N);
            for (int i = 0; i < N; i += 8)
            {
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
            }

            foreach (string item in list)
            {
                count += item.Length;
            }

            return count;
        }

        [Benchmark]
        public int ListPool()
        {
            string stringToAdd = _stringToAdd;
            int count = 0;
            using ListPool<string> list = new ListPool<string>(N);
            for (int i = 0; i < N; i += 8)
            {
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
                list.Add(stringToAdd);
            }

            foreach (string item in list)
            {
                count += item.Length;
            }

            return count;
        }
    }
}
